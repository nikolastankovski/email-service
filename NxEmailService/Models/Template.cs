namespace NxEmailService.Models
{
    public class Template
    {
        public required string Name { get; set; }
        public object? Tokens { get; set; }
    }
}
