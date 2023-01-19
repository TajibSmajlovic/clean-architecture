using Kinoteka.TicketManagement.Application.Contracts.Persistence;
using Kinoteka.TicketManagement.Domain.Entities;
using Kinoteka.TicketManagement.Persistance;
using Kinoteka.TicketManagement.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kinoteka.TicketManagement.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(KinotekaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents)
        {
            var allCategories = await _dbContext.Categories.Include(x => x.Events).ToListAsync();
            if (!includePassedEvents)
            {
                allCategories.ForEach(p => p.Events.ToList().RemoveAll(c => c.Date < DateTime.Today));
            }
            return allCategories;
        }
    }
}