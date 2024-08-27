using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using System.Reflection;
using TMS.Application.Consul;
using TMS.Application.Security;
using TMS.Application.UseCases.DI;
using TMS.Notes.DataAccess;
using TMS.Notes.DataAccess.Repositories;
using TMS.Notes.Service.Services;
using TMS.Notes.UseCases.Abstractions;
using TMS.Notes.UseCases.Common;
using TMS.Notes.UseCases.Notes.Commands.CreateNote;

namespace TMS.Notes.Service;

/// <summary>
/// Экземпляр класса <see cref="Program"/>.
/// </summary>
public class Program
{
    private static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .WriteTo.Async(a => a.Console())
        .WriteTo.Async(a => a.File("logs/NotesWebAppLog.txt", rollingInterval: RollingInterval.Day))
        .CreateLogger();

        try
        {
            Log.Information("Starting up the application");
            var builder = ConfigureApp(args);
            await RunApp(builder);
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "An error occurred while app initialization");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static WebApplicationBuilder ConfigureApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Host.UseSerilog();

        var services = builder.Services;

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        services.AddSwagger(xmlFilePath);

        services.AddHttpContextAccessor();

        services.AddConsul(builder.Configuration);

        services.AddJwtSettingsFromConsul(builder.Configuration["ConsulKey"]!);

        services.AddJwtBearerAuthentication();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:3000")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        services.AddHealthChecks();

        services.AddConsul(builder.Configuration);

        ConfigureDI(services, builder.Configuration);

        return builder;
    }

    private static void ConfigureDI(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<NoteDbContext>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IUserAccessor, UserAccessor>();

        services.AddAutoMapper(typeof(MappingProfile).Assembly, typeof(RepositoryMappingProfile).Assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateNoteCommand).Assembly));
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<CreateNoteCommandValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    private static async Task RunApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseCors();

        app.UseMiddleware<ErrorExceptionHandler>();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        //using (var scope = app.Services.CreateScope())
        //{
        //    var db = scope.ServiceProvider.GetRequiredService<NoteDbContext>();
        //    db.Database.Migrate();
        //}

        await app.RunAsync();
    }
}