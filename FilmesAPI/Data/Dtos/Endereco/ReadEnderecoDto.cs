using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.Endereco
{
    public class ReadEnderecoDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(80, ErrorMessage = " O campo {0} só permite 80 caracteres")]
        public string Logradouro { get; set; }
        [Required]
        [MaxLength(80, ErrorMessage = " O campo {0} só permite 80 caracteres")]
        public string Bairro { get; set; }
        [Required]
        public int Numero { get; set; } 
    }
}
