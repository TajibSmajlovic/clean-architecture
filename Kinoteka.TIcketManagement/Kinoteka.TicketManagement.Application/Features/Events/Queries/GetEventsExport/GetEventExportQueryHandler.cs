using AutoMapper;
using Kinoteka.TicketManagement.Application.Contracts.Infrastructure;
using Kinoteka.TicketManagement.Application.Contracts.Persistence;
using Kinoteka.TicketManagement.Domain.Entities;
using MediatR;

namespace Kinoteka.TicketManagement.Application.Features.Events.Queries.GetEventsExport
{
    public class GetEventExportQueryHandler : IRequestHandler<GetEventExportQuery, EventExportFileVm>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly ICsvExporter _csvExporter;

        public GetEventExportQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository, ICsvExporter csvExporter)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _csvExporter = csvExporter;
        }

        public async Task<EventExportFileVm> Handle(GetEventExportQuery request, CancellationToken cancellationToken)
        {
            var allEvents = _mapper.Map<List<EventExportDto>>((await _eventRepository.ListAllAsync()).OrderBy(x => x.Date));

            var fileData = _csvExporter.ExportEventsToCsv(allEvents);

            return new EventExportFileVm
            {
                ContentType = "text/csv",
                Data = fileData,
                EventExportFileName = $"{Guid.NewGuid()}.csv"
            };
        }
    }
}