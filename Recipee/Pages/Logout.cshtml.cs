using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LogoutModel : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {
        // Pou�it� sch�matu 'Identity.Application', kter� je v�choz� pro odhl�en� v ASP.NET Core Identity
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

        // �i�t�n� session, pokud je pou��v�no
        HttpContext.Session.Clear();

        return RedirectToPage("/Index"); // P�esm�rov�n� na �vodn� str�nku
    }
}
