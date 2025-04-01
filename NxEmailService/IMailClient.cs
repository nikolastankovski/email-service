namespace NxEmailService;

public interface IMailClient
{
    Task<bool> SendAsync(MailClientData data);
}
