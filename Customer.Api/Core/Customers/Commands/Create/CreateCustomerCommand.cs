using Flunt.Notifications;
using Flunt.Validations;

namespace Customers.Api.Core.Customers.Commands.Create;

public class CreateCustomerCommand : Notifiable<Notification>
{
    public string Name { get; set; }
    public string Email { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(Name, "CreateCustomerCommand.Name", "Name is invalid.")
            .IsNotNullOrWhiteSpace(Email, "CreateCustomerCommand.Email", "Email is invalid.")
            );
    }
}
