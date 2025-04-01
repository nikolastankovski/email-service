using FluentEmail.Core;
using NxEmailService.Repositories;
using System.Globalization;

namespace NxEmailService;

public class MailClient : IMailClient
{
    private readonly IFluentEmail _email;
    private readonly IEmailHistoryRepository _emailHistoryRepository;

    public MailClient(IFluentEmail email, IEmailHistoryRepository emailHistoryRepository)
    {
        _email = email;
        _emailHistoryRepository = emailHistoryRepository;
    }

    public async Task<bool> SendAsync(MailClientData data)
    {
        EmailHistory emailHistory = new EmailHistory();
        try
        {
            _email.Subject(data.Subject);

            if (data.From is not null && !string.IsNullOrEmpty(data.From.EmailAddress))
                _email.SetFrom(data.From.EmailAddress, string.IsNullOrEmpty(data.From.Name) ? data.From.EmailAddress : data.From.Name);

            _email.To(data.To);

            if (data.CC is not null && data.CC.Any())
                _email.CC(data.CC);

            if (data.BCC is not null && data.BCC.Any())
                _email.BCC(data.BCC);

            if (data.Body is not null)
                _email.Body(body: data.Body.Content, isHtml: data.Body.IsHTML);

            if (data.Attachments is not null && data.Attachments.Any())
                _email.Attach(data.Attachments);

            if (data.Template is not null)
            {
                emailHistory = _email.Data.ToEmailHistory(data.Template.Name);

                var templatePath = Path.Combine(DirectoryPaths.EmailTemplatesPath, $"{data.Template.Name}.cshtml");
                _email.UsingCultureTemplateFromFile(filename: templatePath, model: data.Template.Tokens, culture: data.Culture ?? CultureInfo.InvariantCulture);
            }
            else
                emailHistory = _email.Data.ToEmailHistory();

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