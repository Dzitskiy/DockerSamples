using System;
using Advertisement.Application;
using Advertisement.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Advertisement.Infrastructure
{
    public static class InfrastructureModule
    {
        public sealed class ModuleConfiguration
        {
            public IServiceCollection Services { get; init; }
        }
        
        public static IServiceCollection AddInfrastructureModule(
            this IServiceCollection services,
            Action<ModuleConfiguration> action
        )
        {
            var moduleConfiguration = new ModuleConfiguration
            {
                Services = services
            };
            action(moduleConfiguration);
            return services;
        }
        
        public static void IdentityFromHttpContext(this ModuleConfiguration moduleConfiguration)
        {
            moduleConfiguration.Services.AddScoped<IClaimsAccessor, HttpContextClaimsAccessor>();
        }
    }
}