using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Recipee.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

public class DetailModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public DetailModel(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public Recipe Recipe { get; set; }
    public IList<Review> Reviews { get; set; }

    [BindProperty]
    public Review NewReview { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Recipe = await _context.Recipes
                               .Include(r => r.Reviews)
                                   .ThenInclude(review => review.User) // Ensure user data is loaded
                               .Include(r => r.Ingredients)
                               .AsNoTracking()
                               .FirstOrDefaultAsync(m => m.Id == id);

        if (Recipe == null)
        {
            return NotFound();
        }

        Reviews = Recipe.Reviews ?? new List<Review>();
        return Page();
    }





    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
        {
            Recipe = await LoadRecipeAsync(id);
            return Page();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            ModelState.AddModelError("", "User must be logged in to add a review.");
            return Page();
        }

        var newReview = new Review
        {
            UserId = user.Id, // Associate review with the logged-in user's ID
            RecipeId = id,
            Comment = NewReview.Comment,
            Rating = NewReview.Rating,
            CreatedDate = DateTime.UtcNow
        };

        _context.Reviews.Add(newReview);
        try
        {
            await _context.SaveChangesAsync();
            return RedirectToPage(new { id = id });
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", "An error occurred while saving the review: " + ex.Message);
            return Page();
        }
    }



    private async Task<Recipe> LoadRecipeAsync(int id)
    {
        return await _context.Recipes
                             .Include(r => r.Reviews) // Removed ThenInclude for User
                             .Include(r => r.Ingredients)
                             .FirstOrDefaultAsync(m => m.Id == id);
    }


}

