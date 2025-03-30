namespace NxEmailService.Models
{
    public class EmailBody
    {
        public required string Content { get; set; }
        public bool IsHTML { get; set; } = false;
    }
}
