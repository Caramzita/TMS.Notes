using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TMS.Tasks.DataAccess;
using TMS.Tasks.DataAccess.Repositories;
using TMS.Tasks.UseCases;
using TMS.Tasks.UseCases.Abstractions;
using TMS.Tasks.UseCases.Commands.AddNote;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TaskDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
    });

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile(typeof(MappingProfile)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AddNoteCommand).Assembly));

builder.Services.AddValidatorsFromAssemblyContaining<AddNoteCommandValidator>();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
