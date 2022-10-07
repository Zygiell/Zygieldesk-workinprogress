using MediatR;

namespace Zygieldesk.Application.Functions.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommand : IRequest<DeleteTicketCommandResponse>
    {
        public int TicketId { get; set; }
    }
}