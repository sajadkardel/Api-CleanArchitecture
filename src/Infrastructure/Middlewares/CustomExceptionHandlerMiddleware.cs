using Domain.Exceptions;
using Infrastructure.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Middlewares
{
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }

    public class CustomExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly IWebHostEnvironment _env = env;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException exception)
            {
                _logger.LogError(exception, exception.Message);
                await WriteToResponseAsync(exception, exception.HttpStatusCode, exception.AdditionalData);
            }
            catch (SecurityTokenExpiredException exception)
            {
                _logger.LogError(exception, exception.Message);
                await WriteToResponseAsync(exception, HttpStatusCode.Unauthorized);
            }
            catch (UnauthorizedAccessException exception)
            {
                _logger.LogError(exception, exception.Message);
                await WriteToResponseAsync(exception, HttpStatusCode.Unauthorized);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await WriteToResponseAsync(exception, HttpStatusCode.InternalServerError);
            }

            async Task WriteToResponseAsync(Exception exception, HttpStatusCode statusCode, object? additionalData = null)
            {
                if (context.Response.HasStarted)
                    throw new InvalidOperationException("The response has already started, the http status code middleware will not be executed.");

                ApiResult? res = new(false, statusCode);

                if (_env.IsDevelopment())
                {
                    var developmentMessage = new
                    {
                        Message = exception.Message,
                        StackTrace = exception.StackTrace,
                        InnerExceptionMessage = exception.InnerException?.Message,
                        InnerExceptionStackTrace = exception.InnerException?.StackTrace,
                        TokenExpired = exception is SecurityTokenExpiredException tokenException ? tokenException.Expires.ToString() : null,
                        AdditionalData = JsonSerializer.Serialize(additionalData)
                    };

                    res.Message = JsonSerializer.Serialize(developmentMessage, new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    });
                }
                else
                {
                    res.Message = exception.Message;
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;
                var json = JsonSerializer.Serialize(res);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
