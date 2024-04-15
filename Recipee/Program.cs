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
    options.CheckConsentNeeded = context => true; // Lze nastavit na false, pokud nevyžadujete souhlas s cookies
    options.MinimumSameSitePolicy = SameSiteMode.None; // Upravte podle potøeby pro ovládání CSRF
    options.Secure = CookieSecurePolicy.Always; // Zajistí, že cookies jsou vysílány pouze pøes HTTPS
});





builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true; // Zabrání pøístupu k cookie pøes JavaScript
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Zajistí, že cookie jsou vysílány pouze pøes HTTPS
        options.Cookie.SameSite = SameSiteMode.Lax; // Pomáhá zabránit CSRF útokùm
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(30); // Nastaví životnost cookie
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
