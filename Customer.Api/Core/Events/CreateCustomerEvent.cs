namespace Customers.Api.Core.Events;

public sealed record CreateCustomerEvent(Guid Id, string Name, DateTime CreatedAt) : IDomainEvent;