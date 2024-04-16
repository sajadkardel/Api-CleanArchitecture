using Domain.Constants;
using Domain.Contexts;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Domain.Markers;
using Domain.Repositories.Contracts;
using Domain.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Configurations;

public static class ServiceCollectionExtensions
{
    public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("MainDB"));
        });
    }

    public static void AddCustomIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        IdentityConfigurationConst config = configuration.GetSection($"{nameof(ConfigurationConst)}:{nameof(IdentityConfigurationConst)}").Get<IdentityConfigurationConst>() ?? new();

        services.AddIdentity<User, Role>(identityOptions =>
        {
            //Password Settings
            identityOptions.Password.RequireDigit = config.PasswordRequireDigit;
            identityOptions.Password.RequiredLength = config.PasswordRequiredLength;
            identityOptions.Password.RequireNonAlphanumeric = config.PasswordRequireNonAlphanumeric; //#@!
            identityOptions.Password.RequireUppercase = config.PasswordRequireUppercase;
            identityOptions.Password.RequireLowercase = config.PasswordRequireLowercase;

            //UserName Settings
            identityOptions.User.RequireUniqueEmail = config.RequireUniqueEmail;

            //Singin Settings
            //identityOptions.SignIn.RequireConfirmedEmail = false;
            //identityOptions.SignIn.RequireConfirmedPhoneNumber = false;

            //Lockout Settings
            //identityOptions.Lockout.MaxFailedAccessAttempts = 5;
            //identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //identityOptions.Lockout.AllowedForNewUsers = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    }

    public static void AddCustomJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        JwtConfigurationConst config = configuration.GetSection($"{nameof(ConfigurationConst)}:{nameof(JwtConfigurationConst)}").Get<JwtConfigurationConst>() ?? new();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            var secretKey = Encoding.UTF8.GetBytes(config.SecretKey);
            var encryptionKey = Encoding.UTF8.GetBytes(config.EncryptKey);

            options.RequireHttpsMetadata = false;
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero, // default: 5 min
                RequireSignedTokens = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ValidateAudience = true, //default : false
                ValidAudience = config.Audience,

                ValidateIssuer = true, //default : false
                ValidIssuer = config.Issuer,

                TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception is not null) 
                        throw new AppException(HttpStatusCode.Unauthorized, "Authentication failed.", context.Exception);

                    return Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    //var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                    var _userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();

                    var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;
                    if (claimsIdentity?.Claims?.Any() is false)
                    {
                        context.Fail("This token has no claims.");
                        return;
                    }

                    //var securityStamp = claimsIdentity.FindFirst(new ClaimsIdentityOptions().SecurityStampClaimType);
                    //if (securityStamp is null) context.Fail("This token has no security stamp");

                    //Find user and token from database and perform your custom validation
                    var userId = claimsIdentity.GetUserId();
                    if (string.IsNullOrEmpty(userId))
                    {
                        context.Fail("User not found.");
                        return;
                    }

                    var user = await _userManager.FindByIdAsync(userId);
                    if (user is null)
                    {
                        context.Fail("User not found."); 
                        return;
                    }
                    if (user.IsActive is false)
                    {
                        context.Fail("User is not active.");
                        return;
                    }

                    //if (user.SecurityStamp != Guid.Parse(securityStamp)) context.Fail("Token security stamp is not valid.");

                    //var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                    //if (validatedUser == null) context.Fail("Token security stamp is not valid.");

                    //await userRepository.UpdateLastLoginDateAsync(user, context.HttpContext.RequestAborted);
                },
                OnChallenge = context =>
                {
                    if (context.AuthenticateFailure != null) 
                        throw new AppException(HttpStatusCode.Unauthorized, "Authenticate failure.", context.AuthenticateFailure);
                    
                    throw new AppException(HttpStatusCode.Unauthorized, "You are unauthorized to access this resource.");
                }
            };
        });
    }

    public static void AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
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
