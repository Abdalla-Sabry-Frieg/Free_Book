using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.ViewModel
{
    public class PermissionViewModel
    {
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }

        public List<RoleClaimsViewModel>? roleClaims { get; set; } = new List<RoleClaimsViewModel>();

    }

    public class RoleClaimsViewModel
    {
        public string? Value { get; set; }
       // public string? Type { get; set; }
        public bool Selected { get; set; }
    }
}
