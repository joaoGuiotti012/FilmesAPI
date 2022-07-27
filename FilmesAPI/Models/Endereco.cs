using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    public class Endereco
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
        [JsonIgnore]
        public virtual Cinema Cinema { get; set; }
    }
}
