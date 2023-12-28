using Customers.Api.Core.Shared;

namespace Customers.Api.Core.Customers.Commands.Create;

public interface ICreateCustomerHandler
{
    Task<Response> Handle(CreateCustomerCommand command, CancellationToken cancellationToken);
}
