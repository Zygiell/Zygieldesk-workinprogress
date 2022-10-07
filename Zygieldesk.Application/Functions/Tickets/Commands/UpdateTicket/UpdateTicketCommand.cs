using MediatR;

namespace Zygieldesk.Application.Functions.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketCommand : IRequest<UpdateTicketCommandResponse>
    {
        public int TicketId { get; set; }
        public string TicketTitle { get; set; }
        public string TicketBody { get; set; }
    }
}