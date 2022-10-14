using MediatR;

namespace Zygieldesk.Application.Functions.AdminPanel.Commands.UpdateUserDetails
{
    public class UpdateUserDetailsCommand : IRequest<UpdateUserDetailsCommandResponse>
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}