using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Api.Core.Products;

namespace Products.Api.Core.Data;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.Ignore(x => x.Id);
        builder.Ignore(x => x.IsValid);

        builder.HasKey(x => x.ProductId);

        builder.Property(x => x.Name)
               .HasColumnName("Name")
               .HasColumnType("varchar")
               .HasMaxLength(60)
               .IsRequired(true);

        builder.Property(x => x.Value)
               .HasColumnName("Value")
               .HasColumnType("decimal(10,2)")
               .IsRequired(true);
    }
}
