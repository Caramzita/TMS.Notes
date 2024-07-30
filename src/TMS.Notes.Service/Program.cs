using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using TMS.Notes.DataAccess;
using TMS.Notes.DataAccess.Repositories;
using TMS.Notes.Service.MiddleWare;
using TMS.Notes.UseCases;
using TMS.Notes.UseCases.Abstractions;
using TMS.Notes.UseCases.Notes.Commands.CreateNote;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<NoteDbContext>(
        options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection"))
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine);
        });

    builder.Services.AddScoped<INoteRepository, NoteRepository>();
    builder.Services.AddAutoMapper(cfg => cfg.AddProfile(typeof(MappingProfile)));
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateNoteCommand).Assembly));

    builder.Services.AddValidatorsFromAssemblyContaining<CreateNoteCommandValidator>();
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });

    builder.Services.AddHealthChecks();

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

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}