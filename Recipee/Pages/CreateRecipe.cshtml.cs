using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recipee.Models;

namespace Recipee.Pages
{
    [Authorize]
    public class CreateRecipeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateRecipeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Recipes.Add(Recipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index"); // Redirect to the main page or a confirmation page
        }
    }

}
