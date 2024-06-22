using Microsoft.AspNetCore.Authorization;
using PayrollSystem.Persistence.Authorization.Requirements;

namespace PayrollSystem.Persistence.Authorization.Handlers
{
    public class DynamicClaimHandler : AuthorizationHandler<DynamicClaimRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DynamicClaimRequirement requirement)
        {
            var userName = context.User.Identity.Name;
            var claimType = $"{requirement.ClaimTypePrefix}{userName}";

            if (context.User.HasClaim(c => c.Type == claimType && c.Value == requirement.Value ))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
