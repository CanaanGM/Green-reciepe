namespace Recipie_Backend.Data.Models { 
public class Recipe
{
    public int Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int Price { get; set; } = 0; // later to be custom type
    public ICollection<RecipieIngredient> Ingredients { get; set; } = new List<RecipieIngredient>();


    //public List<string> Images { get; set; } = new List<string>();  // 4 now a list of strings, later iamges related to users and Recipies


    public User? User { get; set; }
    public string? UserId { get; set; }
    public double Duration { get; internal set; }



    // add management related crap, like when inserted and updated . . . etc


}
}