using Customers.Api.Core.Customers;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading;

namespace Customers.Api.Persistence.Customers;

internal sealed class CachedCustomerRepository : ICachedCustomerRepository
{
    private readonly ICustomerRepository _decorated;
    private readonly IDistributedCache _distributedCache;

    public CachedCustomerRepository(ICustomerRepository customerRepository, IDistributedCache distributedCache)
    {
        _decorated = customerRepository;
        _distributedCache = distributedCache;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken)
    {
        string key = $"customers";

        string? cachedCustomers = await _distributedCache.GetStringAsync(key, cancellationToken);
        IEnumerable<Customer> customers;

        if (string.IsNullOrEmpty(cachedCustomers))
        {
            customers = await _decorated.GetAllAsync(cancellationToken);

            if (customers is null)
            {
                return customers;
            }

            await _distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(customers),
                cancellationToken);

            return customers;
        }

        customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(
            cachedCustomers,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });

        return customers;
    }

    public async Task<Customer> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        string key = $"customer-{id}";

        string? cachedCustomer = await _distributedCache.GetStringAsync(key, cancellationToken);
        Customer? customer;

        if (string.IsNullOrEmpty(cachedCustomer))
        {
            customer = await _decorated.GetByIdAsync(id, cancellationToken);

            if (customer is null)
            {
                return customer;
            }

            await _distributedCache.SetStringAsync(
                key, 
                JsonConvert.SerializeObject(customer), 
                cancellationToken);

            return customer;
        }

        customer = JsonConvert.DeserializeObject<Customer>(
            cachedCustomer,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });

        return customer;
    }

}
