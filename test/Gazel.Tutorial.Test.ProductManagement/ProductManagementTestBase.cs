using Gazel.Tutorial.Module.ProductManagement;
using Gazel.UnitTesting;

namespace Gazel.Tutorial.Test.ProductManagement
{
    public abstract class ProductManagementTestBase : TestBase
    {
        protected ProductManager productManager;

        static ProductManagementTestBase()
        {
            Config.RootNamespace = "Gazel";
        }

        public override void SetUp()
        {
            base.SetUp();

            productManager = Context.Get<ProductManager>();
        }

        protected Product CreateProduct(string name = "Test Product", float price = 4.99F, int stock = int.MaxValue)
        {
            var product = Context.Get<ProductManager>().CreateProduct(name, price, stock);

            return product;
        }

        protected Cart CreateCart(string userName = "Test User", bool empty = true, params Product[] products)
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

            return cart;
        }
    }
}
