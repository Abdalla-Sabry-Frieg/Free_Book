using Domain.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.ViewModel
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceData),ErrorMessageResourceName = "Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "ComparPassword")]
        public string ComparPassword { get; set;}
    }
}
