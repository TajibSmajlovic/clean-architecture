using AutoMapper;
using Kinoteka.TicketManagement.Application.Contracts.Persistence;
using Kinoteka.TicketManagement.Domain.Entities;
using MediatR;

namespace Kinoteka.TicketManagement.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailsQueryHandler : IRequestHandler<GetEventDetailsQuery, EventDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;

        public GetEventDetailsQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository, IAsyncRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<EventDetailsVm> Handle(GetEventDetailsQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetByIdAsync(request.Id);
            var eventDetailsDto = _mapper.Map<EventDetailsVm>(@event);

            var category = await _categoryRepository.GetByIdAsync(@event.CategoryId);

            eventDetailsDto.Category = _mapper.Map<CategoryDto>(category);

            return eventDetailsDto;
        }
    }
}