using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Category
    {
        public Nullable<Guid> Id { get; set; } = Guid.Empty;

        [Required(ErrorMessageResourceType =typeof(Resources.ResourceData),ErrorMessageResourceName =("CategoryName"))]
        [MaxLength(50 , ErrorMessageResourceType =typeof(Resources.ResourceData),ErrorMessageResourceName =("NameMaxLegnth"))]
        [MinLength(1 , ErrorMessageResourceType =typeof(Resources.ResourceData),ErrorMessageResourceName =("NameMinLegnth"))]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CurrentState { get; set; } 
    }
}
