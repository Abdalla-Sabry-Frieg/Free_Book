using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Resources;
using System.Diagnostics.CodeAnalysis;
namespace InfraStructure.ViewModel
{ 
    // because using single page 
    // view model only read one view so i crate one view model then adjust at it
    public class RolesViewModel
    {
        [AllowNull]
        public List<IdentityRole>? Roles { get; set; }
        
        public NewRole? NewRole { get; set; }
    }
    public class NewRole
    {
        
        public string? RoleId { get; set; }
        [Required(ErrorMessageResourceType =typeof(ResourceData),ErrorMessageResourceName = "RoleName")]
        public string? RoleName { get; set; }    
    }
}
