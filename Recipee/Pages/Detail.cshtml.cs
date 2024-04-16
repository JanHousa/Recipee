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
    private readonly UserManager<AppUser> _userManager;  // Pøidání UserManager

    // Pøidání UserManager do konstruktoru
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
        Recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
        Reviews = await _context.Reviews.Where(r => r.RecipeId == id).ToListAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        Recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
        if (!ModelState.IsValid)
        {
            return Page();
        }

        NewReview.UserId = _userManager.GetUserId(User);  // Získání ID uživatele
        NewReview.RecipeId = Recipe.Id;  // Nastavení ID receptu
        _context.Reviews.Add(NewReview);
        await _context.SaveChangesAsync();

        return RedirectToPage(new { id = id });
    }
}
