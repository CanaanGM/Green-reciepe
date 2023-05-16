using Recipie_Backend.Data.DataAccess;
using Recipie_Backend.Data.Models;

namespace Recipie_Backend.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly DataContext _context;

        public RecipeService(DataContext context)
        {
            _context = context;
        }
        public List<Recipe> Recipes()
        {
            return _context.Recipes.ToList();

        }
    }
}
