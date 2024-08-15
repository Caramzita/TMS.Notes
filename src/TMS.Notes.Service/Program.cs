using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text;
using TMS.Notes.DataAccess;
using TMS.Notes.DataAccess.Repositories;
using TMS.Notes.Service.MiddleWare;
using TMS.Notes.Service.Services;
using TMS.Notes.UseCases.Abstractions;
using TMS.Notes.UseCases.Common;
using TMS.Notes.UseCases.Common.Behaviors;
using TMS.Notes.UseCases.Notes.Commands.CreateNote;

namespace TMS.Notes.Service;

/// <summary>
/// Ёкземпл€р класса <see cref="Program"/>.
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
        //builder.Configuration
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var services = builder.Services;

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        services.AddSwaggerGen(opts =>
        {
            opts.IncludeXmlComments(xmlFilePath);

            opts.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Description = @"Enter access token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            opts.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme,
                        },
                    },
                    new List<string>()
                }
            });
        });

        services.AddHttpContextAccessor();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opts =>
        {
            opts.TokenValidationParameters = new TokenValidationParameters
            {
                //ValidAudience = builder.Configuration["JwtSecurityToken:Audience"],
                //ValidIssuer = builder.Configuration["JwtSecurityToken:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["K17T6p+mYlBuIll6EOQDUmAdM6xmzeHOpE+O35zsAvw="]!)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
        });


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

        ConfigureDI(services, builder.Configuration);

        return builder;
    }

    private static void ConfigureDI(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<NoteDbContext>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IUserAccessor, UserAccessor>();

        services.AddAutoMapper(cfg => cfg.AddProfile(typeof(MappingProfile)));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateNoteCommand).Assembly));

        services.AddValidatorsFromAssemblyContaining<CreateNoteCommandValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
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

        app.UseMiddleware<ErrorExceptionHandler>();

        app.UseRouting();
        app.UseCors();

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