using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Orders.Api.Core.Orders;

namespace Orders.Api.Core.Data;

internal sealed class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Notification>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
