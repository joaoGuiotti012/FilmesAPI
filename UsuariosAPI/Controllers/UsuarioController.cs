using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public IActionResult create([FromBody] CreateUsuarioDto createDto)
        {
            Result result = _usuarioService.CadastrarUsuario(createDto);
            return (result.IsFailed)
                ? StatusCode(500, result.Errors) : Ok(result.Successes);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            Result result = _usuarioService.Login(request);
            return (result.IsFailed)
                ? Unauthorized(result.Errors) : Ok(result.Successes);
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            Result result = _usuarioService.Logout();
            return result.IsFailed ? Unauthorized(result.Errors) : Ok(result.Successes);
        }

        [HttpGet("ativar-conta")]
        public IActionResult ActiveAccount([FromQuery] AtivaContaRequest request)
        {
            Result result = _usuarioService.ActivateAcountUser(request);
            if (result.IsFailed) return StatusCode(500);
            return Ok(result.Successes);
        }

        [HttpPost("solicita-reset")]
        public IActionResult ResetPassword([FromBody] SolicitaResetRequest request)
        {
            Result result = _usuarioService.SolicitaResetSenha(request);
            if( result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }

        [HttpPost("reseta-senha")]
        public IActionResult AlterarSenha([FromBody] AlterarSenhaRequest request)
        {
            Result result = _usuarioService.AlteraSenhaUsuario(request);
            if (result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }
    }
}
