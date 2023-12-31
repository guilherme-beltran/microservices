using Customers.Api.Core.Customers.Commands.Create;
using Customers.Api.Core.Data;
using Customers.Api.Core.EventBus;
using Customers.Api.Core.Events;
using Customers.Api.Core.MessageBroker;
using Customers.Api.Persistence.Customers;
using Customers.Api.Persistence.UnitOfWork;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Customers.Api.Extensions;

public static class ServicesExtensions
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("Database") ?? throw new Exception($"Nenhuma conexão foi definida.");
        string redis = configuration.GetConnectionString("Redis");
        services.AddDbContext<CustomerContext>(options => options.UseSqlServer(connection));
        services.AddStackExchangeRedisCache(redisOptions =>
        {
            redisOptions.Configuration = redis;
        });

        services.AddHandlers();
        services.AddServices();
        services.AddRabbitMq(configuration);
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICachedCustomerRepository, CachedCustomerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<ICreateCustomerHandler, CreateCustomerHandler>();
    }

    public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEventBus, EventBus>();

        services.Configure<MessageBrokerSettings>(
            configuration.GetSection("MessageBroker"));

        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.AddConsumer<CustomerCreatedEventConsumer>();

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

                configurator.Host(new Uri(settings.Host), host =>
                {
                    host.Username(settings.Username);
                    host.Password(settings.Password);
                });

                configurator.ReceiveEndpoint("customer-created-queue", e =>
                {
                    e.ConfigureConsumer<CustomerCreatedEventConsumer>(context);
                });
            });
        });
    }
}
