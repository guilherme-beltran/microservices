using Microsoft.EntityFrameworkCore;
using Orders.Api.Core.Data;

namespace Orders.Api.Extensions;

public static class ServicesExtensions
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("Database") ?? throw new Exception($"Nenhuma conexão foi definida.");
        services.AddDbContext<OrderContext>(options => options.UseSqlServer(connection));
    }
}
