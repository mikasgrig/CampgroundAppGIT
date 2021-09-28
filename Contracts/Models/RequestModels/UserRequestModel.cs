﻿using System.ComponentModel.DataAnnotations;

namespace Contracts.Models.RequestModels
{
    public class UserRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public bool ReturnSecureToken { get; set; }
    }
}