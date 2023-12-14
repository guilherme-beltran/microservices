using Microsoft.EntityFrameworkCore;
using Products.Api.Core.Data;

namespace Products.Api.Extensions;

public static class ServicesExtensions
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("Database") ?? throw new Exception($"Nenhuma conexão foi definida.");
        services.AddDbContext<ProductContext>(options => options.UseSqlServer(connection));
    }
}
