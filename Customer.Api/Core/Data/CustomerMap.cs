using Customers.Api.Core.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Api.Core.Data;

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");

        builder.Ignore(x => x.IsValid);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .HasColumnName("Name")
               .HasColumnType("varchar")
               .HasMaxLength(60)
               .IsRequired(true);

        builder.Property(x => x.Email)
               .HasColumnName("Email")
               .HasColumnType("varchar")
               .HasMaxLength(60)
               .IsRequired(true);
    }
}
