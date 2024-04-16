using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;  // Pøidat, pokud již není pøidáno
using Recipee.Models;
using System.ComponentModel.DataAnnotations;

namespace Recipee.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;  // Pøidat logger

        public LoginModel(SignInManager<AppUser> signInManager, ILogger<LoginModel> logger)  // Pøidat ILogger do konstruktoru
        {
            _signInManager = signInManager;
            _logger = logger;  // Inicializovat logger
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                HttpContext.Session.SetString("UserEmail", Input.Email); // Ukládání emailu do session
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

    }
}
