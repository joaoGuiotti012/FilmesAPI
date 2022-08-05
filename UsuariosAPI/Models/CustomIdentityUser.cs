using Microsoft.AspNetCore.Identity;
using System;

namespace UsuariosAPI.Models
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public DateTime DataNascimento { get; set; }
    }
}
