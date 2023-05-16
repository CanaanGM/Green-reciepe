namespace Recipie_Backend.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fats { get; set; }
        public int KCal { get; set; } // can be calculated via the above props

        public string? UserId { get; set; } // the fool who added it

        public ICollection<RecipieIngredient> Recipies { get; set; } = new List<RecipieIngredient>();
    }
}