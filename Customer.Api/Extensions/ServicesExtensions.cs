using Customers.Api.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Extensions;

public static class ServicesExtensions
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("Database") ?? throw new Exception($"Nenhuma conexão foi definida.");

        services.AddDbContext<CustomerContext>(options => options.UseSqlServer(connection));
    }
}
