using Customers.Api.Core.Events;

namespace Customers.Api.Core.Customers;

public sealed class Customer : Entity
{
    private Customer() { }
    private Customer(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public long CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public Customer Create(string name, string email)
    {
        Customer customer = new(name: name, email: email);

        RaiseDomainEvent(new CreateCustomerEvent(Name: customer.Name, CreatedAt: DateTime.UtcNow));

        return customer;
    }
}
