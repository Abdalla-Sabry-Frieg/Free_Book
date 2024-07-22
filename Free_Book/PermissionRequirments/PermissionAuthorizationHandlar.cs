using Domain.Entity;
using Microsoft.AspNetCore.Authorization;

namespace Free_Book.PermissionRequirments
{
    public class PermissionAuthorizationHandlar : AuthorizationHandler<PermissionRequirements>
    {

        public PermissionAuthorizationHandlar()
        {
            
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                                PermissionRequirements requirement)
        {
            // ل مفيش user عامل login علي السيستم رجعلي فاضي
            if(context.User == null) 
            {
                return;
            }

            var permission = context.User.Claims.Where(x => x.Type == Helper.Permission &&
                                                        x.Value == requirement.Permission &&
                                                        x.Issuer == "LOCAL AUTHORITY");

            if(permission.Any())  // لو موجوده 
            {
                context.Succeed(requirement); 
                return;   
            }
        }
    }
}
