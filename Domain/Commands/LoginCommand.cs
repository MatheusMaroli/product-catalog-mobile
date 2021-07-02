

using Domain.Commands.Contracts;

namespace Domain.Commands
{
    public class LoginCommand : CommandsBehaviors.CommandValidatorContext, ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public override void Validate()
        {
            NotificationEmptyString(Email, "Email", "Email não informado");
            NotificationEmptyString(Password, "Password", "Password não informado");
        }
    }
}
