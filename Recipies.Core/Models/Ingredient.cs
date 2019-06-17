
using System.ComponentModel.DataAnnotations;

namespace Recipies.Core.Models
{
    public class Ingredient : IIngredient
    {
        public int IngredientId { get; set; }

        [Required]
        public string Name { get; set; }

        public Unit Unit { get; set; }
    }

    public interface IIngredient
    {
        int IngredientId { get; set; }
        string Name { get; set; }
        Unit Unit { get; set; }
    }
}
