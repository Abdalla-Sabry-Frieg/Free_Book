using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class SubCategory
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("SubCategoryName"))]
        [MaxLength(50, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("NameMaxLegnth"))]
        [MinLength(3, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("NameMaxLegnth"))]
        public string Name { get; set; }

        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int CurrentState { get; set; }
    }
}
