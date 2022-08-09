using NUnit.Framework;

namespace Gazel.Tutorial.Test.ProductManagement
{
    [TestFixture]
    public class CompletePurchase : ProductManagementTestBase
    {
        [Test]
        public void GIVEN_there_exists_a_cart__WHEN_user_completes_purchase__THEN_purchase_record_is_created_with_cart()
        {
            var cart = CreateCart(empty: false);

            BeginTest();

            var purchase = cart.CompletePurchase();

            Verify.ObjectIsPersisted(purchase);
            Assert.AreEqual(cart, cart.GetPurchaseRecord().Cart);
        }

        [Test]
        public void GIVEN_there_exists_a_cart__WHEN_user_completes_purchase__THEN_product_stock_is_updated()
        {
            var product = CreateProduct("test Product", 4.55.TRY(), 1);
            var cart = CreateCart(products: new[] { product });

            BeginTest();

            cart.CompletePurchase();

            Assert.AreNotEqual(1, product.Stock, $"product stock = {product.Stock}");
        }

        [Test]
        public void GIVEN_there_exists_a_product__WHEN_user_adds_more_than_stock_to_cart__THEN_system_gives_an_error()
        {
            var product = CreateProduct(stock: 20);
            var cart = CreateCart();
            cart.AddToCart(product, 21);

            BeginTest();

            Assert.Throws<Exception>(() => cart.CompletePurchase());
        }
    }
}
