using CsvHelper;
using Kinoteka.TicketManagement.Application.Contracts.Infrastructure;
using Kinoteka.TicketManagement.Application.Features.Events.Queries.GetEventsExport;

namespace Kinoteka.TicketManagement.Infrastructure.FileExport
{
    public class CsvExporter : ICsvExporter
    {
        public byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter);
                csvWriter.WriteRecord(eventExportDtos);
            }

            return memoryStream.ToArray();
        }
    }
}