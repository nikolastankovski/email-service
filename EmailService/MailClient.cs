using FluentEmail.Core;
using NxEmailService.ExtensionMethods;
using NxEmailService.Extensions;
using NxEmailService.Repositories;

namespace NxEmailService.EmailService;

public class MailClient : IMailClient
{
    private readonly IFluentEmail _email;
    private readonly IEmailHistoryRepository _emailHistoryRepository;

    public MailClient(IFluentEmail email, IEmailHistoryRepository emailHistoryRepository)
    {
        _email = email;
        _emailHistoryRepository = emailHistoryRepository;
    }

    public async Task<bool> SendEmailAsync(EmailSetUp es)
    {
        EmailHistory emailHistory = new EmailHistory();
        try
        {
            _email.Subject(es.Subject);

            if (es.From is not null)
                _email.SetFrom(es.From.EmailAddress, es.From.Name);

            _email.To(es.To.EmailAddress);

            if (es.CC.Any())
                _email.CC(es.CC);

            if (es.BCC.Any())
                _email.BCC(es.BCC);

            _email.UsingCultureTemplateFromFile(es.EmailTemplate, es.Tokens, es.Culture);

            if (es.Attachments.Any())
                _email.Attach(es.Attachments);

            emailHistory = _email.Data.ToEmailHistory(es.EmailTemplate);

            var result = await _email.SendAsync();

            emailHistory.IsSent = result.Successful;
            emailHistory = await _emailHistoryRepository.CreateAsync(emailHistory);

            if (!result.Successful)
                Log.Error(messageTemplate: "Error while sending e-mail! Errors: " + string.Join("; ", result.ErrorMessages));

            return result.Successful;
        }
        catch (Exception e)
        {
            Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());

            emailHistory.IsSent = false;
            await _emailHistoryRepository.CreateAsync(emailHistory);

            return false;
        }
    }
}