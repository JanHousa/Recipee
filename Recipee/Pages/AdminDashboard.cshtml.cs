using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Recipee.Models;

namespace Recipee.Pages
{
    [Authorize(Policy = "IsAdminPolicy")] // Changed from roles to custom policy
    public class AdminDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AdminDashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AppUser> Users { get; set; }
        public IList<Recipe> Recipes { get; set; }
        public IList<Review> Reviews { get; set; }

        public async Task OnGetAsync()
        {
            // Retrieve data for the admin dashboard
            Users = await _context.Users.ToListAsync();
            Recipes = await _context.Recipes.Include(r => r.Ingredients).ToListAsync();
            Reviews = await _context.Reviews.Include(r => r.User).ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                // If the recipe exists, remove it and its related ingredients
                _context.Recipes.Remove(recipe);

                // This will remove all ingredients that are related to this recipe
                var ingredients = _context.Ingredients.Where(i => i.RecipeId == id);
                _context.Ingredients.RemoveRange(ingredients);

                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                // If the review exists, remove it
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

    }
}
