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
    public List<Review> Reviews { get; set; }

    [BindProperty]
    public Review NewReview { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Recipe = await _context.Recipes
                               .Include(r => r.Reviews)
                                   .ThenInclude(review => review.User) // Load reviewers
                               .Include(r => r.Ingredients) // Make sure to load ingredients
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

        Recipe = await _context.Recipes.Include(r => r.Reviews).FirstOrDefaultAsync(m => m.Id == id);
        if (Recipe == null)
        {
            return NotFound("Recipe not found.");
        }

        if (!ModelState.IsValid)
        {
            // It's helpful to log or output the ModelState errors here
            foreach (var error in ModelState.Values.SelectMany(m => m.Errors))
            {
                Debug.WriteLine(error.ErrorMessage);
            }
            return Page();
        }

        NewReview.UserId = user.Id; // This should be sufficient if the user is correctly attached
        _context.Reviews.Add(NewReview);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log or output the exception message
            Debug.WriteLine(ex.Message);
            ModelState.AddModelError("", "An error occurred while saving the review.");
            return Page();
        }

        return RedirectToPage(new { id = id });
    }





}

