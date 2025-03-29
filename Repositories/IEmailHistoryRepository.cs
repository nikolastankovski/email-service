using NxEmailService.Models;

namespace NxEmailService.Repositories;

public interface IEmailHistoryRepository 
{
    Task<EmailHistory> CreateAsync(EmailHistory entity);
    Task UpdateAsync(EmailHistory entity);
}
