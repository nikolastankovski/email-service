namespace NxEmailService.Constants
{
    public static class DirectoryPaths
    {
        public static readonly string BasePath = Environment.CurrentDirectory;

        public static readonly string EmailTemplatesPath = Path.Combine(BasePath, "Templates");
    }
}
