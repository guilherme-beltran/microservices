﻿using Customers.Api.Core.Events;
using Flunt.Notifications;

namespace Customers.Api.Core.Customers;

public abstract class Entity : Notifiable<Notification>
{
    private readonly List<IDomainEvent> _events = new();
    protected Entity()
        => Id = Guid.NewGuid();
    public Guid Id { get; }
    public IReadOnlyCollection<IDomainEvent> Events { get { return _events; } }

    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _events.Add(domainEvent);
}
