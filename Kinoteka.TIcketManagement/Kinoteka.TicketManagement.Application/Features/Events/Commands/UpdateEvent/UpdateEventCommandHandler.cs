using AutoMapper;
using Kinoteka.TicketManagement.Application.Contracts.Persistence;
using Kinoteka.TicketManagement.Application.Exceptions;
using Kinoteka.TicketManagement.Domain.Entities;
using MediatR;

namespace Kinoteka.TicketManagement.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IMapper mapper, IAsyncRepository<Event> eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventToUpdate = await _eventRepository.GetByIdAsync(request.EventId);
            if (eventToUpdate == null)
            {
                throw new NotFoundException(nameof(Event), request.EventId);
            }

            var validator = new UpdateEventCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, eventToUpdate, typeof(UpdateEventCommand), typeof(Event));

            await _eventRepository.UpdateAsync(eventToUpdate);

            return Unit.Value;
        }
    }
}