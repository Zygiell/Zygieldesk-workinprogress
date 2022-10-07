using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Persistance.Configuration
{
    public class TicketCommentConfiguration : IEntityTypeConfiguration<TicketComment>
    {
        public void Configure(EntityTypeBuilder<TicketComment> builder)
        {
            builder.Property(x => x.CommentBody)
                .IsRequired();
        }
    }
}