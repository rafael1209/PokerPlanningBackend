using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PokerPlanningBackend.Helpers;
using PokerPlanningBackend.Interfaces;
using PokerPlanningBackend.Models;
using PokerPlanningBackend.Requests;
using EmailConfirmation = PokerPlanningBackend.Helpers.EmailConfirmation;

namespace PokerPlanningBackend.Controllers
{
    [ApiController]
    [Route("api/v1/authorize")]
    public class AuthorizeController(
        IGoogleAuthService googleAuthService,
        IUsuallyAuthService usuallyAuthService,
        ITokenService tokenService,
        IUserRepository userRepository,
        IEmailService emailService) : Controller
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await usuallyAuthService.LoginUserAsync(request.Login, request.Password);

                return Ok(new { authToken = user?.AuthToken });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Invalid password or username" });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request.Password.Length < 8)
                return BadRequest(new { message = "Password is too weak" });

            if (!await usuallyAuthService.IsEmailFree(request.Email) ||
                !await usuallyAuthService.IsUsernameFree(request.Username)) 
                return BadRequest(new { message = "User with this email or username already exists" });

            try
            {
                var randomCode = EmailConfirmation.GenerateConfirmationCode();

                await usuallyAuthService.RegisterUserAsync(
                    request.Username,
                    request.Email,
                    request.Password,
                    randomCode);

                var confirmationLink = EmailConfirmation.GenerateConfirmationLink(request.Email, randomCode, Request);
                await emailService.SendConfirmationEmail(request.Email, request.Username, confirmationLink);

                return Ok(new { message = "Confirmation email sent" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("google/url")]
        public async Task<IActionResult> Index()
        {
            var url = await googleAuthService.GetGoogleAuthUrl();

            return Ok(new { url });
        }

        [HttpGet("google/callback")]
        public async Task<IActionResult> Callback([FromQuery] string code)
        {
            var payload = await googleAuthService.HandleGoogleCallbackAsync(code);

            if (payload == null)
                return BadRequest();

            var user = await userRepository.GetByEmailAsync(payload.Email);

            if (user != null)
                return Ok(new { authtoken = user.AuthToken });

            var authToken = tokenService.GenerateAccessToken(payload.Email);

            var newUsername = payload.Name.Replace(" ", string.Empty).ToLower() + payload.Subject;

            await userRepository.CreateAsync(new User()
            {
                Email = payload.Email,
                AvatarUrl = payload.Picture,
                CreatedAtUtc = DateTime.UtcNow,
                Username = newUsername,
                AuthToken = authToken,
            });

            return Ok(new { authToken });
        }

        [HttpGet("email/confirm")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, string token)
        {
            var tempUser = await usuallyAuthService.GetTempUserByEmail(email);

            if (tempUser == null)
                return BadRequest(new { message = "User not found" });

            if (tempUser.EmailConfirmation.Token != token)
                return BadRequest(new { message = "Invalid token" });

            await usuallyAuthService.ConfirmUserEmail(tempUser);

            return Ok(new { message = "Email successfully confirmed" });
        }

        [HttpPost("password/reset-request")]
        public async Task<IActionResult> ResetPassword(/*[FromBody] EmailRequest request*/)
        {
            //Todo: Implement password reset
            //Will send email with link to reset password
            //After clicking the link, user will be redirected to the page where he can set new password

            return Ok(new { message = "If the email exists, a reset link has been sent" });
        }
    }
}
