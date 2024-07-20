using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class BethanysPieShopDbContext : DbContext
    {
        // Inherits from DbContext, which is the base class in EF Core responsible for managing database connections and performing CRUD (Create, Read, Update, Delete) operations.

        // Constructor that takes DbContextOptions as a parameter. This is the object that EF Core uses to configure the database connection. This includes specifying the database provider (SQL Server) and connection string, which are defined in the Program.cs file and read from the appsettings.json file.
        public BethanysPieShopDbContext(DbContextOptions<BethanysPieShopDbContext> options) : base(options)
        {
        }

        // DbSet<TEntity> properties represent collections of entities that EF Core tracks and maps to database tables.
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; }   
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    
    }
}
