using Microsoft.EntityFrameworkCore;
using Recipee.Models;
using Recipee.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers(); // P�id�v� podporu pro kontrolery
// P�id�n� DbContext s konfigurac� pro SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<RecipeService>();

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

app.UseAuthentication(); // Ujist�te se, �e je povoleno, pokud pou��v�te autentizaci
app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
