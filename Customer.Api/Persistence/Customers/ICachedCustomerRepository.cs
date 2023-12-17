using Customers.Api.Core.Customers;

namespace Customers.Api.Persistence.Customers;

public interface ICachedCustomerRepository
{
    Task<Customer> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken);
}
