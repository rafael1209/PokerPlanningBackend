using PokerPlanningBackend.Interfaces;
using System.Net.Mail;
using System.Net;

namespace PokerPlanningBackend.Services
{
    public class EmailService(IConfiguration configuration) : IEmailService
    {
        private readonly string _smtpServer = configuration.GetValue<string>("Email:SmtpServer")
                                              ?? throw new ArgumentNullException($"Email:SmtpServer", "SMTP Server is not configured.");
        private readonly int _smtpPort = configuration.GetValue<int>("Email:SmtpPort");
        private readonly string _smtpUser = configuration.GetValue<string>("Email:SmtpUser")
                                            ?? throw new ArgumentNullException($"Email:SmtpUser", "SMTP User is not configured.");
        private readonly string _smtpPassword = configuration.GetValue<string>("Email:SmtpPassword")
                                                ?? throw new ArgumentNullException($"EmailSettings:SmtpPassword", "SMTP Password is not configured.");

        public async Task SendConfirmationEmail(string toEmail, string username, string confirmationLink)
        {
            const string subject = "Confirm Your Email - Elysium";

            var templatePath = Path.Combine(AppContext.BaseDirectory, "EmailTemplates", "emailConfirm.html");

            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Email template not found at {templatePath}");
            }

            var body = (await File.ReadAllTextAsync(templatePath))
                .Replace("{{USERNAME}}", username)
                .Replace("{{CONFIRMATION_LINK}}", confirmationLink)
                .Replace("{{CURRENT_YEAR}}", DateTime.UtcNow.Year.ToString());

            using var client = new SmtpClient(_smtpServer, _smtpPort);
            client.Credentials = new NetworkCredential(_smtpUser, _smtpPassword);
            client.EnableSsl = true;

            var mail = new MailMessage(_smtpUser, toEmail, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mail);
        }
    }
}
