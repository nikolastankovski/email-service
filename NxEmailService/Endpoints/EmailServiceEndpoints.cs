namespace NxEmailService.Endpoints
{
    public static class EmailServiceEndpoints
    {
        public static void MapEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var endpointGroup = endpoints.MapGroup("api/email-service");

            endpointGroup.MapPost("/send-email", async (SendEmailRequest request, IEmailService _emailService) =>
            {
                var sendEmail = await _emailService.SendEmailAsync(request);
                return sendEmail.IsSuccess ? sendEmail.ToNoContentResponse() : sendEmail.ToBadRequest();
            })
            .Produces(statusCode: StatusCodes.Status204NoContent)
            .Produces<BadRequestModel>(statusCode: StatusCodes.Status400BadRequest)
            .Produces<InternalServerErrorModel>(statusCode: StatusCodes.Status500InternalServerError)
            .WithTags("email-service");

            endpointGroup.MapPost("/health-check", async (string to, IEmailService _emailService) =>
            {
                var request = new SendEmailRequest()
                {
                    To = [to],
                    Subject = "Health check",
                };

                var sendEmail = await _emailService.SendEmailAsync(request);
                return sendEmail.IsSuccess ? sendEmail.ToNoContentResponse() : sendEmail.ToBadRequest();
            })
            .Produces(statusCode: StatusCodes.Status204NoContent)
            .Produces<BadRequestModel>(statusCode: StatusCodes.Status400BadRequest)
            .Produces<InternalServerErrorModel>(statusCode: StatusCodes.Status500InternalServerError)
            .WithTags("email-service");
        }
    }
}
