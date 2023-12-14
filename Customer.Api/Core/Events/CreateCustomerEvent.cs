namespace Customers.Api.Core.Events;

public sealed record CreateCustomerEvent(string Name, DateTime CreatedAt) : IDomainEvent;