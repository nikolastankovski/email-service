using FluentEmail.Core.Models;
using System.Globalization;

namespace NxEmailService.Models;

public class MailClientData
{
    public Address? From { get; set; }
    public List<Address> To { get; set; } = new List<Address>();
    public List<Address> CC { get; set; } = new List<Address>();
    public List<Address> BCC { get; set; } = new List<Address>();
    public string Subject { get; set; } = null!;
    public Template? Template { get; set; }
    public Body? Body { get; set; }
    public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    public CultureInfo? Culture { get; set; }
}
