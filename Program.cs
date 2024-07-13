using BethanysPieShop.Models;

var builder = WebApplication.CreateBuilder(args); // Loads the configuration from appsettings.json, includes Kestrel, sets up IIS integration and the wwwroot folder

// This block of code adds the interfaces created in the Models folder to the dependency injection container. 
#region
builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
builder.Services.AddScoped<IPieRepository, MockPieRepository>();
// Using AddScoped means that the same instance of the service is used throughout the request.Both the interface and the implementation are added to the dependency injection container.
#endregion

builder.Services.AddControllersWithViews(); // Adds MVC services to the services container

var app = builder.Build(); // Builds the app

// This block of code sets up the request pipeline (middleware components)
#region

app.UseStaticFiles(); // For the wwwroot folder

if (app.Environment.IsDevelopment()) // Checks if the environment is Development
{
    app.UseDeveloperExceptionPage(); // Shows detailed error information
}

app.MapDefaultControllerRoute(); // Adds a default route to the app

#endregion

app.Run(); // Runs the app
