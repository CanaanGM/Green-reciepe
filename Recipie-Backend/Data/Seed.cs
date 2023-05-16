using Microsoft.AspNetCore.Identity;
using Recipie_Backend.Data.DataAccess;
using Recipie_Backend.Data.Models;

namespace Recipie_Backend.Data
{
    public static class Seed
    {

        public static async Task SeedData(DataContext context, UserManager<User> userManager)
        {
            await Task.Run(async () => await CreateIfEmpty(context, userManager));
            await context.SaveChangesAsync();
        }

        private static async Task CreateIfEmpty(
            DataContext context, UserManager<User> userManager)
        {
            if (!context.Recipes.Any())
                await CreateRecipes(context);
            if (!context.Ingredients.Any())
                await CreateIngredients(context);
            if (!userManager.Users.Any())
                await CreateUser(userManager);
        }

        private static async Task CreateUser(UserManager<User> userManager)
        {
            var users = new List<User>
                {
                    new User{
                        UserName = "Canaan",
                        DisplayName = "Canaan",
                        Email = "canaan@test.com"
                    },
                    new User{
                        UserName = "Dante",
                        DisplayName = "Lendary Devil Hunter",
                        Email = "dante@test.com"
                    },
                };
            foreach (var user in users) await userManager.CreateAsync(user, "Pa$$w0rd!");
        }

        private static async Task CreateIngredients(DataContext context)
        {
            var ingrs = new List<Ingredient>()
            {
                 new Ingredient
                    {
                        Id = 1,
                        Title = "Oats",
                        Description = "Plain oats"
                    },
                 new Ingredient
                 {
                     Id= 2,
                     Title = "Protein",
                     Description = "whey protein"
                 },
                 new Ingredient {
                     Id= 3,
                     Title = "Powder milk",
                     Description = "Nido Powdered milk"
                 }
            };
            await context.Ingredients.AddRangeAsync(ingrs);
        }

        private static async Task CreateRecipes(DataContext context)
        {
            var recipes = new List<Recipe>()
           {
               new Recipe
               {
                   Id = 1,
                   Title = "Oats with milk and protein",
                   Description = "Oats",
                   Duration = 10.0,
                   Ingredients =
                   {
                       new RecipieIngredient {IngredientId = 1, RecipieId = 1},
                       new RecipieIngredient {IngredientId = 2, RecipieId = 2},
                       new RecipieIngredient {IngredientId = 3, RecipieId = 3},
                   },
                   Price = 1,
               }
           };

            await context.Recipes.AddRangeAsync(recipes);
        }


    }
}
