using MediatR;

namespace Kinoteka.TicketManagement.Application.Features.Events.Queries.GetEventsExport
{
    public class GetEventExportQuery : IRequest<EventExportFileVm>
    {
    }
}