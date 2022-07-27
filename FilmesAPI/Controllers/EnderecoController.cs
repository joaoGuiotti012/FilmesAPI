using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Endereco;
using FilmesAPI.Models;
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
        private AppDbContext _context;
        private IMapper _mapper;

        public EnderecoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<EnderecoController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Enderecos);
        }

        // GET api/<EnderecoController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(end => end.Id == id);
            if (endereco == null)
                return NotFound();
            return Ok(_mapper.Map<ReadEnderecoDto>(endereco));
        }

        // POST api/<EnderecoController>
        [HttpPost]
        public IActionResult Create([FromBody] CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { Id = endereco.Id }, endereco);
        }

        // PUT api/<EnderecoController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(e => e.Id == id);
            _mapper.Map(enderecoDto, endereco);
            if (endereco == null)
                return NotFound();
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/<EnderecoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(e => e.Id == id);
            if (endereco == null)
                return NotFound();
            _context.Remove(endereco);
            _context.SaveChanges();
            return Ok("Endereco Deletado com sucesso");
        }
    }
}
