
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipies.Core.Models
{
    /// <summary>
    /// Represents a an ingredient with a quantity for a given recipie
    /// </summary>
    public class RecipieIngredient : IIngredient
    {
        public double Qty { get; set; }
        public bool Optional { get; set; }

        public int RecipieId { get; set; }
        public Recipie Recipie { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        [NotMapped]
        public string Name
        {
            get => Ingredient.Name;
            set => Ingredient.Name = value;
        }

        [NotMapped]
        public Unit Unit
        {
            get => Ingredient.Unit;
            set => Ingredient.Unit = value;
        }
    }
}
