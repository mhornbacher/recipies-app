
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipies.Core.Models
{
    /// <summary>
    /// Represents a an ingredient with a quantity for a given recipie
    /// </summary>
    public class RecipieIngredient
    {
        public int RecipieIngredientId { get; set; }

        public double Qty { get; set; }

        [ForeignKey("RecipieForeignKey")]
        public Recipie Recipie { get; set; }

        [ForeignKey("IngredientForeignKey")]
        public Ingredient Ingredient { get; set; }

        public int IngredientId {
            get => Ingredient.IngredientId;
            set => Ingredient.IngredientId = value;
        }
        public string Label
        {
            get => Ingredient.Label;
            set => Ingredient.Label = value;
        }
        public string Unit
        {
            get => Ingredient.Unit;
            set => Ingredient.Unit = value;
        }
    }
}
