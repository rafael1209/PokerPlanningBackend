namespace PokerPlanningBackend.Helpers
{
    public static class EmailConfirmation
    {
        public static string GenerateConfirmationCode() =>
            new Random().Next(100_000_000, 999_999_999).ToString();

        public static string GenerateConfirmationLink(string email, string token, HttpRequest request) =>
            $"{request.Scheme}://{request.Host}/api/v1/authorize/email/confirm?email={email}&token={token}";
    }
}