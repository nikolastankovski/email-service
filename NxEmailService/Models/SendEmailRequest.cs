namespace NxEmailService.Models
{
    public class SendEmailRequest
    {
        public string? From { get; set; }
        public required List<string> To { get; set; }
        public List<string>? CC { get; set; }
        public List<string>? BCC { get; set; }
        public required string Subject { get; set; }
        public Body? Body { get; set; }
        public Template? Template { get; set; }
        public string? LanguageTwoLetterIsoCode { get; set; }
    }
}
