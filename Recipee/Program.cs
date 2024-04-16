using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recipee.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache(); // Potøebné pro ukládání session do pamìti

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Nastavuje, jak dlouho mùže session zùstat neaktivní, než vyprší
    options.Cookie.HttpOnly = true; // Zabraòuje pøístupu k session cookie pøes JavaScript
    options.Cookie.IsEssential = true; // Oznaèuje cookie jako nezbytné pro funkènost aplikace
});

// Pokraèování s pøidáním DbContext a Identity jako již máte
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // Pøidat session middleware do pipeline
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
