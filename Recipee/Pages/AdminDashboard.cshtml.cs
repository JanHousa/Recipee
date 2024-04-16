using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Recipee.Models;

namespace Recipee.Pages
{
    [Authorize(Roles = "Admin")] // Ensure this page is accessible only to admins
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
            Users = await _context.Users.ToListAsync();
            Recipes = await _context.Recipes.Include(r => r.Ingredients).ToListAsync();
            Reviews = await _context.Reviews.Include(r => r.User).ToListAsync();
        }
    }
}
