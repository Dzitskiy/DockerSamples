using System;
using Advertisement.Application.Repositories;
using Advertisement.Domain;
using Advertisement.Infrastructure.DataAccess;
using Advertisement.Infrastructure.DataAccess.Repositories;
//using Advertisement.Infrastructure.Migrations;
//using Advertisement.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InMemoryRepository = Advertisement.Infrastructure.DataAccess.Repositories.InMemoryRepository;

namespace Advertisement.Infrastructure
{
    public static class DataAccessModule
    {
        public sealed class ModuleConfiguration
        {
            public IServiceCollection Services { get; init; }
        }

        public static IServiceCollection AddDataAccessModule(
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

        public static void InMemory(this ModuleConfiguration moduleConfiguration)
        {
            moduleConfiguration.Services.AddSingleton(new InMemoryRepository());
            
            moduleConfiguration.Services.AddSingleton<IRepository<User, int>>(sp => sp.GetService<InMemoryRepository>());
            moduleConfiguration.Services.AddSingleton<IRepository<Ad, int>>(sp => sp.GetService<InMemoryRepository>());
        }

        public static void InSqlServer(this ModuleConfiguration moduleConfiguration, string connectionString)
        {
            moduleConfiguration.Services.AddDbContextPool<DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString, builder =>
                    builder.MigrationsAssembly(
                        typeof(DataAccessModule).Assembly.FullName)
                       // typeof(DatabaseContextModelSnapshot).Assembly.FullName)
                );
            });

            moduleConfiguration.Services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
            moduleConfiguration.Services.AddScoped<IAdRepository, AdRepository>();
        }

        public static void InPostgress(this ModuleConfiguration moduleConfiguration, string connectionString)
        {
            moduleConfiguration.Services.AddDbContextPool<DatabaseContext>(options =>
            {
                options.UseNpgsql(connectionString, builder =>
                    builder.MigrationsAssembly(
                typeof( DataAccessModule).Assembly.FullName)
                //typeof(DatabaseContextModelSnapshot).Assembly.FullName)
                );
            });

            moduleConfiguration.Services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
            moduleConfiguration.Services.AddScoped<IAdRepository, AdRepository>();

            //moduleConfiguration.Services.AddScoped<IRepository<Ad, int>, EfRepository<Ad, int>>();
            //moduleConfiguration.Services.AddScoped<IRepository<User, int>, EfRepository<User, int>>();
        }
    }
}