using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recipee.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers(); // If you are using APIs

// Adding DbContext configuration for SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services and specify IdentityUser for the user's store
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true; // Lze nastavit na false, pokud nevy�adujete souhlas s cookies
    options.MinimumSameSitePolicy = SameSiteMode.None; // Upravte podle pot�eby pro ovl�d�n� CSRF
    options.Secure = CookieSecurePolicy.Always; // Zajist�, �e cookies jsou vys�l�ny pouze p�es HTTPS
});





builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true; // Zabr�n� p��stupu k cookie p�es JavaScript
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Zajist�, �e cookie jsou vys�l�ny pouze p�es HTTPS
        options.Cookie.SameSite = SameSiteMode.Lax; // Pom�h� zabr�nit CSRF �tok�m
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(30); // Nastav� �ivotnost cookie
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();

app.UseAuthentication(); // This is crucial, ensures the authentication services are available
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
