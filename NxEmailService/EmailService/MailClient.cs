using FluentEmail.Core;
using FluentEmail.Core.Models;
using NxEmailService.ExtensionMethods;
using NxEmailService.Extensions;
using NxEmailService.Repositories;
using System.Globalization;

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
                _email.SetFrom(es.From.EmailAddress, string.IsNullOrEmpty(es.From.Name) ? es.From.EmailAddress : es.From.Name);

            _email.To(es.To.EmailAddress);

            var test = new Address("nikola");

            if (es.CC.Any())
                _email.CC(es.CC);

            if (es.BCC.Any())
                _email.BCC(es.BCC);

            if (es.Body is not null)
                _email.Body(body: es.Body.Content, isHtml: es.Body.IsHTML);

            if (es.Attachments.Any())
                _email.Attach(es.Attachments);

            emailHistory = _email.Data.ToEmailHistory(es.EmailTemplate);

            var templatePath = Path.Combine(DirectoryPaths.EmailTemplatesPath, $"{es.EmailTemplate}.cshtml");

            if(!string.IsNullOrEmpty(es.EmailTemplate) && !File.Exists(templatePath))
            {
                throw new Exception($"Template not found! Template: {es.EmailTemplate}");
            }

            if (File.Exists(templatePath))
            {
                var culture = string.IsNullOrEmpty(es.LanguageTwoLetterIsoCode) ? CultureInfo.InvariantCulture : CultureInfo.GetCultureInfo(es.LanguageTwoLetterIsoCode);
                _email.UsingCultureTemplateFromFile(filename: templatePath, model: es.Tokens, culture: culture);
            }

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