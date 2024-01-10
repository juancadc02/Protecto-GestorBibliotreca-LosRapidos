using Microsoft.AspNetCore.Authorization;

namespace Poyecto_Gestor_Biblioteca_Web_Los_Rapidos.NewFolder1
{
    public class RequiereIdAcceso1Handler: AuthorizationHandler<RequiereIdAcceso1Requirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequiereIdAcceso1Requirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "id_acceso" && c.Value == "1"))
            {
                context.Fail();  // Si el usuario no tiene el id_acceso igual a 1, falla la autorización.
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
