using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BarberShop.Data;
using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options => { options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin")); });

// Add services to the container.
builder.Services.AddRazorPages(options => { 
    options.Conventions.AuthorizeFolder("/Bookings");
    options.Conventions.AuthorizeFolder("/Shops");
    options.Conventions.AllowAnonymousToPage("/Shops/Index");
    options.Conventions.AllowAnonymousToPage("/Shops/Details");
    options.Conventions.AuthorizeFolder("/Consumers", "AdminPolicy");
});
builder.Services.AddDbContext<BarberShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BarberShopContext") ?? throw new InvalidOperationException("Connection string 'BarberShopContext' not found.")));


builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BarberShopContext") ?? throw new InvalidOperationException("Connection string 'BarberShopContext' not found.")));



builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
