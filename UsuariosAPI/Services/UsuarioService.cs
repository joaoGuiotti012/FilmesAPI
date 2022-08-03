using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;
        private EmailService _emailService;

        public UsuarioService(IMapper mapper, UserManager<IdentityUser<int>> userManager,
            SignInManager<IdentityUser<int>> signInManager, TokenService tokenService, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public Result CadastrarUsuario(CreateUsuarioDto createDto)
        {
            Usuario user = _mapper.Map<Usuario>(createDto);
            IdentityUser<int> identityUser = _mapper.Map<IdentityUser<int>>(user);
            Task<IdentityResult> identityResult = _userManager
                .CreateAsync(identityUser, createDto.Password);
            if (identityResult.Result.Succeeded)
            {
                var codeAtivation = _userManager
                    .GenerateEmailConfirmationTokenAsync(identityUser).Result;
                var encodedCode = HttpUtility.UrlEncode(codeAtivation);
                _emailService.EnviarEmail(new[] { identityUser.Email },
                    "Link de Ativação",
                    identityUser.Id,
                    encodedCode
                );
                return Result.Ok().WithSuccess(codeAtivation);
            }
            return Result.Fail("Falha ao cadastrar o Usuário!");
        }

        public Result Login(LoginRequest request)
        {
            var resultIdentity = _signInManager
                .PasswordSignInAsync(request.Username, request.Password, false, false);
            if (resultIdentity.Result.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(user =>
                        user.NormalizedUserName == request.Username.Normalize().ToUpper()
                    );
                Token token = _tokenService.CreateToken(identityUser);
                return Result.Ok().WithSuccess(token.Value);
            } 
            return Result.Fail("Falha ao realizar o login!");
        }

        public Result Logout()
        {
            var resultIdentity = _signInManager.SignOutAsync();
            return resultIdentity.IsCompletedSuccessfully
                ? Result.Ok() : Result.Fail("Falha ao realizar o Logout!");
        }

        public Result ActivateAcountUser(AtivaContaRequest request)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(u => u.Id == request.UsuarioId);
            var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, request.CodigoAtivacao).Result;
            if (identityResult.Succeeded)
                return Result.Ok();
            return Result.Fail("Falha ao ativar a conta");
        }

    }
}
