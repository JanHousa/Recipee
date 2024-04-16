using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;  // P�idat, pokud ji� nen� p�id�no
using Recipee.Models;
using System.ComponentModel.DataAnnotations;

namespace Recipee.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;  // P�idat logger

        public LoginModel(SignInManager<AppUser> signInManager, ILogger<LoginModel> logger)  // P�idat ILogger do konstruktoru
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
                HttpContext.Session.SetString("UserEmail", Input.Email); // Ukl�d�n� emailu do session
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
