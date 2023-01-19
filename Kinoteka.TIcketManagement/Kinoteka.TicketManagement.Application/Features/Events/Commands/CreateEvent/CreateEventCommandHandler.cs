using AutoMapper;
using Kinoteka.TicketManagement.Application.Contracts.Infrastructure;
using Kinoteka.TicketManagement.Application.Contracts.Persistence;
using Kinoteka.TicketManagement.Application.Models.Mail;
using Kinoteka.TicketManagement.Domain.Entities;
using MediatR;

namespace Kinoteka.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, IEmailService emailService)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _emailService = emailService;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var @event = _mapper.Map<Event>(request);

            @event = await _eventRepository.AddAsync(@event);

            var email = new Email
            {
                To = "raif.rojf@mail.com",
                Body = $"The new event has been created: ${request}",
                Subject = "New event created"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
            }

            return @event.EventId;
        }
    }
}