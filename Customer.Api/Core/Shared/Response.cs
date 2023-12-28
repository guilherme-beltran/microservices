using Flunt.Notifications;
using System.Net;

namespace Customers.Api.Core.Shared;

public class Response
{
    private Response(bool isSuccess, Error error, object? obj = null, string? message = null)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Message = message;
        Obj = obj;
        Error = error;
    }

    public bool IsSuccess { get; }
    public string Message { get; }
    public object Obj { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Response Sucess(string? message = null) => new(isSuccess: true, error: Error.None, message: message);
    public static Response Failure(Error error) => new(isSuccess: false, error: error);
    public static Response NotFound(Error error) => new(isSuccess: false, error: error);
}
