using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Recipee.Models
{
    // DbContext je nyní IdentityDbContext, který přijímá IdentityUser jako výchozí typ uživatele.
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; } // Přidán DbSet pro Ingredient

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Důležité pro správnou konfiguraci Identity

            // Nastavení primárního klíče pro Ingredient
            modelBuilder.Entity<Ingredient>().HasKey(i => i.Id);

            // Přidání seed dat pro Recipes a Ingredients
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
            // Přidat další ingredience pokud potřebujete
            );

            // Přidání seed dat pro Users
            
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "1",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "admin"),
                    SecurityStamp = string.Empty,
                    ConcurrencyStamp = string.Empty,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    IsAdmin = true
                }

            );
        }
    }
}
