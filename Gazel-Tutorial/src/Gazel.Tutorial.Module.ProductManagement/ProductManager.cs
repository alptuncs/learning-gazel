using Gazel;
using Gazel.Tutorial.Module.ProductManagement.Service;

namespace Gazel.Tutorial.Module.ProductManagement
{
    public class ProductManager : IProductManagerService
    {
        private readonly IModuleContext context;

        public ProductManager(IModuleContext context)
        {
            this.context = context;
        }

        public Product CreateProduct(string name, float price, int stock)
        {
            return context.New<Product>().With(name, price, stock);
        }

        public void DeleteProduct(Product product)
        {
            context.Query<CartItems>().ByProduct(product).ForEach(t => t.Cart.RemoveFromCart(product));

            product.RemoveProduct();
        }

        public void UpdateProduct(Product product, string name, float price, int stock)
        {
            product.UpdateProduct(name, price, stock);
        }
        public CartItem CreateCartItem(Product product, Cart cart, int amount)
        {
            return context.New<CartItem>().With(product, cart, amount);
        }

        public void DeleteCartItem(CartItem cartItem)
        {
            cartItem.RemoveCartItem();
        }

        public Cart CreateCart(string userName)
        {
            return context.New<Cart>().With(userName);
        }

        public void AddProductToCart(Product product, int amount, Cart cart)
        {
            if (amount > product.Stock) throw new Exception($"There are {product.Stock} amount of products in stock, can't add {amount} amount to your cart");

            cart.AddToCart(product, amount);
        }

        public void RemoveProductFromCart(Product product, Cart cart)
        {
            cart.RemoveFromCart(product);
        }

        public void RemoveAllProductsFromCart(Cart cart)
        {
            cart.EmptyCart();
        }

        public void DeleteCart(Cart cart)
        {
            RemoveAllProductsFromCart(cart);
            cart.DeleteCart();
        }
    }
}
