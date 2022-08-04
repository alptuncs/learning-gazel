using Gazel;
using Gazel.UnitTesting;
using Gazel.Tutorial.Module.ProductManagement;
using NUnit.Framework;

namespace Inventiv.Todo.Test.Products
{
    [TestFixture]
    public class TaskTest : TestBase
    {
        static TaskTest()
        {
            Config.RootNamespace = "Inventiv";
        }

        public void CreateProduct__creates_a_product_using_given_name()
        {
            var productManager = Context.Get<ProductManager>();

            BeginTest();

            var actual = productManager.CreateProduct("Cips", 10.25F, 125);

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual("Cips", actual.Name);
        }

        [Test]
        public void CreateProduct__creates_a_product_using_given_price()
        {
            var productManager = Context.Get<ProductManager>();

            BeginTest();

            var actual = productManager.CreateProduct("Cips", 10.25F, 125);

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual(10.25F, actual.Price);
        }

        [Test]
        public void CreateProduct__creates_a_product_using_given_stock()
        {
            var productManager = Context.Get<ProductManager>();

            BeginTest();

            var actual = productManager.CreateProduct("Cips", 10.25F, 125);

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual(125, actual.Stock);
        }

        [Test]
        public void Products_ByPrice__filters_products_by_price()
        {
            var productManager = Context.Get<ProductManager>();

            productManager.CreateProduct("Cips", 14.99F, 10);

            BeginTest();

            var actual = Context.Query<Products>.ByPrice(14.99F);

            Assert.AreEqual(14.99F, actual[0].Price);
        }

        [Test]
        public void Products_ByStock__filters_products_by_stock()
        {
            var productManager = Context.Get<ProductManager>();

            productManager.CreateProduct("Cips", 14.99F, 10);

            BeginTest();

            var actual = Context.Query<Products>.ByStock(true);

            Assert.AreEqual(true, actual[0].StockInfo);

        }
        [Test]
        public void Products_ByStock__filters_products_by_id()
        {
            var productManager = Context.Get<ProductManager>();

            productManager.CreateProduct("Cips", 14.99F, 10);

            BeginTest();

            var actual = Context.Query<Products>.ByStock(true);

            Assert.AreEqual(true, actual[0].StockInfo);

        }
    }
}