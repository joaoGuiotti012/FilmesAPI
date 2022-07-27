using System;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.Filme
{
    public class ReadFilmeDto : FilmesAPI.Models.Filme
    {
        public DateTime HoraDaConsulta { get; set; }
    }
}
