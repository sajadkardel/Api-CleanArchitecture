using Domain.Dtos.Product;
using Domain.Entities.Identity;
using Domain.Settings;
using FluentValidation;
using Infrastructure.Configuration;
using Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddIdentity();

builder.Services.AddSwagger();

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDto>();

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