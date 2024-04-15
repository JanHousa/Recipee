using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recipee.Models;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Recipe> LatestRecipes { get; set; }
    public IList<Recipe> TopRatedRecipes { get; set; }

    public async Task OnGetAsync()
    {
        LatestRecipes = await _context.Recipes.OrderByDescending(r => r.CreatedDate).Take(3).ToListAsync();
        TopRatedRecipes = await _context.Recipes.OrderByDescending(r => r.AverageRating).Take(3).ToListAsync();
    }
}
