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

        protected Product CreateProduct(string name = "Test Product", Money price = default, int stock = int.MaxValue, bool available = true)
        {
            if (price.IsDefault()) price = 10.TRY();

            var product = Context.Get<ProductManager>().CreateProduct(name, price, stock);

            if (!available)
            {
                product.MakeUnavailable();
            }

            return product;
        }

        protected Cart CreateCart(
            string userName = "Test User",
            bool empty = true,
            bool purchased = false,
            bool haveMoreThanStock = false,
            params Product[] products
        )
        {
            var cart = Context.Get<ProductManager>().CreateCart(userName);

            if (!empty)
            {
                cart.AddProduct(CreateProduct(name: "first"));
                cart.AddProduct(CreateProduct(name: "second"));
                cart.AddProduct(CreateProduct(name: "third"));
            }

            foreach (var product in products)
            {
                if (haveMoreThanStock)
                {
                    cart.AddProduct(product, product.Stock + 1);
                }
                else
                {
                    cart.AddProduct(product);
                }
            }

            if (purchased)
            {
                cart.Purchase();
            }

            return cart;
        }
    }
}
