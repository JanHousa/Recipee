using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Recipee.Models;

namespace Recipee.Pages
{
    [Authorize(Policy = "IsAdminPolicy")] // Ensuring only admins can access
    public class AdminDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        // Single constructor with all dependencies
        public AdminDashboardModel(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<AppUser> Users { get; set; }
        public IList<Recipe> Recipes { get; set; }
        public IList<Review> Reviews { get; set; }

        public async Task OnGetAsync(string keyword = "")
        {
            Users = string.IsNullOrEmpty(keyword) ?
                await _context.Users.ToListAsync() :
                await _context.Users.Where(u => u.Email.Contains(keyword) || u.UserName.Contains(keyword)).ToListAsync();

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
                _context.Recipes.Remove(recipe);
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
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync(string userId, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return RedirectToPage();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddUserAsync(string email, string password)
        {
            var newUser = new AppUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToPage();
            }

            return RedirectToPage();
        }
    }
}
