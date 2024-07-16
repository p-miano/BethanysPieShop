namespace BethanysPieShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;

        public CategoryRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IEnumerable<Category> AllCategories => _bethanysPieShopDbContext.Categories.OrderBy(c => c.CategoryName); // This property returns all categories from the database in alphabetical order
    }
}
