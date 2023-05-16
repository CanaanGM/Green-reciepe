using Recipie_Backend.Data.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Recipie_Backend.Data.DataAccess
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<RecipieIngredient>()
                .HasKey(ri => new { ri.RecipieId, ri.IngredientId });

            modelBuilder.Entity<RecipieIngredient>()
                .HasOne(ri => ri.Recipie)
                .WithMany(ri => ri.Ingredients)
                .HasForeignKey(ri => ri.RecipieId);

            modelBuilder.Entity<RecipieIngredient>()
                .HasOne(ri => ri.Ingrdient)
                .WithMany(ri => ri.Recipies)
                .HasForeignKey(ri => ri.RecipieId);

            
            modelBuilder.Entity<Recipe>()
                .HasOne(x => x.User)
                .WithMany(x => x.AddedRecipies)
                .HasForeignKey(x => x.UserId);


        }

        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

    }
}
