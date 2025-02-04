namespace PokerPlanningBackend.Interfaces;

public interface IEmailService
{
    Task SendConfirmationEmail(string toEmail, string username, string confirmationLink);
}