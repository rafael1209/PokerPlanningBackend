using System.ComponentModel.DataAnnotations;

namespace PokerPlanningBackend.Requests
{
    public class EmailRequest
    {
        [EmailAddress]
        public required string Email { get; set; }
    }
}