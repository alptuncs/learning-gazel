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

        protected Product CreateProduct(
            string name = "Test Product",
            Money price = default,
            int stock = int.MaxValue,
            bool available = true
            )
        {
            if (price.IsDefault()) price = 10.TRY();

            var product = Context.Get<ProductManager>().CreateProduct(name, price, stock);

            if (!available)
            {
                product.MakeUnavailable();
            }

            return product;
        }

        protected Product CreateRandomProduct(bool zeroStock = false)
        {
            var randGen = new Random();
            var name = Guid.NewGuid().ToString();
            var price = randGen.Next(10, 30).TRY();
            var stock = zeroStock ? 0 : randGen.Next(1, 30);

            return CreateProduct(name, price, stock);
        }

        protected Cart CreateCart(
            string userName = "Test User",
            bool empty = true,
            bool purchased = false,
            bool withAProductMoreThanItsStock = false,
            params Product[] products
        )
        {
            var cart = Context.Get<ProductManager>().CreateCart(userName);

            if (!empty)
            {
                cart.AddProduct(CreateRandomProduct());
                cart.AddProduct(CreateRandomProduct());
                cart.AddProduct(CreateRandomProduct());
            }

            if (withAProductMoreThanItsStock)
            {
                var product = CreateRandomProduct();
                cart.AddProduct(product, product.Stock + 1);

            }

            foreach (var product in products)
            {
                cart.AddProduct(product);
            }

            if (purchased)
            {
                cart.Purchase();
            }

            return cart;
        }
    }
}
