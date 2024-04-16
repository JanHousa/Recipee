using Recipee.Models;
using System;
using System.Collections.Generic;

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

    // Add a property for storing reviews related to this recipe
    public List<Review> Reviews { get; set; } = new List<Review>();
}

public class Ingredient
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Amount { get; set; }
    public Recipe Recipe { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAdmin { get; set; } // Fix the type here, assuming it's meant to be boolean
}

public class Review
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; } // Navigation property
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; }
}
