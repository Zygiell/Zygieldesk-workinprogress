using MediatR;

namespace Zygieldesk.Application.Functions.AdminPanel.Commands.ChangeUserRole
{
    public class ChangeUserRoleCommand : IRequest<ChangeUserRoleCommandResponse>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}