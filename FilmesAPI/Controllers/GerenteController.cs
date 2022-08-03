using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GerenteController : ControllerBase
    {
        private readonly GerenteService _gerenteService;
        public GerenteController(GerenteService gerenteService)
        {
            _gerenteService = gerenteService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_gerenteService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ReadGerenteDto gerente = _gerenteService.GetById(id);
            return gerente != null ? Ok(gerente) : NotFound();
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _gerenteService.Add(gerenteDto);
            return CreatedAtAction(nameof(GetById), new { Id = gerente.Id }, gerente);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _gerenteService.DeleteById(id);
            return deleted ? Ok("Gerente deletado com sucesso!") : NotFound();
        }

    }
}
