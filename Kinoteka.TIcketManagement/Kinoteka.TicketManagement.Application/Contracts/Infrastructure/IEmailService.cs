using Kinoteka.TicketManagement.Application.Models.Mail;

namespace Kinoteka.TicketManagement.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}