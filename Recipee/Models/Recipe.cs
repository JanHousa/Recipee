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
