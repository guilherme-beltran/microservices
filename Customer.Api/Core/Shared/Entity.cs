using Customers.Api.Core.Events;
using Flunt.Notifications;

namespace Customers.Api.Core.Customers;

public abstract class Entity : Notifiable<Notification>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    protected Entity()
        => Id = Guid.NewGuid();
    public Guid Id { get; }
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get { return _domainEvents; } }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
