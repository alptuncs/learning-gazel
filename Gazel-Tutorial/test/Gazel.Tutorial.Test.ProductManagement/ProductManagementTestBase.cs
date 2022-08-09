using Gazel.Tutorial.Module.ProductManagement;
using Gazel.Tutorial.Module.ProductManagement.Service;
using Gazel.UnitTesting;
using NUnit.Framework;

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

        protected Cart CreateCart(string userName = "Test User")
        {
            var cart = Context.Get<ProductManager>().CreateCart(userName);

            return cart;
        }
    }
}