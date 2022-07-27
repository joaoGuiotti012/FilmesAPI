using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Filme;
using FilmesAPI.Models;
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
        private AppDbContext _context;
        private IMapper _mapper;

        public FilmeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int? classificacaoEtaria)
        {
            List<Filme> filmes;
            if (classificacaoEtaria == null)
                filmes = _context.Filmes.ToList();
            else
                filmes = _context.Filmes.Where(f => f.ClassificaoEtaria <= classificacaoEtaria).ToList();
            if (filmes != null)
            {
                List<ReadFilmeDto> readDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return Ok(readDto);
            }
            return NotFound();
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);
            if (filme == null)
                return NotFound();
            ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            filmeDto.HoraDaConsulta = DateTime.Now;
            return Ok(filmeDto);
        }

        [HttpPost]
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeNovo)
        {

            Filme filme = _mapper.Map<Filme>(filmeNovo);
            _context.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { Id = filme.Id }, filme);
        }

        [HttpPut("{Id}")]
        public IActionResult AtualizaFilme(int Id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);
            if (filme == null)
                return NotFound();
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return (NoContent());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null)
                return NotFound();
            _context.Filmes.Remove(filme);
            _context.SaveChanges();
            return Ok($"Filme \"{filme.Id} - {filme.Titulo}\" deletado com sucesso");
        }
    }
}
