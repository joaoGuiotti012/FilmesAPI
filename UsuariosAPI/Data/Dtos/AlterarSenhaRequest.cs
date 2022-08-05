﻿using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Dtos
{
    public class AlterarSenhaRequest
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token{ get; set; }
    }
}
