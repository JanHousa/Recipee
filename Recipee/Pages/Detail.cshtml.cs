using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Recipee.Models;

public class DetailModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Recipe Recipe { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Recipe = await _context.Recipes
                               .Include(r => r.Ingredients) // Pøedpokládá, že chcete zobrazit i ingredience
                               .FirstOrDefaultAsync(m => m.Id == id);

        if (Recipe == null)
        {
            return NotFound();
        }

        return Page();
    }
}
