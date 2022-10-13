using AutoMapper;
using Zygieldesk.Application.Functions.Account.Queries.GetUserByEmail;
using Zygieldesk.Application.Functions.Categories.Commands.CreateCategory;
using Zygieldesk.Application.Functions.Categories.Commands.UpdateCategory;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryList;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryWithTickets;
using Zygieldesk.Application.Functions.TicketComments.Commands.CreateTicketComment;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommentsList;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById;
using Zygieldesk.Application.Functions.Tickets.Commands.CreateTicket;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketById;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryListViewModel>();
            CreateMap<Category, CategoryWithTitcketsViewModel>();
            CreateMap<Category, CreatedCategoryCommand>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
            CreateMap<Ticket, CategoryTicketDto>();
            CreateMap<Ticket, TicketListViewModel>();
            CreateMap<Ticket, TicketViewModel>();
            CreateMap<Ticket, CreateTicketCommand>().ReverseMap();
            CreateMap<TicketComment, TicketCommentListViewModel>();
            CreateMap<TicketComment, TicketCommentViewModel>();
            CreateMap<TicketComment, CreateTicketCommentCommand>().ReverseMap();
            CreateMap<User, UserViewModel>()
                .ForMember(m=>m.RoleName, c=>c.MapFrom(s=>s.Role.Name));            
        }
    }
}