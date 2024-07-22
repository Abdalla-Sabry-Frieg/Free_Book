using Microsoft.AspNetCore.Authorization;

namespace Free_Book.PermissionRequirments
{
    // To Add Policy
    public class PermissionRequirements : IAuthorizationRequirement
    { 
        public string Permission { get;private set; }

        public PermissionRequirements( string permission)
        {
            Permission = permission;
        }
    }
}
