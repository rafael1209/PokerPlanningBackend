using PokerPlanningBackend.Models;

namespace PokerPlanningBackend.Interfaces;

public interface IUsuallyAuthService
{
    Task<User?> LoginUserAsync(string login, string password);
    Task RegisterUserAsync(string username, string email, string password, string confirmToken);
    Task<TempUser?> GetTempUserByEmail(string email);
    Task ConfirmUserEmail(TempUser tempUser);
    Task<bool> IsUsernameFree(string username);
    Task<bool> IsEmailFree(string email);
}