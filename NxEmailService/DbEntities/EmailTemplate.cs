namespace NxEmailService.DbEntities
{
    public class EmailTemplate
    {
        public int EmailTemplateID { get; set; }
        public required string Name { get; set; }
        public required DateTime ValidFrom { get; set; }
        public required DateTime ValidTo { get; set; }
        public required DateTime CreatedOnUTC { get; set; }
    }
}
