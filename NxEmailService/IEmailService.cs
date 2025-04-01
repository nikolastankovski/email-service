using FluentResults;

namespace NxEmailService
{
    public interface IEmailService
    {
        Task<Result<string>> SendEmailAsync(SendEmailRequest request);
    }
}
