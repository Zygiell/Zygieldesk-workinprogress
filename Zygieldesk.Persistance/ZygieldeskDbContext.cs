using Microsoft.EntityFrameworkCore;
using Zygieldesk.Application.Services;
using Zygieldesk.Domain.Common;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Persistance
{
    public class ZygieldeskDbContext : DbContext
    {
        private readonly IUserContextService _userContextService;

        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ZygieldeskDbContext(DbContextOptions<ZygieldeskDbContext> options, IUserContextService userContextService) : base(options)
        {
            _userContextService = userContextService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedByUserId = _userContextService.GetUserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedByUserId = _userContextService.GetUserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}