using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using PokerPlanningBackend.Interfaces;
using PokerPlanningBackend.Models;

namespace PokerPlanningBackend.Services
{
    public class UsuallyAuthService(
        IUserRepository userRepository,
        ITokenService tokenService,
        IPasswordHasher passwordHasher,
        ITempUserRepository tempUserRepository) : IUsuallyAuthService
    {
        public async Task<User?> LoginUserAsync(string login, string password)
        {
            var user = IsValidEmail(login)
                ? await userRepository.GetByEmailAsync(login)
                : await userRepository.GetByUsernameAsync(login);

            if (user?.HashedPassword != null && passwordHasher.IsVerify(password, user.HashedPassword))
                return user;

            throw new Exception("Invalid password or username");
        }

        public async Task RegisterUserAsync(string username, string email, string password, string confirmToken)
        {
            var userHashedPassword = passwordHasher.Generate(password);

            var user = new TempUser
            {
                Username = username,
                Email = email,
                HashedPassword = userHashedPassword,
                CreatedAtUtc = DateTime.UtcNow,
                EmailConfirmation = new EmailConfirmation
                {
                    Token = confirmToken,
                    ExpiresAtUtc = DateTime.UtcNow //todo: change this shit
                }
            };

            await tempUserRepository.CreateAsync(user);
        }

        public async Task<TempUser?> GetTempUserByEmail(string email)
        {
            return await tempUserRepository.GetByEmailAsync(email);
        }

        public async Task ConfirmUserEmail(TempUser tempUser)
        {
            var newAuthToken = tokenService.GenerateAccessToken(email: tempUser.Email);

            await userRepository.CreateAsync(new User
            {
                Email = tempUser.Email,
                Username = tempUser.Username,
                HashedPassword = tempUser.HashedPassword,
                AuthToken = newAuthToken,
                CreatedAtUtc = tempUser.CreatedAtUtc
            });

            await tempUserRepository.DeleteAsync(tempUser.Id);
        }

        public bool IsValidEmail(string email)
        {
            const string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            return Regex.IsMatch(email, pattern);
        }
    }
}
