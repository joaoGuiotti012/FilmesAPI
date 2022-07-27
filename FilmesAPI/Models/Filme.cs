using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Diretor { get; set; }
        [StringLength(30, ErrorMessage = "O campo {0} não pode passar de {1} caracteres")]
        public string Genero { get; set; }
        [Range(1, 600, ErrorMessage = "O campo {0} deve ter uma intervalo entre {1} até {2}")]
        public int Duracao { get; set; }
        public int ClassificaoEtaria { get; set; }
        [JsonIgnore]
        public virtual List<Sessao> Sessoes{ get; set; }
    }
}
