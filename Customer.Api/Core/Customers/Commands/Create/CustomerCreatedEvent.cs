using Customers.Api.Core.Events;

namespace Customers.Api.Core.Customers.Commands.Create;

public sealed record CustomerCreatedEvent(
    Guid Id,
    string Name,
    string Email,
    DateTime CreatedAt) : IDomainEvent;