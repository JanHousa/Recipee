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
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Ingredient
            modelBuilder.Entity<Ingredient>()
                .HasKey(i => i.Id);

            // Configure Recipe and related entities
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithOne(i => i.Recipe)
                .HasForeignKey(i => i.RecipeId);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Reviews)
                .WithOne(rv => rv.Recipe)
                .HasForeignKey(rv => rv.RecipeId);

            // Configure Review and User relationship
            modelBuilder.Entity<Review>()
                .HasOne<AppUser>(r => r.User)
                .WithMany() // Assuming no navigation property back from User to Reviews
                .HasForeignKey(r => r.UserId)
                .IsRequired();

            // Seed data for Recipes
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = 1,
                    Title = "Čokoládový dort",
                    Description = "Bohatý čokoládový dort s třemi vrstvami.",
                    Instructions = "Smíchejte suroviny a pečte na 180°C 50 minut.",
                    CreatedDate = DateTime.Now,
                    AverageRating = 4.5,
                    ImageUrl = "https://vikendovepeceni.cz/wp-content/uploads/2021/09/cokoladovy-dort-03.jpg"
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

            // Seed data for Ingredients
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, RecipeId = 1, Name = "Čokoláda", Amount = "200g" }
            // Add more ingredients as needed
            );

            // Seed data for Users
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "1",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "admin123"), // Update password here
                    SecurityStamp = Guid.NewGuid().ToString(), // Use a new GUID for security stamp
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    IsAdmin = true
                }
            );
        }

    }
}
