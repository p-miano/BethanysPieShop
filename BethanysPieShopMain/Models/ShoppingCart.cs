using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;
        public string? ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        public ShoppingCart(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }
        
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            BethanysPieShopDbContext context = services.GetService<BethanysPieShopDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        } // Retrieves the shopping cart for a user by accessing the session, retrieving the database context, generating or retrieving a unique cart ID, and returning a new instance of the ShoppingCart class with the retrieved cart ID.

        public void AddToCart(Pie pie)
        {
            var shoppingCartItem = _bethanysPieShopDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };

                _bethanysPieShopDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _bethanysPieShopDbContext.SaveChanges();
        } // Adds a pie to the shopping cart. If the pie is already in the cart, the amount is incremented; otherwise, a new ShoppingCartItem object is created and added to the database context.

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem = _bethanysPieShopDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _bethanysPieShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _bethanysPieShopDbContext.SaveChanges();

            return localAmount;
        } // Removes a pie from the shopping cart. If the pie is in the cart and its amount is greater than 1, the amount is decremented; otherwise, the ShoppingCartItem object is removed from the database context.


        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= 
                        _bethanysPieShopDbContext.ShoppingCartItems.Where(c => 
                        c.ShoppingCartId == ShoppingCartId)
                            .Include(s => s.Pie)
                            .ToList();
        } // Retrieves the shopping cart items for the current cart ID from the database context and returns them. If the ShoppingCartItems property is null, the items are retrieved from the database context and assigned to the property.

        public void ClearCart()
        {
            var cartItems = _bethanysPieShopDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _bethanysPieShopDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _bethanysPieShopDbContext.SaveChanges();
        } // Removes all shopping cart items for the current cart ID from the database context.

        public decimal GetShoppingCartTotal()
        {
            var total = _bethanysPieShopDbContext.ShoppingCartItems.Where(c => 
                c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        } // Calculates the total price of all shopping cart items for the current cart ID and returns the total.
    }
}
