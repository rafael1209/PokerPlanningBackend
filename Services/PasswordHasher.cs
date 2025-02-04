using Org.BouncyCastle.Crypto.Generators;
using PokerPlanningBackend.Interfaces;
using System.Text;

namespace PokerPlanningBackend.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly byte[] _salt;

        public PasswordHasher(IConfiguration configuration)
        {
            var saltBase64 = configuration.GetValue<string>("PasswordHasher:salt")
                             ?? throw new InvalidOperationException("Salt not found in configuration!");

            _salt = Convert.FromBase64String(saltBase64);
            if (_salt.Length != 16)
            {
                throw new ArgumentException("Salt must be exactly 128 bits (16 bytes)!");
            }
        }

        public string Generate(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            int cost = 12;
            byte[] hashBytes = BCrypt.Generate(passwordBytes, _salt, cost);
            return Convert.ToBase64String(hashBytes);
        }

        public bool IsVerify(string password, string hashedPassword)
        {
            var enteredHashedPassword = Generate(password);
            return enteredHashedPassword == hashedPassword;
        }
    }
}