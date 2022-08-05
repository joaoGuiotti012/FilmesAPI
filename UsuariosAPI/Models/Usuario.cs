using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsuariosAPI.Models
{
    public class Usuario 
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
