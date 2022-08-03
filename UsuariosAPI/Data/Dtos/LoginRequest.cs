﻿using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Dtos
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
