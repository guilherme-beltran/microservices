using Customers.Api.Core.Customers;
using Customers.Api.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Persistence.Customers;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly CustomerContext _context;

    public CustomerRepository(CustomerContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken) 
        => await _context
                .Customers
                .ToListAsync(cancellationToken);

    public async Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
        => await _context
                .Customers
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

    public async Task Insert(Customer customer, CancellationToken cancellationToken) 
        => await _context.Customers.AddAsync(customer, cancellationToken);
}
