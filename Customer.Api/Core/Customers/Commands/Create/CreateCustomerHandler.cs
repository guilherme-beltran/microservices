namespace Customers.Api.Core.Customers.Commands.Create;

public class CreateCustomerHandler : ICreateCustomerHandler
{
    public async Task<Response> Handle(CreateCustomerCommand command)
    {
        return Response.Sucess();
    }
}
