using System;

namespace FilmesAPI.Data.Dtos.Sessao
{
    public class ReadSessaoDto
    {
        public int Id { get; set; }
        public FilmesAPI.Models.Cinema Cinema { get; set; }
        public FilmesAPI.Models.Filme Filme { get; set; }
        public DateTime HorarioEncerramento { get; set; }
        public DateTime HorarioInicio { get; set; }
    }
}
