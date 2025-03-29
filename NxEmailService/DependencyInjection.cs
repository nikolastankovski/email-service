using FluentEmail.Core;
using FluentEmail.MailKitSmtp;
using FluentEmail.Razor;
using System.Reflection;
using NxEmailService.EmailService;
using NxEmailService.Repositories;

namespace NxEmailService
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmailHistoryRepository, EmailHistoryRepository>();

            return services;
        }

        public static IApplicationBuilder RegisterEndpoints(this IApplicationBuilder app)
        {
            var mapEndpointMethods = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace == "NxEmailService.Endpoints" && t.GetMethod("MapEndpoints") != null)
                .Select(x => x.GetMethod("MapEndpoints"));

            foreach (var m in mapEndpointMethods)
            {
                if (m != null)
                    m.Invoke(null, new object[] { app });
            }

            return app;
        }

        public static WebApplicationBuilder AddEmailService(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddOptions<EmailServiceConfig>()
                .Bind(builder.Configuration.GetSection(EmailServiceConfig.SectionName))
                .ValidateOnStart();

            EmailServiceConfig emailConfig = builder.Configuration.GetSection(EmailServiceConfig.SectionName).Get<EmailServiceConfig>() ?? new EmailServiceConfig();

            builder.Services
                    .AddFluentEmail(emailConfig.From, emailConfig.DisplayName)
                    .AddRazorRenderer(DirectoryPaths.EmailTemplatesPath)
                    .AddMailKitSender(emailConfig.SmtpClientOptions);

            Email.DefaultRenderer = new RazorRenderer(DirectoryPaths.EmailTemplatesPath);
            Email.DefaultSender = new MailKitSender(emailConfig.SmtpClientOptions);

            builder.Services.AddScoped<IMailClient, MailClient>();

            return builder;
        }
    }
}
