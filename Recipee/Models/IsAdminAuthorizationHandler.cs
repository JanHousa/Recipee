using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Recipee.Models;

public class IsAdminAuthorizationHandler : AuthorizationHandler<IsAdminRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == "IsAdmin" && c.Value.Equals("True", StringComparison.OrdinalIgnoreCase)))
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}

public class IsAdminRequirement : IAuthorizationRequirement
{
    // This requirement is just a marker.
}
