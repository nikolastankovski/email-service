using FluentEmail.Core.Models;
using FluentResults;
using System.Globalization;

namespace NxEmailService
{
    public class EmailService : IEmailService
    {
        private readonly IMailClient _mailClient;

        public EmailService(IMailClient mailClient)
        {
            _mailClient = mailClient;
        }

        public async Task<Result<string>> SendEmailAsync(SendEmailRequest request)
        {
            MailClientData mailClientData = new MailClientData();

            try
            {
                if (!string.IsNullOrEmpty(request.From))
                {
                    if (!Helper.IsEmailValid(request.From))
                        throw new InvalidEmailDataException($"The provided \"From\" address is invalid. Address: {{{request.From}}}");

                    mailClientData.From = new Address(request.From);
                }


                foreach (var address in request.To)
                {
                    if (!string.IsNullOrEmpty(address) && !Helper.IsEmailValid(address))
                        throw new InvalidEmailDataException($"The provided \"To\" address is invalid. Address: {{{address}}}");

                    mailClientData.To.Add(new Address(address));
                }

                if (request.CC is not null && request.CC.Any())
                {
                    foreach (var address in request.CC)
                    {
                        if (!string.IsNullOrEmpty(address) && !Helper.IsEmailValid(address))
                            throw new InvalidEmailDataException($"The provided \"CC\" address is invalid. Address: {{{address}}}");

                        mailClientData.CC.Add(new Address(address));
                    }
                }

                if (request.BCC is not null && request.BCC.Any())
                {
                    foreach (var address in request.BCC)
                    {
                        if (!string.IsNullOrEmpty(address) && !Helper.IsEmailValid(address))
                            throw new InvalidEmailDataException($"The provided \"BCC\" address is invalid. Address: {{{address}}}");

                        mailClientData.BCC.Add(new Address(address));
                    }
                }

                if (string.IsNullOrEmpty(request.Subject))
                    throw new InvalidEmailDataException("\"Subject\" is missing!");
                else
                    mailClientData.Subject = request.Subject;

                if (request.Template is not null)
                {
                    var templatePath = Path.Combine(DirectoryPaths.EmailTemplatesPath, $"{request.Template.Name}.cshtml");

                    if (!File.Exists(templatePath))
                        throw new InvalidEmailDataException($"Template not found! Template: {request.Template.Name}");

                    mailClientData.Template = request.Template;
                }

                mailClientData.Body = request.Body;
                mailClientData.Culture = string.IsNullOrEmpty(request.LanguageTwoLetterIsoCode) ? CultureInfo.InvariantCulture : CultureInfo.GetCultureInfo(request.LanguageTwoLetterIsoCode);
                //mailClientData.Attachments = request.Attachments;

                return await _mailClient.SendAsync(mailClientData) ? Result.Ok("Success") : Result.Fail("Unsuccessful");
            }
            catch (InvalidEmailDataException e)
            {
                Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
                return Result.Fail(e.Message);
            }
            catch (Exception e)
            {
                Log.Error(exception: e, messageTemplate: e.ToMessageTemplate());
                return Result.Fail("Unexpected error occured!");
            }
        }
    }
}
