using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Persistance.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(x => x.TicketTitle)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.TicketBody)
                .IsRequired();
        }
    }
}