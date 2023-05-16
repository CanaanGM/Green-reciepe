using Recipie_Backend.Data.Models;

namespace Recipie_Backend.Services
{
    public interface IRecipeService
    {
        public List<Recipe> Recipes();
    }
}
