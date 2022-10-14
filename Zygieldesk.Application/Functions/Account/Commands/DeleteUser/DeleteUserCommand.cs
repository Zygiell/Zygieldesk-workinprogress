using MediatR;

namespace Zygieldesk.Application.Functions.Account.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<DeleteUserCommandResponse>
    {
        public string Email { get; set; }
    }
}