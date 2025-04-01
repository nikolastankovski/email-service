using System.Text.RegularExpressions;

namespace NxEmailService.Helpers;

public class Helper
{
    public static bool IsEmailValid(string email)
    {
        var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        return regex.IsMatch(email);
    }
}
