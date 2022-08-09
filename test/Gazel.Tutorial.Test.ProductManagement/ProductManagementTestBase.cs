using Gazel.Tutorial.Module.ProductManagement;
using Gazel.UnitTesting;

namespace Gazel.Tutorial.Test.ProductManagement
{
    public abstract class ProductManagementTestBase : TestBase
    {
        static ProductManagementTestBase()
        {
            Config.RootNamespace = "Gazel";
        }

        protected ProductManager productManager;

        public override void SetUp()
        {
            base.SetUp();

            productManager = Context.Get<ProductManager>();
        }

        protected Product CreateProduct(string name = "Test Product", Money price = default, int stock = int.MaxValue)
        {
            if (price.IsDefault()) price = 10.TRY();

            var product = Context.Get<ProductManager>().CreateProduct(name, price, stock);

            return product;
        }

        protected Cart CreateCart(
            string userName = "Test User",
            bool empty = true,
            bool purchased = false,
            params Product[] products
        )
        {
            var cart = Context.Get<ProductManager>().CreateCart(userName);

            if (!empty)
            {
                cart.AddToCart(CreateProduct());
                cart.AddToCart(CreateProduct());
                cart.AddToCart(CreateProduct());
            }

            foreach (var product in products)
            {
                cart.AddToCart(product);
            }

            if (purchased)
            {
                cart.CompletePurchase();
            }

            return cart;
        }
    }
}
