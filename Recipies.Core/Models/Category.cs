using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipies.Core.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [InverseProperty("Category")]
        public List<Recipie> Recipies { get; set; }
            = new List<Recipie>();
    }
}
