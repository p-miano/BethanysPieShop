using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); // Loads the configuration from appsettings.json, includes Kestrel, sets up IIS integration and the wwwroot folder

// This block of code adds the interfaces created in the Models folder to the dependency injection container. 
#region
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
// Using AddScoped means that the same instance of the service is used throughout the request.Both the interface and the implementation are added to the dependency injection container.

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp)); // Adds the ShoppingCart service to the dependency injection container
builder.Services.AddSession(); // Adds session support to the app
builder.Services.AddHttpContextAccessor(); // Adds HttpContextAccessor to the services container

builder.Services.AddControllersWithViews(); // Adds MVC services to the services container
builder.Services.AddDbContext<BethanysPieShopDbContext>(options => {
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]);
}); // Adds the DbContext to the services container
#endregion
var app = builder.Build(); // Builds the app

// This block of code sets up the request pipeline (middleware components)
#region
app.UseStaticFiles(); // For the wwwroot folder
app.UseSession(); // Adds session support to the app

if (app.Environment.IsDevelopment()) // Checks if the environment is Development
{
    app.UseDeveloperExceptionPage(); // Shows detailed error information
}
app.MapDefaultControllerRoute(); // Adds a default route to the app {controller=Home}/{action=Index}/{id?}
#endregion

DbInitializer.Seed(app); // Seeds the database with data when the database is empty 

app.Run(); // Runs the app
