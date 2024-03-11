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
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType =typeof(Resources.ResourceData),ErrorMessageResourceName =("CategoryName"))]
        [MaxLength(50 , ErrorMessageResourceType =typeof(Resources.ResourceData),ErrorMessageResourceName =("NameMaxLegnth"))]
        [MinLength(3 , ErrorMessageResourceType =typeof(Resources.ResourceData),ErrorMessageResourceName =("NameMinLegnth"))]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("CategoryName"))]
        [MaxLength(50, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("NameMaxLegnth"))]
        [MinLength(3, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("NameMinLegnth"))]
        public int CurrentState { get; set; }
    }
}
