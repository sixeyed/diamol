using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using ToDoList.Model;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddToDoContext(this IServiceCollection services, IConfiguration config, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var dbProvider = config.GetValue<DbProvider>("Database:Provider", DbProvider.Postgres);
            _ = dbProvider switch
            {
                DbProvider.Postgres => services.AddDbContext<ToDoContext>(options =>
                     options.UseNpgsql(config.GetConnectionString("ToDoDb"),
                     postgresOptions => postgresOptions.EnableRetryOnFailure()), 
                     lifetime),

                _ => throw new NotSupportedException("Supported providers: Posgtres")
            };
            return services;
        }
    }
}
