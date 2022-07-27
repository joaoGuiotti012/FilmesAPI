using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public SessaoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSessaoDto sessaoDto)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDto);
            _context.Add(sessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { Id = sessao.Id }, sessao);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(c => c.Id == id);
            if (sessao == null)
                return NotFound();
            return Ok(_mapper.Map<ReadSessaoDto>(sessao));
        }
    }
}
