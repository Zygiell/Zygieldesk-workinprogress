namespace Zygieldesk.Application.Functions.Categories.Queries.GetCategoryWithTickets
{
    public class CategoryWithTitcketsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CategoryTicketDto> Tickets { get; set; }
    }
}