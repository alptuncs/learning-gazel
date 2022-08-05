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
            Config.RootNamespace = "Gazel";
        }

        [Test]
        public void CreateCartItem__creates_a_cartitem_using_given_productid()
        {
            var productManager = Context.Get<ProductManager>();
            var product = productManager.CreateProduct("Cips", 10.25F, 10);
            var productId = 1;

            BeginTest();

            var actual = productManager.CreateCartItem(productId, 1, 1);

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual(productId, actual.ProductInfo.Id);
        }

        [Test]
        public void CreateCartItem__creates_a_cartitem_using_given_amount()
        {
            var productManager = Context.Get<ProductManager>();
            var product = productManager.CreateProduct("Cips", 10.25F, 10);
            var amount = 100;

            BeginTest();

            var actual = productManager.CreateCartItem(1, 1, amount);

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual(amount, actual.Amount);
        }
    }
}