using FluentEmail.Core.Models;
using NxEmailService.Templates;
using NxEmailService.EmailService;
using System.Globalization;

namespace NxEmailService.Endpoints
{
    public static class EmailServiceEndpoints
    {
        public static void MapEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var endpointGroup = endpoints.MapGroup("api/email-service");

            endpointGroup.MapPost("/send-email", async (string templateName, object tokens, IMailClient _mailClient) =>
            {
                var emailSetUp = new EmailSetUp()
                {
                    To = new Address("stankovski.n@hotmail.com"),
                    EmailTemplate = EmailTemplates.Account_ForgotPassword,
                    Subject = "Test mail",
                    Culture = CultureInfo.GetCultureInfo("en-US"),
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
