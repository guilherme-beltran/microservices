using Customers.Api.Core.Events;
using Customers.Api.Core.Shared;

namespace Customers.Api.Core.Customers;

public sealed class Customer : Entity
{
    private Customer() { }
    private Customer(string name, string email)
    {
        Name = name;
        Email = email;
    }
    public string Name { get; set; }
    public string Email { get; set; }

    public static Customer Create(string name, string email)
    {
        Customer customer = new(name: name, email: email);

        //customer.RaiseDomainEvent(new CreateCustomerEvent(Id: customer.Id, Name: customer.Name, CreatedAt: DateTime.Now));

        return customer;
    }
}
