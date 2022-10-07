using MediatR;

namespace Zygieldesk.Application.Functions.Account.Commands.AddUser
{
    public class CreateUserCommand : IRequest<CreateUserCommandResponse>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}