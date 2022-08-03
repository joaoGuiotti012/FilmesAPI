using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Endereco;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private EnderecoService _enderecoService; 

        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        // GET: api/<EnderecoController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_enderecoService.GetAll());
        }

        // GET api/<EnderecoController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ReadEnderecoDto endereco = _enderecoService.GetById(id);
            return endereco != null ? Ok(endereco) : NotFound();
        }

        // POST api/<EnderecoController>
        [HttpPost]
        public IActionResult Create([FromBody] CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _enderecoService.Add(enderecoDto);
            return CreatedAtAction(nameof(GetById), new { Id = endereco.Id }, endereco);
        }

        // PUT api/<EnderecoController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            var updated = _enderecoService.Update(id, enderecoDto);
            return updated ? NoContent() : NotFound();
        }

        // DELETE api/<EnderecoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = _enderecoService.Delete(id);
            return deleted ? Ok("Endereco foi deletado com sucesso!") : NotFound();
        }
    }
}
