using NUnit.Framework;

namespace Tutorial.Business.Test.ProductManagement
{
    [TestFixture]
    public class CompletePurchase : ProductManagementTestBase
    {
        [Test]
        public void GIVEN_there_exists_a_cart__WHEN_user_completes_purchase__THEN_purchase_record_is_created_with_cart()
        {
            var cart = CreateCart(empty: false);

            BeginTest();

            var purchase = cart.Purchase();

            Verify.ObjectIsPersisted(purchase);
            Assert.AreEqual(cart, cart.GetPurchaseRecord().Cart);
        }

        [Test]
        public void GIVEN_there_exists_a_cart__WHEN_user_completes_purchase__THEN_product_stock_is_updated()
        {
            var product = CreateProduct("test Product", 4.55.TRY(), 1);
            var cart = CreateCart(products: new[] { product });

            BeginTest();

            cart.Purchase();

            Assert.AreEqual(0, product.Stock);
        }

        [Test]
        public void GIVEN_there_exists_a_product_with_higher_amount_than_stock_in_cart__WHEN_user_tries_to_purchase__THEN_system_gives_an_error()
        {
            var cart = CreateCart(withAProductMoreThanItsStock: true);

            BeginTest();

            Assert.Throws<Exception>(() => cart.Purchase());
        }
    }
}
