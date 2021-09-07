using Advertisement.Application.Services.Ad.Implementations;
using Advertisement.Application.Services.Ad.Interfaces;
using Advertisement.Application.Services.User.Implementations;
using Advertisement.Application.Services.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Advertisement.Infrastructure
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<IAdService, AdServiceV1>();
            services.AddScoped<IUserService, UserServiceV1>();

            return services;
        }
        
    }
}