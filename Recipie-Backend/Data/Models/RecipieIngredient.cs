namespace Recipie_Backend.Data.Models
{
    public class RecipieIngredient
    {
        public int RecipieId { get; set; }
        public Recipe? Recipie { get; set; }

        public int IngredientId { get; set; }
        public Ingredient? Ingrdient { get; set; }
    }
}
