﻿using System.ComponentModel.DataAnnotations;

namespace PokerPlanningBackend.Requests
{
    public class RegisterRequest
    {
        public required string Username { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
