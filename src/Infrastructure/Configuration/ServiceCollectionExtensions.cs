using Domain.Contexts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Infrastructure.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });
    }

    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddIdentityApiEndpoints<User>(config =>
        {
            config.SignIn = new SignInOptions
            {
                RequireConfirmedEmail = true
            };

            config.User = new UserOptions
            {
                RequireUniqueEmail = true
            };

        }).AddEntityFrameworkStores<ApplicationDbContext>();
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options => {

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
    }
}
