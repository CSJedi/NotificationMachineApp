using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using NotificationMachineApp.Core.Interfaces.Services;

namespace NotificationMachineApp.Infrastructure.Email
{
    public class EmailSender : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, string fromEmail, ILogger<EmailSender> logger)
        {
            _smtpClient = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };
            _fromEmail = fromEmail;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var mailMessage = new MailMessage(_fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                await _smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation($"Email sent to {toEmail}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to {toEmail}");
                return false;
            }
        }

        public async Task<int> SendEmailToMultipleRecipientsAsync(IEnumerable<string> toEmails, string subject, string body)
        {
            int successfulSends = 0;
            foreach (var email in toEmails)
            {
                if (await SendEmailAsync(email, subject, body))
                {
                    successfulSends++;
                }
            }
            return successfulSends;
        }
    }
}
