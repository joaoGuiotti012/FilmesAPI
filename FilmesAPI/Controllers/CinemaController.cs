using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private CinemaService _cinemaService;
        public CinemaController(CinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string nomeFilme)
        {
            List<ReadCinemaDto> cinemas = _cinemaService.GetAll(nomeFilme);
            return cinemas == null ? NotFound() : Ok(cinemas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ReadCinemaDto cinema = _cinemaService.GetById(id);
            return cinema == null ? NotFound() : Ok(cinema);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _cinemaService.Add(cinemaDto);
            return CreatedAtAction(nameof(GetById), new { Id = cinema.Id }, cinema);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            var updatedCinema = _cinemaService.Update(id, cinemaDto);
            return updatedCinema ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _cinemaService.DeleteById(id);
            return deleted ? Ok("Filme Deletado com sucesso!") : NotFound(); 
        }
    }
}
