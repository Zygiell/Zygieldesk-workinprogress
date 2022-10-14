using MediatR;
using Zygieldesk.Application.Functions.Account.Queries.GetUserByEmail;

namespace Zygieldesk.Application.Functions.AdminPanel.Queries.GetAllUseres
{
    public class GetAllUsersQuery : IRequest<List<UserViewModel>>
    {
    }
}