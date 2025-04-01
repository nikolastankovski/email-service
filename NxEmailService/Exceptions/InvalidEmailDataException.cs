namespace NxEmailService.Exceptions
{
    public class InvalidEmailDataException : Exception
    {
        public InvalidEmailDataException(string message)
                : base(message) { }
    }
}
