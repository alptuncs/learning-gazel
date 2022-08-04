using Gazel;
using Gazel.UnitTesting;
using Gazel.Tutorial.Module.ProductManagement;
using NUnit.Framework;

namespace Gazel.Tutorial.Test.ProductManagement
{
    [TestFixture]
    public class CartTests : TestBase
    {
        static CartTests()
        {
            Config.RootNamespace = "Inventiv";
        }

        [Test]
        public void CreateCart__creates_a_cart_using_given_customer_name()
        {
            var productManager = Context.Get<ProductManager>();

            BeginTest();

            var actual = productManager.CreateCart("CustomerName");

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual("CustomerName", actual.CustomerName);
        }

        [Test]
        public void AddToCart__adds_given_cartitem_to_cart()
        {
            var productManager = Context.Get<ProductManager>();
            var product = productManager.CreateProduct("Cips", 13.99F, 10);

            BeginTest();

            var actual = productManager.CreateCart("CustomerName");
            actual.AddToCart(product, 5);

            Assert.AreEqual(product.Name, actual.CartItems[0].Name);
        }

        [Test]
        public void AddToCart__can_not_add_given_cartitem_to_cart_if_amount_is_greater_than_product_stock()
        {
            var productManager = Context.Get<ProductManager>();
            var product = productManager.CreateProduct("Cips", 13.99F, 10);

            BeginTest();

            var actual = productManager.CreateCart("CustomerName");
            
            Assert.Throws<InvalidOperationException>(() => actual.AddToCart(product, 20));
        }

        [Test]
        public void CalculateTotalCost__calculates_total_cost_of_items_in_cart()
        {
            var productManager = Context.Get<ProductManager>();
            var product = productManager.CreateProduct("Cips", 13.99F, 10);
            var cartItem = productManager.CreateCartItem(product, 8);

            BeginTest();

            var actual = productManager.CreateCart("CustomerName");
            actual.AddToCart(cartItem);
            actual.CalculateTotalCost();

            Assert.AreEqual(111, 92F, actual.TotalCost);
        }
    }
}
