using RabbitMQ.Client;

namespace Customers.Api.Core.MessageBroker;

public sealed class Producer
{
    private readonly ConnectionFactory _factory;
    public Producer(IConfiguration configuration)
    {
        _factory = new ConnectionFactory()
        {
            
        };
    }
}
