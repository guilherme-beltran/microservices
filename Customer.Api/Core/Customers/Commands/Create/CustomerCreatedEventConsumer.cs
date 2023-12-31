using Customers.Api.Core.Data;
using MassTransit;

namespace Customers.Api.Core.Customers.Commands.Create;

public class CustomerCreatedEventConsumer : IConsumer<CustomerCreatedEvent>
{
    private readonly ILogger<CustomerCreatedEventConsumer> _logger;
    private readonly CustomerContext _customerContext;

    public CustomerCreatedEventConsumer(ILogger<CustomerCreatedEventConsumer> logger, CustomerContext customerContext)
    {
        _logger = logger;
        _customerContext = customerContext;
    }

    public async Task Consume(ConsumeContext<CustomerCreatedEvent> context)
    {
        var @event = context.Message;

        _logger.LogInformation($"Product created: {context.Message}");

        var customer = Customer.Create(@event.Name, @event.Email);
        _customerContext.Customers.Add(customer);
        await _customerContext.SaveChangesAsync();

    }
}
