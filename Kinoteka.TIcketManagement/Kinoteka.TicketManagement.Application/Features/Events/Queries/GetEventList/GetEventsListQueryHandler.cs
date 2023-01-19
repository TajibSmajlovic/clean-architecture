using AutoMapper;
using Kinoteka.TicketManagement.Application.Contracts.Persistence;
using Kinoteka.TicketManagement.Domain.Entities;
using MediatR;

namespace Kinoteka.TicketManagement.Application.Features.Events.Queries.GetEventList
{
    public class GetEventsListQueryHandler : IRequestHandler<GetEventsListQuery, List<EventListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Event> _eventRepository;

        public GetEventsListQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<List<EventListVm>> Handle(GetEventsListQuery request, CancellationToken cancellationToken)
        {
            var allEvents = (await _eventRepository.ListAllAsync()).OrderBy(x => x.Date);

            return _mapper.Map<List<EventListVm>>(allEvents);
        }
    }
}