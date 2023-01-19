using Kinoteka.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kinoteka.TicketManagement.Persistance.Configurations
{
    public class EventCOnfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
        }
    }
}