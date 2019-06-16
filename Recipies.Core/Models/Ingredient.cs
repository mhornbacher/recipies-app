
using System.ComponentModel.DataAnnotations;

namespace Recipies.Core.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        [Required]
        public string Label { get; set; }

        [Required]
        public string Unit { get; set; }
    }
}
