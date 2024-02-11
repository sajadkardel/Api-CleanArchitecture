using Domain.Context;
using Domain.Markers;
using Domain.Settings;
using Domain.Utilities;
using FluentValidation.AspNetCore;
using Infrastructure.PackageConfiguration.FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;

namespace Infrastructure.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            //Tips
            //Automatic client evaluation is no longer supported. This event is no longer generated.
            //This line is no longer needed.
            //.ConfigureWarnings(warning => warning.Throw(RelationalEventId.QueryClientEvaluationWarning));
        });
    }

    public static void AddMinimalMvc(this IServiceCollection services)
    {
        services.AddControllers().AddFluentValidation(fv =>
        {
            fv.RegisterAllDtoValidators<IDtoValidator>(Assembly.GetEntryAssembly());
        });
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        Assert.NotNull(services, nameof(services));

        //Add services and configuration to use swagger
        services.AddSwaggerGen(options =>
        {
            #region Add Jwt Authentication
            //Add Lockout icon on top of swagger ui page to authenticate
            //OAuth2Scheme
            options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri("/api/v1/users/Token", UriKind.Relative),
                        //AuthorizationUrl = new Uri("/api/v1/users/Token", UriKind.Relative)
                        //Scopes = new Dictionary<string, string>
                        //{
                        //    { "readAccess", "Access read operations" },
                        //    { "writeAccess", "Access write operations" }
                        //}
                    }
                }
            });
            #endregion
        });
    }
}
