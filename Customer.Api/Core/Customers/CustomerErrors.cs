using Flunt.Notifications;
using System.Net;

namespace Customers.Api.Core.Customers;

public static class CustomerErrors
{
    public static readonly Error InvalidRequest = new("UserError.InvalidRequest", HttpStatusCode.BadRequest, "Invalid request. Please validate the data entered");
    public static readonly Error NullReference = new("UserError.NullReference", HttpStatusCode.BadRequest, "Invalid request. Please validate the data entered");
    public static readonly Error ExceptionResult = new("UserError.ExceptionResult", HttpStatusCode.InternalServerError);

    public static Error SendNotifications(IReadOnlyCollection<Notification>? notifications)
        => new("UserError.InvalidRequest", HttpStatusCode.BadRequest, "Invalid request. Please validate the data entered", notifications);

    public static Error ReturnNullReference(string key)
       => new(key, HttpStatusCode.BadRequest, "Invalid request. Please validate the data entered");

    public static Error NotFound(string key, string search)
       => new(key, HttpStatusCode.BadRequest, $"{search} was not found.");

    public static Error Exception(string key, string message)
       => new(key, HttpStatusCode.InternalServerError, message);
}
