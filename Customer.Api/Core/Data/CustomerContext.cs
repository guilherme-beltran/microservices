using Customers.Api.Core.Customers;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Core.Data;

public sealed class CustomerContext : DbContext
{
    public CustomerContext() { }
    public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Notification>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
