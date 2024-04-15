using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Recipee.Models;
using Microsoft.EntityFrameworkCore;


namespace Recipee.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
       public RecipesController(ApplicationDbContext context)
        {
            _context = context;
       }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return Ok(recipe);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Recipes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

    }
}
