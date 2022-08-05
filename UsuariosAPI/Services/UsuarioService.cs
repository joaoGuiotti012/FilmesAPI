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
        private UserManager<CustomIdentityUser> _userManager;
        private SignInManager<CustomIdentityUser> _signInManager;
        private TokenService _tokenService;
        private EmailService _emailService;
        private RoleManager<IdentityRole<int>> _roleManager;

        public UsuarioService(IMapper mapper, UserManager<CustomIdentityUser> userManager,
            SignInManager<CustomIdentityUser> signInManager, TokenService tokenService,
            EmailService emailService, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public Result CadastrarUsuario(CreateUsuarioDto createDto)
        {
            Usuario user = _mapper.Map<Usuario>(createDto);
            CustomIdentityUser identityUser = _mapper.Map<CustomIdentityUser>(user);
            Task<IdentityResult> identityResult = _userManager
                .CreateAsync(identityUser, createDto.Password);
            // Setando Role Inicial
            _userManager.AddToRoleAsync(identityUser, "initial");
             
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
                Token token = _tokenService
                    .CreateToken(identityUser, _signInManager
                        .UserManager.GetRolesAsync(identityUser).Result
                        .FirstOrDefault()
                    );
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

        public Result SolicitaResetSenha(SolicitaResetRequest request)
        {
            CustomIdentityUser identityUser = GetUserByEmail(request.Email);
            if (identityUser != null)
            {
                string codidoRecuperacao = _signInManager
                    .UserManager
                    .GeneratePasswordResetTokenAsync(identityUser)
                    .Result;
                return Result.Ok().WithSuccess(codidoRecuperacao);
            }
            return Result.Fail("Falha ao solicitar a redefinição de senha!");
        }
        public Result AlteraSenhaUsuario(AlterarSenhaRequest request)
        {
            CustomIdentityUser identityUser = GetUserByEmail(request.Email);
            IdentityResult result = _signInManager
                .UserManager.ResetPasswordAsync(identityUser, request.Token, request.Password)
                .Result;
            if (result.Succeeded) return Result.Ok().WithSuccess("Senha redefinida com sucesso!");
            return Result.Fail("Houve um erro na opreção de alteração de senha!");
        }

        private CustomIdentityUser GetUserByEmail(string email)
        {
            return _signInManager
                .UserManager
                .Users
                .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
        }
    }
}
