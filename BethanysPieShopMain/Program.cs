using BethanysPieShop.App;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args); // Loads the configuration from appsettings.json, includes Kestrel, sets up IIS integration and the wwwroot folder

// This block of code adds the interfaces created in the Models folder to the dependency injection container. 
#region
builder.Services.AddControllersWithViews(); // Adds MVC services to the services container

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
// Using AddScoped means that the same instance of the service is used throughout the request.Both the interface and the implementation are added to the dependency injection container.

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp)); // Adds the ShoppingCart service to the dependency injection container
builder.Services.AddSession(); // Adds session support to the app
builder.Services.AddHttpContextAccessor(); // Adds HttpContextAccessor to the services container

builder.Services.AddControllersWithViews()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        }); // Adds MVC services to the services container (for controllers and views) and configures the JSON serializer to ignore cycles in the object graph to prevent stack overflow exceptions

builder.Services.AddRazorPages(); // Adds Razor Pages services to the services container
builder.Services.AddRazorComponents().AddInteractiveServerComponents(); // Adds Razor Components services to the services container, necesary for Blazor

builder.Services.AddDbContext<BethanysPieShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BethanysPieShopDbContextConnection"))); // Adds the DbContext to the services container

//builder.Services.AddDbContext<BethanysPieShopDbContext>(options => {
//    options.UseSqlServer(
//        builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]);
//}); // Adds the DbContext to the services container

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<BethanysPieShopDbContext>(); // Adds Identity services to the services container

//builder.Services.AddControllers(); // Adds controllers services to the services container (for API controllers). Not needed because AddControllersWithViews() includes AddControllers(). Would be needed if dealing with API controllers only.
#endregion

var app = builder.Build(); // Builds the app

// This block of code sets up the request pipeline (middleware components)
#region
if (app.Environment.IsDevelopment()) // Checks if the environment is Development
{
    app.UseDeveloperExceptionPage(); // Shows detailed error information
}

app.UseStaticFiles(); // For the wwwroot folder
app.UseSession(); // Adds session support to the app
app.UseAuthentication(); // Adds authentication support to the app
app.UseAuthorization(); // Adds authorization support to the app

app.MapDefaultControllerRoute(); // Adds a default route to the app {controller=Home}/{action=Index}/{id?}
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
// Not necessary, as MapDefaultControllerRoute() does the same thing

app.UseAntiforgery(); // Adds antiforgery support to the app, to prevent cross-site request forgery attacks. It's mandatory when using Blazor

app.MapRazorPages(); // Adds Razor Pages to the app

app.MapRazorComponents<App>().AddInteractiveServerRenderMode(); // Adds Razor Components to the app, necessary for Blazor

//app.MapControllers(); // Adds controllers to the app (for API controllers). Not needed because MapDefaultControllerRoute() includes MapControllers(). Would be needed if dealing with API controllers only.
#endregion

DbInitializer.Seed(app); // Seeds the database with data when the database is empty 

app.Run(); // Runs the app
