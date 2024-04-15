using Microsoft.EntityFrameworkCore;
using System;


namespace Recipee.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; } // Add DbSet for Ingredient

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ingredient>().HasKey(i => i.Id);  // Nastavení primárního klíče

            // Přidání dat pro Recipes a Ingredients
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = 1,
                    Title = "Čokoládový dort",
                    Description = "Bohatý čokoládový dort s třemi vrstvami.",
                    Instructions = "Smíchejte suroviny a pečte na 180°C 50 minut.",
                    CreatedDate = DateTime.Now,
                    AverageRating = 4.5,
                    ImageUrl = "url_k_obrazku_dortu"
                },
                new Recipe
                {
                    Id = 2,
                    Title = "Caesar salát",
                    Description = "Klasický Caesar salát s kuřecím masem.",
                    Instructions = "Smíchejte a podávejte čerstvé.",
                    CreatedDate = DateTime.Now,
                    AverageRating = 4.0,
                    ImageUrl = "https://receptypanicuby.cz/wp-content/uploads/2020/08/caesar-salat-recept-5.jpg"
                }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, RecipeId = 1, Name = "Čokoláda", Amount = "200g" }
                // další ingredience
            );
        }

    }
}
