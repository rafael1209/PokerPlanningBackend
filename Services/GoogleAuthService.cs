using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth;
using PokerPlanningBackend.Interfaces;

namespace PokerPlanningBackend.Services
{
    public class GoogleAuthService(IConfiguration configuration) : IGoogleAuthService
    {
        private readonly string? _clientId = configuration["Google:ClientId"];
        private readonly string? _clientSecret = configuration["Google:ClientSecret"];
        private readonly string? _redirectUri = configuration["Google:RedirectUrl"];

        public Task<Uri> GetGoogleAuthUrl()
        {
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = _clientId,
                    ClientSecret = _clientSecret
                },
                Scopes = ["email", "profile"]
            });

            var authUri = flow.CreateAuthorizationCodeRequest(_redirectUri).Build();
            return Task.FromResult(authUri);
        }

        public async Task<GoogleJsonWebSignature.Payload?> HandleGoogleCallbackAsync(string code)
        {
            try
            {
                var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = _clientId,
                        ClientSecret = _clientSecret
                    }
                });

                var tokenResponse = await flow.ExchangeCodeForTokenAsync("me", code, _redirectUri, CancellationToken.None);

                var payload = await GoogleJsonWebSignature.ValidateAsync(tokenResponse.IdToken);

                return payload;
            }
            catch
            {
                return null;
            }
        }
    }
}
