using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Account.Queries.GetUserByEmail;

namespace Zygieldesk.Application.Functions.AdminPanel.Queries.GetAllUseres
{
    public class GetAllUsersQuery : IRequest<List<UserViewModel>>
    {

    }
}
