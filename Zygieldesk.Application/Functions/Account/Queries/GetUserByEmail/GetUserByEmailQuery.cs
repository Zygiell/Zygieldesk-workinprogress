using MediatR;

namespace Zygieldesk.Application.Functions.Account.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<UserViewModel>
    {
        public string Email { get; set; }
    }
}