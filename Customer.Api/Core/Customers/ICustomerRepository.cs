namespace Customers.Api.Core.Customers;

public interface ICustomerRepository
{
    Task<Customer> GetAllAsync(CancellationToken cancellationToken);
    Task<Customer> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<Customer> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
