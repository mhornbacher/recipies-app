using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipies.Core.Models
{
    public class Recipie
    {
        public int RecipieId { get; set; } 

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgName { get; set; }

        [InverseProperty("Recipie")]
        public List<RecipieIngredient> Ingredients { get; set; }

        [Required]
        [ForeignKey("CategoryForeignKey")]
        public Category Category { get; set; }

        public List<string> Instructions { get; set; }

        public Nutrition Nutrition { get; set; }
    }
}
