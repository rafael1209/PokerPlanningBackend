namespace PokerPlanningBackend.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(string email);
}