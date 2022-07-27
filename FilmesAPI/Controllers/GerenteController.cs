using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GerenteController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public GerenteController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Gerentes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(g => g.Id == id);
            if (gerente == null)
                return NotFound();
            return Ok(_mapper.Map<ReadGerenteDto>(gerente));

        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { Id = gerente.Id }, gerente);
        }
 

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var gerente = _context.Gerentes.FirstOrDefault(g => g.Id == id);
            if (gerente == null)
                return NotFound();
            _context.Gerentes.Remove(gerente);
            _context.SaveChanges();
            return Ok($"Gerente \"{gerente.Id} - {gerente.Nome}\" deletado com sucesso");
        }

    }
}
