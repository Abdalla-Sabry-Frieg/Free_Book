using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Free_Book.PermissionRequirments
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);  
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
           return FallbackPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
           return FallbackPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
           if(policyName.StartsWith(Helper.Permission , System.StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirements(policyName));
                return Task.FromResult(policy.Build());
            }

           return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
