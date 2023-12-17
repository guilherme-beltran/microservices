using Customers.Api.Persistence.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Extensions.Endpoints;

public static class CustomerEndpoints
{
    public static void AddCustomerRoutes(this RouteGroupBuilder route)
    {
        var customerRoute = route.MapGroup("customers");
        customerRoute.MapGet("/{id}", async ([FromServices] ICachedCustomerRepository services, [FromRoute] long id, CancellationToken cancellationToken) =>
        {
            var customer = await services.GetByIdAsync(id, cancellationToken);

            return Results.Ok(customer);

        });

        customerRoute.MapGet("/", async ([FromServices] ICachedCustomerRepository services, CancellationToken cancellationToken) =>
        {
            var customer = await services.GetAllAsync(cancellationToken);

            return Results.Ok(customer);

        });
    }
}
