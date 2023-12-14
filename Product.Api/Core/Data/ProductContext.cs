using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Products.Api.Core.Products;

namespace Products.Api.Core.Data;

internal sealed class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Notification>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
