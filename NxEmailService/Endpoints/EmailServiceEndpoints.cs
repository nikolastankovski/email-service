using FluentEmail.Core.Models;
using NxEmailService.EmailService;

namespace NxEmailService.Endpoints
{
    public static class EmailServiceEndpoints
    {
        public static void MapEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var endpointGroup = endpoints.MapGroup("api/email-service");

            endpointGroup.MapPost("/send-email", async (string test, IMailClient _mailClient) =>
            {
                var emailSetUp = new EmailSetUp()
                {
                    To = new Address("stankovski.n@hotmail.com"),
                    From = new Address("nikola.stankovski98@gmail.com"),
                    EmailTemplate = "asd",
                    Subject = "Test mail",
                    LanguageTwoLetterIsoCode = "en",
                    Tokens = new { Name = "Nikola Stankovski" }
                };

                await _mailClient.SendEmailAsync(emailSetUp);

                return Results.Ok("great");
            })
            .Produces(statusCode: StatusCodes.Status200OK)
            .WithTags("email-service");

        }
    }
}
