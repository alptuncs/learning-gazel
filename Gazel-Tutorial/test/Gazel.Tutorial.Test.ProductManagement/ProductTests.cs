using Gazel;
using Gazel.UnitTesting;
using Gazel.Tutorial.Module.ProductManagement;
using NUnit.Framework;

namespace Gazel.Tutorial.Test.ProductManagement
{
    [TestFixture]
    public class ProductTests : TestBase
    {
        static ProductTests()
        {
            Config.RootNamespace = "Gazel";
        }

        [Test]
        public void CreateProduct__creates_a_product_using_given_name()
        {
            var productManager = Context.Get<ProductManager>();

            BeginTest();

            var actual = productManager.CreateProduct("Cips", 10.25F, 10);

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual("Cips", actual.ProductName);
        }

        [Test]
        public void CreateProduct__creates_a_product_using_given_price()
        {
            var productManager = Context.Get<ProductManager>();

            BeginTest();

            var actual = productManager.CreateProduct("Cips", 10.25F, 10);

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual(10.25F, actual.Price);
        }

        [Test]
        public void CreateProduct__creates_a_product_using_given_stock()
        {
            var productManager = Context.Get<ProductManager>();

            BeginTest();

            var actual = productManager.CreateProduct("Cips", 10.25F, 10);

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual(10, actual.Stock);
        }

        [Test]
        public void UpdateProductName__updates_product_name()
        {
            var productManager = Context.Get<ProductManager>();
            var product = productManager.CreateProduct("Cips", 10.25f, 10);
            var updatedName = "Pringles";

            BeginTest();

            product.UpdateProductName(updatedName);

            Assert.AreEqual(updatedName, product.ProductName);
        }

        [Test]
        public void UpdateProductPrice__updates_product_price()
        {
            var productManager = Context.Get<ProductManager>();
            var product = productManager.CreateProduct("Cips", 10.25f, 10);
            var updatedPrice = 11.99F;

            BeginTest();

            product.UpdateProductPrice(updatedPrice);

            Assert.AreEqual(updatedPrice, product.Price);
        }

        [Test]
        public void UpdateProductStock__updates_product_stock()
        {
            var productManager = Context.Get<ProductManager>();
            var product = productManager.CreateProduct("Cips", 10.25f, 10);
            var updatedStock = 20;

            BeginTest();

            product.UpdateProductStock(updatedStock);

            Assert.AreEqual(updatedStock, product.Stock);
        }

        [Test]
        public void Products_ByName__filters_products_by_name_column()
        {
            var productManager = Context.Get<ProductManager>();
            var productName = "Çerezza Kokteyl";

            productManager.CreateProduct(productName, 10, 10);
            productManager.CreateProduct("Pringles", 20, 10);

            BeginTest();

            var actual = Context.Query<Products>().ByName(productName);

            Assert.AreEqual(productName, actual[0].ProductName);
        }

        [Test]
        public void Products_ById__returns_product_with_given_id()
        {
            var productManager = Context.Get<ProductManager>();
            var productId = 1;

            productManager.CreateProduct("Lays", 15.99F, 0);
            productManager.CreateProduct("Coca Cola", 9.99F, 10);

            BeginTest();

            var actual = Context.Query<Products>().ById(productId);

            Assert.AreEqual(productId, actual.Id);
        }

        [Test]
        public void Products_ByPositiveStock__returns_products_with_positive_stock()
        {
            var productManager = Context.Get<ProductManager>();

            productManager.CreateProduct("Lays", 15.99F, 0);
            productManager.CreateProduct("Coca Cola", 9.99F, 10);

            BeginTest();

            var actual = Context.Query<Products>().ByPositiveStock();

            Assert.AreEqual(1, actual.Count());
        }

        [Test]
        public void Products_ByPriceHigherThan__returns_products_with_price_higher_than_given_price()
        {
            var productManager = Context.Get<ProductManager>();
            var priceLowerBound = 9.99F;

            productManager.CreateProduct("Lays", 15.99F, 0);
            productManager.CreateProduct("Coca Cola", 9.99F, 10);

            BeginTest();

            var actual = Context.Query<Products>().ByPriceHigherThan(priceLowerBound);

            Assert.AreEqual(2, actual.Count());
        }

        [Test]
        public void Products_ByPriceLowerThan__returns_products_with_price_lower_than_given_price()
        {
            var productManager = Context.Get<ProductManager>();
            var priceUpperBound = 20F;

            productManager.CreateProduct("Lays", 15.99F, 0);
            productManager.CreateProduct("Coca Cola", 9.99F, 10);

            BeginTest();

            var actual = Context.Query<Products>().ByPriceLowerThan(priceUpperBound);

            Assert.AreEqual(2, actual.Count());
        }

        [Test]
        public void Products_ByPriceRange__returns_products_within_price_range()
        {
            var productManager = Context.Get<ProductManager>();
            var priceUpperBound = 20F;
            var priceLowerBound = 10F;

            productManager.CreateProduct("Lays", 15.99F, 0);
            productManager.CreateProduct("Coca Cola", 9.99F, 10);

            BeginTest();

            var actual = Context.Query<Products>().ByPriceRange(priceLowerBound, priceUpperBound);

            Assert.AreEqual(1, actual.Count());
        }
    }
}