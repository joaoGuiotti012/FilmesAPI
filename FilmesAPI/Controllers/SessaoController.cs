using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private SessaoService _sessaoService;
        public SessaoController(SessaoService sessaoService)
        {
            _sessaoService = sessaoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSessaoDto sessaoDto)
        {
            Sessao sessao = _sessaoService.Add(sessaoDto);
            return CreatedAtAction(nameof(GetById), new { Id = sessao.Id }, sessao);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ReadSessaoDto sessao = _sessaoService.GetById(id);
            return sessao != null ? Ok(sessao) : NotFound(); 
        }
    }
}
