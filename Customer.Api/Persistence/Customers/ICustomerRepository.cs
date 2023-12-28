using Customers.Api.Core.Customers;

namespace Customers.Api.Persistence.Customers;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken);
    Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task Insert(Customer customer, CancellationToken cancellationToken);
}
