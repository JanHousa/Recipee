using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recipee.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache(); // Pot�ebn� pro ukl�d�n� session do pam�ti

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Nastavuje, jak dlouho m��e session z�stat neaktivn�, ne� vypr��
    options.Cookie.HttpOnly = true; // Zabra�uje p��stupu k session cookie p�es JavaScript
    options.Cookie.IsEssential = true; // Ozna�uje cookie jako nezbytn� pro funk�nost aplikace
});

// Pokra�ov�n� s p�id�n�m DbContext a Identity jako ji� m�te
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddControllers();

// Register the custom authorization handler
builder.Services.AddSingleton<IAuthorizationHandler, IsAdminAuthorizationHandler>();

// Define the authorization policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdminPolicy", policy =>
        policy.Requirements.Add(new IsAdminRequirement()));
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
app.UseRouting();

app.UseSession(); // P�idat session middleware do pipeline
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
