using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.ViewModel
{
    public class CategoryViewModel
    {
        // To show the new data
        [AllowNull]
        public List<Category>? categories { get; set; }

        [AllowNull]
        public List<LogCategory>? LogCategories { get; set; }

        // To save the new data

        [AllowNull]
        public Category? NewCategory { get; set; }
    }
}
