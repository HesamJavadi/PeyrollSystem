using Microsoft.AspNetCore.Authorization;

namespace PayrollSystem.Persistence.Authorization.Requirements
{
    public class DynamicClaimRequirement : IAuthorizationRequirement
    {
        public string ClaimTypePrefix { get; }
        public string Value { get; }

        public DynamicClaimRequirement(string claimTypePrefix , string value)
        {
            ClaimTypePrefix = claimTypePrefix;
            Value = value;
        }
    }
}
