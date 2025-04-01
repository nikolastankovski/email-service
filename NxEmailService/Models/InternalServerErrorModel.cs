namespace NxEmailService.Models
{
    public class InternalServerErrorModel
    {
        public string type { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public int status { get; set; }
    }
}
