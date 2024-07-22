using Domain.Entity;
using Domain.Resources;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.ViewModel
{
    public class RegisterViewModel
    {
        [AllowNull]
        public List<VwUser>? Users { get; set; }
        public NewRegister? NewRegister { get; set; }

        // when create new user m/ust choses the role  and will use this prop  to show  drop down list
        [AllowNull]
        // To view a drop list
        public List<IdentityRole>? Roles { get; set; }
        [AllowNull]
        public ChangePasswordViewModel? ChangePassword { get; set; }

    }
    public class NewRegister
    {
        public string? Id { get; set; }

        [Required(ErrorMessageResourceType =typeof(ResourceData),ErrorMessageResourceName = "RegisterName")]
        public string Name { get; set; }

       // [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RoleName")]
        public string? RoleName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "EmailName")]
        [EmailAddress(ErrorMessageResourceType =typeof(ResourceData),ErrorMessageResourceName = "EmailNameError")]
        public string Email { get; set; }
        public string? ImageUser { get; set; }
        public bool ActiveUser { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "Password")]

        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "ComparPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "ComparPasswordError")]
        public string ComparePassword { get; set; }

    }
}
