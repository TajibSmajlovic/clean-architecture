using Kinoteka.TicketManagement.Domain.Entities;

namespace Kinoteka.TicketManagement.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
    }
}