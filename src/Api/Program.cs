using Domain.Entities.Identity;
using Domain.Markers;
using Domain.Settings;
using FluentValidation.AspNetCore;
using Infrastructure.Configuration;
using Infrastructure.Middlewares;
using Infrastructure.PackageConfiguration.FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddIdentity();

builder.Services.AddSwagger();

builder.Services.Configure<GeneralSettings>(builder.Configuration.GetSection(nameof(GeneralSettings)));

builder.Services.AddControllers().AddFluentValidation(fv =>
{
    fv.RegisterAllDtoValidators<IDtoValidator>(Assembly.GetEntryAssembly());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseHsts(app.Environment);

app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();