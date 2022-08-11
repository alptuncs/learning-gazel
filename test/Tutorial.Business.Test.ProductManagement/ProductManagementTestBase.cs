using Gazel;
using Gazel.UnitTesting;
using Tutorial.Business.Module.ProductManagement;

namespace Tutorial.Business.Test.ProductManagement
{
    public abstract class ProductManagementTestBase : TestBase
    {
        static ProductManagementTestBase()
        {
            Config.RootNamespace = "Tutorial";
        }

        protected ProductManager productManager;

        public override void SetUp()
        {
            base.SetUp();

            productManager = Context.Get<ProductManager>();
        }

        protected Product CreateProduct(
            string name = default,
            Money price = default,
            int stock = int.MaxValue,
            bool available = true
            )
        {
            if (price.IsDefault()) price = 10.TRY();

            if (name == default) name = Guid.NewGuid().ToString();

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
            bool withAProductMoreThanItsStock = false,
            params Product[] products
        )
        {
            var cart = Context.Get<ProductManager>().CreateCart(userName);

            if (!empty)
            {
                cart.AddProduct(CreateProduct());
                cart.AddProduct(CreateProduct());
                cart.AddProduct(CreateProduct());
            }

            if (withAProductMoreThanItsStock)
            {
                var product = CreateProduct(stock: 20);
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
