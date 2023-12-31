using MassTransit;

namespace Customers.Api.Core.Customers.Commands.Create;

public class CustomerCreatedEventConsumer : IConsumer<CustomerCreatedEvent>
{
    private readonly ILogger<CustomerCreatedEventConsumer> _logger;

    public CustomerCreatedEventConsumer(ILogger<CustomerCreatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<CustomerCreatedEvent> context)
    {
        _logger.LogInformation("Product created: {@Product}", context.Message);

        return Task.CompletedTask;
    }
}
