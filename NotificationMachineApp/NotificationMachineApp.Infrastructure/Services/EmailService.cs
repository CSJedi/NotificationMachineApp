using Microsoft.Extensions.Logging;
using NotificationMachineApp.Core.Interfaces.Services;
using System.Net.Mail;

namespace NotificationMachineApp.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;
        private readonly ILogger<EmailService> _logger;

        public EmailService(SmtpClient smtpClient, string fromEmail, ILogger<EmailService> logger)
        {
            _smtpClient = smtpClient;
            _fromEmail = fromEmail;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            var successfulSends = false;
            try
            {
                var mailMessage = new MailMessage(_fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                await _smtpClient.SendMailAsync(mailMessage);
                successfulSends = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to {toEmail}");
                successfulSends = false;
            }
            return successfulSends;
        }
    }
}
