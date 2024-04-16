using Application.Dtos.Product;
using Application.Services.Implementations;
using Domain.Constants;
using Domain.Markers;
using Domain.Repositories.Implementations;
using FluentValidation;
using Infrastructure.Configurations;
using Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ConfigurationConst>(builder.Configuration.GetSection(nameof(ConfigurationConst)));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDto>();

#region Register Customized Services
builder.Services.AddCustomDbContext(builder.Configuration);

builder.Services.AddCustomIdentity(builder.Configuration);

builder.Services.AddCustomJwtAuthentication(builder.Configuration);

builder.Services.AddCustomSwagger();
#endregion

#region Register Marked Services
var domainAssembly = typeof(UserRepository).Assembly;
var applicationAssembly = typeof(AuthService).Assembly;

builder.Services.Scan(scan => scan.FromAssemblies(domainAssembly, applicationAssembly)
.AddClasses(classes => classes.AssignableTo<IScopedDependency>())
.AsImplementedInterfaces()
.WithScopedLifetime());

builder.Services.Scan(scan => scan.FromAssemblies(domainAssembly, applicationAssembly)
.AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
.AsImplementedInterfaces()
.WithSingletonLifetime());

builder.Services.Scan(scan => scan.FromAssemblies(domainAssembly, applicationAssembly)
.AddClasses(classes => classes.AssignableTo<ITransientDependency>())
.AsImplementedInterfaces()
.WithTransientLifetime());
#endregion

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();