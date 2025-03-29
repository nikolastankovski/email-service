using NxEmailService.Models;

namespace NxEmailService.EmailService;

public interface IMailClient
{
    Task<bool> SendEmailAsync(EmailSetUp emailSetUp);
}
