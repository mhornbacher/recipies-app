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
            = new List<RecipieIngredient>();

        [Required]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Column(TypeName = "text")]
        public List<string> Instructions { get; set; }
            = new List<string>();

        public bool Quick { get; set; }
        public bool Fruit { get; set; }
        public bool Reboot { get; set; }
        public bool PostWorkout { get; set; }
    }
}
