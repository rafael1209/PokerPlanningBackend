using System.ComponentModel.DataAnnotations;

namespace PokerPlanningBackend.Requests.Auth
{
    public class EmailRequest
    {
        [EmailAddress]
        public required string Email { get; set; }
    }
}