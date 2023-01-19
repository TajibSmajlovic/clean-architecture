using MediatR;

namespace Kinoteka.TicketManagement.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailsQuery : IRequest<EventDetailsVm>
    {
        public Guid Id { get; set; }
    }
}