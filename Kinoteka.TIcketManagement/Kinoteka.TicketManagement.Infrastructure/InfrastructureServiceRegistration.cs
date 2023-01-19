using Kinoteka.TicketManagement.Application.Contracts.Infrastructure;
using Kinoteka.TicketManagement.Application.Models.Mail;
using Kinoteka.TicketManagement.Infrastructure.FileExport;
using Kinoteka.TicketManagement.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kinoteka.TicketManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ICsvExporter, CsvExporter>();

            return services;
        }
    }
}