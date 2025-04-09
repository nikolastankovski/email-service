namespace NxEmailService.Models;

public partial class EmailHistory
{
    public int EmailHistoryID { get; set; }


    public string From { get; set; } = null!;

    public string To { get; set; } = null!;

    public string? CC { get; set; }

    public string? BCC { get; set; }

    public string? Template { get; set; }

    public string? Body { get; set; }

    public string? Attachments { get; set; }

    public string? RelatedEntityName { get; set; }

    public string? RelatedEntityId { get; set; }

    public bool? IsSent { get; set; }

    public DateTime CreatedOnUTC { get; set; }

}
