using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Filme;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        private FilmeService _filmeService;
        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpGet]
        [Authorize(Roles = "admin, initial", Policy = "IdadeMinima")]
        public IActionResult GetAll([FromQuery] int? classificacaoEtaria)
        {
            List<ReadFilmeDto> filmes = _filmeService.GetAll(classificacaoEtaria);
            return filmes != null ? Ok(filmes) : NotFound();
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "admin, initial")]
        public IActionResult GetById(int Id)
        {
            ReadFilmeDto filme = _filmeService.GetById(Id);
            return filme != null ? Ok(filme) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
        {
            ReadFilmeDto filme = _filmeService.Add(filmeDto);
            return CreatedAtAction(nameof(GetById), new { Id = filme.Id }, filme);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "admin")]
        public IActionResult AtualizaFilme(int Id, [FromBody] UpdateFilmeDto filmeDto)
        {
            bool updatedFilme = _filmeService.Update(Id, filmeDto);
            return updatedFilme ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            return _filmeService.DeleteById(id) ?
                Ok("Filme deletado com sucesso") : NotFound();
        }
    }
}
