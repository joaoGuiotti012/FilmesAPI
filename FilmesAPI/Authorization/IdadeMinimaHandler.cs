using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmesAPI.Authorization
{
    public class IdadeMinimaHandler : AuthorizationHandler<IdadeMinimaRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinimaRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "dataNascimento")) 
                return Task.CompletedTask;
            var dataNascimento = Convert.ToDateTime(context.User.FindFirst(c =>
                c.Type == "dataNascimento"
            ).Value);

            int idadeObitda = DateTime.Now.Year - dataNascimento.Year;
            
            if (dataNascimento > DateTime.Today.AddYears(-idadeObitda))
                idadeObitda--;
            
            if (idadeObitda >= requirement.IdadeMinima)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
