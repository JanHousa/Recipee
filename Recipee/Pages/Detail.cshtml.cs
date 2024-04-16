using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Recipee.Models;
using System.Collections.Generic;
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
    public List<Review> Reviews { get; set; }

    [BindProperty]
    public Review NewReview { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Recipe = await _context.Recipes
                               .Include(r => r.Reviews)
                                   .ThenInclude(review => review.User) // Ensure the User object is included
                               .FirstOrDefaultAsync(m => m.Id == id);
        if (Recipe == null)
        {
            return NotFound();
        }
        Reviews = Recipe.Reviews ?? new List<Review>(); // Ensure Reviews is never null
        return Page();
    }


    public async Task<IActionResult> OnPostAsync(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            ModelState.AddModelError("", "User must be logged in to add a review.");
            return Page();
        }

        Recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
        if (Recipe == null)
        {
            return NotFound("Recipe not found.");
        }

        if (!ModelState.IsValid)
        {
            // Optionally reload the reviews if you're showing them on the same page
            Reviews = await _context.Reviews.Where(r => r.RecipeId == id).ToListAsync();
            return Page();
        }

        NewReview.User = user;
        NewReview.RecipeId = id;

        try
        {
            _context.Reviews.Add(NewReview);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // Log the exception (using a logging framework)
            ModelState.AddModelError("", "An error occurred while saving the review.");
            return Page();
        }

        return RedirectToPage(new { id = id });
    }



}

