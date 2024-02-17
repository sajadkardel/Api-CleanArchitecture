using Domain.Utilities;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHsts(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            Assert.NotNull(app, nameof(app));
            Assert.NotNull(env, nameof(env));

            if (!env.IsDevelopment())
                app.UseHsts();

            return app;
        }
    }
}
