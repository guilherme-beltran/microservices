using Customers.Api.Core.Events;
using MassTransit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Customers.Api.Core.EventBus;

public sealed class EventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBus(IPublishEndpoint publishEndpoint) 
        => _publishEndpoint = publishEndpoint;

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class 
        => _publishEndpoint.Publish(message, cancellationToken);

    public Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        _publishEndpoint.Publish(domainEvent, cancellationToken);
        //var eventName = domainEvent.GetType().Name;
        //var factory = new ConnectionFactory()
        //{

        //};

        //using var connection = factory.CreateConnection();
        //using var channel = connection.CreateModel();

        //channel.ExchangeDeclare(exchange: "queue-teste",
        //    type: "direct");

        //string message = JsonConvert.SerializeObject(domainEvent);

        //var body = Encoding.UTF8.GetBytes(message);

        //channel.BasicPublish(exchange: "queue-teste",
        //    routingKey: eventName,
        //    basicProperties: null,
        //    body: body);

        return Task.CompletedTask;
    }
}
