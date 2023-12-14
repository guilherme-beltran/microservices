using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Api.Core.Orders;

namespace Orders.Api.Core.Data;

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.Ignore(x => x.Id);
        builder.Ignore(x => x.IsValid);

        builder.HasKey(x => x.OrderId);

        builder.Property(x => x.ProductId)
               .HasColumnName("ProductId")
               .IsRequired(true);

        builder.Property(x => x.CustomerId)
               .HasColumnName("CustomerId")
               .IsRequired(true);

        builder.Property(x => x.Quantity)
               .HasColumnName("Quantity")
               .IsRequired(true);
    }
}
