using Gazel;
using Gazel.UnitTesting;
using Gazel.Tutorial.Module.ProductManagement;
using NUnit.Framework;

namespace Gazel.Tutorial.Test.ProductManagement
{
    [TestFixture]
    public class CartItemTests : TestBase
    {
        static CartItemTests()
        {
            Config.RootNamespace = "Inventiv";
        }

        [Test]
        public void CreateCartItem__creates_a_cart_item_using_given_product()
        {
            var productManager = Context.Get<ProductManager>();
            var product = productManager.CreateProduct("Cips", 13.99F, 10);

            BeginTest();

            var actual = productManager.CreateCartItem(product, 10, 1);

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual("Cips", actual.CartProduct.Name);
        }

        [Test]
        public void CreateCartItem__creates_a_cart_item_using_given_amount()
        {
            var productManager = Context.Get<ProductManager>();
            var product = productManager.CreateProduct("Cips", 13.99F, 10);

            BeginTest();

            var actual = productManager.CreateCartItem(product, 10, 1);

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual(10, actual.Amount);
        }

        [Test]
        public void CartItems_ByCartId__filters_cart_items_by_their_cart_id()
        {
            var productManager = Context.Get<ProductManager>();
            var product = productManager.CreateProduct("Cips", 13.99F, 10);
            productManager.CreateCartItem(product, 10, 1);

            BeginTest();

            var actual = Context.Query<CartItems>.ByCartId(1);

            Assert.AreEqual(14.99F, actual[0].Price);
        }
    }
}