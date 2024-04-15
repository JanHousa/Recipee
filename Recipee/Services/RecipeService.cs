using Microsoft.EntityFrameworkCore;
using Recipee.Models;

namespace Recipee.Services
{
    public class RecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task<Recipe> GetRecipeById(int id)
        {
            return await _context.Recipes.FindAsync(id);
        }
    }

}
