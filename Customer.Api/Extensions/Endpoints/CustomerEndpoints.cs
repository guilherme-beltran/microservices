using Customers.Api.Core.Customers.Commands.Create;
using Customers.Api.Persistence.Customers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Customers.Api.Extensions.Endpoints;

public static class CustomerEndpoints
{
    public static void AddCustomerRoutes(this RouteGroupBuilder route)
    {
        var customerRoute = route.MapGroup("customers");
        customerRoute.MapGet("/{id}", async ([FromServices] ICachedCustomerRepository services, [FromRoute] Guid id, CancellationToken cancellationToken) =>
        {
            var customer = await services.GetByIdAsync(id, cancellationToken);

            return Results.Ok(customer);

        });

        customerRoute.MapGet("/", async ([FromServices] ICachedCustomerRepository services, CancellationToken cancellationToken) =>
        {
            var customer = await services.GetAllAsync(cancellationToken);

            return Results.Ok(customer);

        });

        customerRoute.MapPost("/", async ([FromServices] ICreateCustomerHandler handler, [FromBody] CreateCustomerCommand command, CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(command, cancellationToken);

            if (response.IsSuccess == false)
            {
                if (response.Error.StatusCode == HttpStatusCode.BadRequest)
                    Results.BadRequest(response);

                if (response.Error.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var problemDetails = new ProblemDetails
                    {
                        Status = (int)response.Error.StatusCode,
                        Title = response.Error.Message,
                        Detail = response.Error.Notifications.ToString()
                    };

                    Results.Problem(problemDetails);
                }
            }

            return Results.Ok(response);

        });
    }
}
