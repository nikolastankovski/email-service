namespace NxEmailService.Models
{
    public class EmailTemplate
    {
        public required string Name { get; set; }
        public object? Tokens { get; set; }
    }
}
