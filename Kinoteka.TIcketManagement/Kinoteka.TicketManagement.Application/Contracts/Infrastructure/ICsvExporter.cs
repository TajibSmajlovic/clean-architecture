using Kinoteka.TicketManagement.Application.Features.Events.Queries.GetEventsExport;

namespace Kinoteka.TicketManagement.Application.Contracts.Infrastructure
{
    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos);
    }
}