namespace NxEmailService.Models
{
    public class Body
    {
        public required string Content { get; set; }
        public bool IsHTML { get; set; } = false;
    }
}
