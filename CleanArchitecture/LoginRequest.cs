﻿using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.API
{
    public class LoginRequest
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
