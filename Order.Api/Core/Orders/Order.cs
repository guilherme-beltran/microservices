namespace Orders.Api.Core.Orders;

public class Order : Entity
{
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public long CustomerId { get; set; }
    public decimal Quantity { get; set; }
}
