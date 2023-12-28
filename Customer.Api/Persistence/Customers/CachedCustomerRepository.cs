using Customers.Api.Core.Customers;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

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

            var cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4)
            };

            await _distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(customers),
                cacheEntryOptions,
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

    public async Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken)
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

            var cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4)
            };

            await _distributedCache.SetStringAsync(
                key, 
                JsonConvert.SerializeObject(customer),
                cacheEntryOptions,
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

    public async Task Insert(Customer customer, CancellationToken cancellationToken)
    {
        string key = $"customer-{customer.Id}";

        var cacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4)
        };

        await Task.WhenAll(
            _decorated.Insert(customer, cancellationToken),

            _distributedCache.SetStringAsync(
            key,
            JsonConvert.SerializeObject(customer),
            cacheEntryOptions,
            cancellationToken)
            );
    }
}
