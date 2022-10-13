using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.AdminPanel.Commands.ChangeUserRole
{
    public class ChangeUserRoleCommand : IRequest<ChangeUserRoleCommandResponse>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
