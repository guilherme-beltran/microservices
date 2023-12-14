namespace Products.Api.Core.Products;

public sealed class Product : Entity
{
    public long ProductId { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
}
