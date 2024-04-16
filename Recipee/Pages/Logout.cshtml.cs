using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LogoutModel : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {
        // Použití schématu 'Identity.Application', které je výchozí pro odhlášení v ASP.NET Core Identity
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

        // Èištìní session, pokud je používáno
        HttpContext.Session.Clear();

        return RedirectToPage("/Index"); // Pøesmìrování na úvodní stránku
    }
}
