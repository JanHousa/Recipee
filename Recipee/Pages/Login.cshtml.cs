using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Recipee.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Recipee.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager; // UserManager needed to retrieve user data
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<LoginModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Please enter your email address.")]
            [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Please enter your password.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, isPersistent: false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User {Email} logged in at {Time}.", Input.Email, DateTime.UtcNow);

                    // Add IsAdmin claim
                    if (user.IsAdmin)
                    {
                        var claim = new Claim("IsAdmin", "True");
                        var claimResult = await _userManager.AddClaimAsync(user, claim);
                        if (!claimResult.Succeeded)
                        {
                            _logger.LogError("Failed to add IsAdmin claim for {Email}.", Input.Email);
                        }
                    }

                    return RedirectToPage("/Index");
                }
                else
                {
                    HandleFailedLogin(result);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "No account found with this email address.");
            }

            return Page();
        }

        private void HandleFailedLogin(Microsoft.AspNetCore.Identity.SignInResult result)
        {
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                ModelState.AddModelError(string.Empty, "Account locked out.");
            }
            else if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Login not allowed. Please confirm your email.");
            }
            else
            {
                _logger.LogWarning("Failed login attempt at {Time}.", DateTime.UtcNow);
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }
    }
}
