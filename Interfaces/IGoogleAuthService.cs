using Google.Apis.Auth;

namespace PokerPlanningBackend.Interfaces;

public interface IGoogleAuthService
{
    Task<Uri> GetGoogleAuthUrl();
    Task<GoogleJsonWebSignature.Payload?> HandleGoogleCallbackAsync(string code);
}