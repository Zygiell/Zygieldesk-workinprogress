using MediatR;

namespace Zygieldesk.Application.Functions.Account.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UpdateUserCommandResponse>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}