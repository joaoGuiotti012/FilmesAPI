using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Dtos
{
    public class SolicitaResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
