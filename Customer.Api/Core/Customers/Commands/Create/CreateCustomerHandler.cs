using Azure.Core;
using Customers.Api.Core.Shared;
using Customers.Api.Persistence.Customers;
using Customers.Api.Persistence.UnitOfWork;
using MediatR;

namespace Customers.Api.Core.Customers.Commands.Create;

public class CreateCustomerHandler : ICreateCustomerHandler
{
    private readonly ICachedCustomerRepository _cachedRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerHandler(ICachedCustomerRepository cachedRepository, IUnitOfWork unitOfWork)
    {
        _cachedRepository = cachedRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        #region Validate request

        try
        {
            command.Validate();
            if (!command.IsValid)
                return CustomerErrors.SendNotifications(notifications: command.Notifications);
        }
        catch (NullReferenceException)
        {
            return CustomerErrors.ReturnNullReference("CreateCustomerHandler");
        }
        catch (Exception ex)
        {
            return CustomerErrors.Exception("CreateCustomerHandler.command.Validate", $"Request failed. Please try again later. Details: {ex.Message}");
        }

        #endregion

        #region Add customer

        var customer = Customer.Create(
            name: command.Name,
            email: command.Email);

        try
        {
            _unitOfWork.BeginTransaction();

            await _cachedRepository.Insert(customer, cancellationToken);

            var hasCommited = await _unitOfWork.Commit(cancellationToken);

            if (hasCommited == 0)
            {
                return CustomerErrors.Failure("CreateCustomerHandler.Commit", "We had an internal failure when saving the data. Please try again later.");
            }

            return Response.Sucess($"Custome successfully created.");

        }
        catch (Exception ex)
        {
            return CustomerErrors.Exception("CreateCustomerHandler.Commit", $"There was an internal failure. Please try again later.. Please try again later. Details: {ex.Message}");
        }
        finally
        {
            _unitOfWork.Dispose();
        }

        #endregion
    }
}
