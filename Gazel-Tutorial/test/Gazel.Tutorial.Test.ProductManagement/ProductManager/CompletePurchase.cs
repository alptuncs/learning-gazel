using Gazel.Tutorial.Test.ProductManagement;
using NUnit.Framework;

namespace Inventiv.ToDo.Test.UnitTest.TaskManagement
{
    [TestFixture]
    public class CompletePurchase : ProductManagementTestBase
    {
        [Test]
        public void GIVEN_there_exists_a_cart__WHEN_user_completes_purchase__THEN_purchase_record_is_created_with_cart()
        {
            var cart = CreateCart();
            cart.AddToCart(CreateProduct());

            BeginTest();

            var purchase = cart.CompletePurchase();

            Verify.ObjectIsPersisted(purchase);
            Assert.IsNotEmpty(cart.GetPurchaseRecords());
        }

        public void GIVEN_there_exists_a_cart__WHEN_user_completes_purchase__THEN_product_stock_is_updated()
        {
            var product = CreateProduct("test Product", 4.55F, 1);
            var cart = CreateCart();

            cart.AddToCart(product);

            BeginTest();

            cart.CompletePurchase();

            Assert.AreNotEqual(1, product.Stock, $"product stock = {product.Stock}");
        }
    }
}
