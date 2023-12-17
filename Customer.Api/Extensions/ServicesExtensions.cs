using Customers.Api.Core.Data;
using Customers.Api.Persistence.Customers;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Extensions;

public static class ServicesExtensions
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("Database") ?? throw new Exception($"Nenhuma conexão foi definida.");
        string redis = configuration.GetConnectionString("Redis");
        services.AddDbContext<CustomerContext>(options => options.UseSqlServer(connection));
        services.AddStackExchangeRedisCache(redisOptions =>
        {
            redisOptions.Configuration = redis;
        });

        services.AddServices();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICachedCustomerRepository, CachedCustomerRepository>();
    }
}
