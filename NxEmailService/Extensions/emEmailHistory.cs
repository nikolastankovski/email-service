using FluentEmail.Core.Models;
using NxEmailService.Models;
using System.Text.Json;

namespace NxEmailService.ExtensionMethods
{
    public static class emEmailHistory
    {
        public static EmailHistory ToEmailHistory(this EmailData entity, string emailTemplate)
        {
            var et =  $"\\{nameof(NxEmailService.Templates)}\\";
            emailTemplate = emailTemplate.Substring(emailTemplate.IndexOf(et) + et.Length);

            var emailHistory = new EmailHistory()
            {
                From = entity.FromAddress.EmailAddress,
                To = JsonSerializer.Serialize(entity.ToAddresses.Select(x => x.EmailAddress)),
                CC = entity.CcAddresses.Any() ? JsonSerializer.Serialize(entity.CcAddresses.Select(x => x.EmailAddress)) : null,
                BCC = entity.BccAddresses.Any() ? JsonSerializer.Serialize(entity.BccAddresses.Select(x => x.EmailAddress)) : null,
                Body = entity.Body,
                Attachments = entity.Attachments.Any() ? JsonSerializer.Serialize(entity.Attachments.Select(x => x.Filename)) : null,
                Template = emailTemplate,
                IsSent = false
            };

            return emailHistory;
        }
    }
}
