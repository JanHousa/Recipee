using Microsoft.AspNetCore.Identity;
using Recipee.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public string Instructions { get; set; }
    public DateTime CreatedDate { get; set; }
    public double AverageRating { get; set; }
    public string ImageUrl { get; set; }
}

public class Ingredient
{
    public int Id { get; set; } // Přidání primárního klíče
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Amount { get; set; }
    public Recipe Recipe { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Boolean IsAdmin { get; set; } = false;
}


public class Review
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; }
}
