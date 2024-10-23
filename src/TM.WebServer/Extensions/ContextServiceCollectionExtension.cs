using System;
using TM.Application.Interfaces;
using TM.Application.Repositoryies;
using TM.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TM.WebServer.Extensions
{
    public static class ContextServiceCollectionExtension
    {
        public static IServiceCollection AddOrderContext(this IServiceCollection services, IConfiguration configuration)
        {
            var dbSettings = configuration.GetSection("DbConfig").Get<DbConfig>();

            services.AddDbContextFactory<TMDbContext>(options =>
            {
                switch (dbSettings.Provider)
                {
                    case DbProviders.SQLServer:
                        options.UseSqlServer(dbSettings.ConnectionString);
                        break;
                    case DbProviders.Postgres:
                        options.UseNpgsql(dbSettings.ConnectionString);
                        break;
                    case DbProviders.InMemory:
                        options.UseInMemoryDatabase("InMemoryDatabase");
                        break;
                    default:
                        throw new Exception($"Провайдер {dbSettings.Provider} не определнен.");
                }
            });

            if (dbSettings.Provider == DbProviders.InMemory)
            {
                services.AddScoped<EmployeeRepository>();
                services.AddScoped<IEmployeeRepository, EmployeeRepositoryDecorator>(provider =>
                    new EmployeeRepositoryDecorator(
                        provider.GetRequiredService<EmployeeRepository>())
                );

                services.AddScoped<BuisnessTaskRepository>();
                services.AddScoped<IBuisnessTaskRepository, BuisnessTaskRepositoryDecorator>(provider =>
                    new BuisnessTaskRepositoryDecorator(
                        provider.GetRequiredService<BuisnessTaskRepository>())
                );
            }
            else
            {
                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
                services.AddScoped<IBuisnessTaskRepository, BuisnessTaskRepository>();
            }

            return services;
        }

    }
}
