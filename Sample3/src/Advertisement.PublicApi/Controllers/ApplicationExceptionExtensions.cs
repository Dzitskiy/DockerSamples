using System;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Advertisement.PublicApi.Controllers
{
    public static class ApplicationExceptionExtensions
    {
        public static void UseApplicationException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApplicationExceptionHandler>();
        }

        public static void AddApplicationException(this IServiceCollection services,
            Action<ApplicationExceptionOptions> setupAction = null)
        {
            services.Configure(setupAction);
        }
    }
}