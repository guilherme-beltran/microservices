using Customers.Api.Core.Events;

namespace Customers.Api.Core.EventBus;

public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
    Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}
