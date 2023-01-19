using Kinoteka.TicketManagement.Application.Contracts.Persistence;
using Kinoteka.TicketManagement.Domain.Entities;
using Kinoteka.TicketManagement.Persistance;
using Kinoteka.TicketManagement.Persistance.Repositories;

namespace Kinoteka.TicketManagement.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(KinotekaDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> IsEventNameAndDateUnique(string name, DateTime eventDate)
        {
            var matches = _dbContext.Events.Any(e => e.Name.Equals(name) && e.Date.Date.Equals(eventDate.Date));
            return Task.FromResult(matches);
        }
    }
}