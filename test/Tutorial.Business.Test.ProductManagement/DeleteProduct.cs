using NUnit.Framework;

namespace Tutorial.Business.Test.ProductManagement
{
    [TestFixture]
    public class DeleteProduct : ProductManagementTestBase
    {
        [Test]
        public void GIVEN_there_exists_a_product_within_carts__WHEN_product_is_made_unavailable__THEN_it_is_only_removed_from_non_purchased_carts()
        {
            var product = CreateProduct();
            var cart = CreateCart(purchased: false, products: new[] { product });
            var purchased = CreateCart(purchased: true, products: new[] { product });

            BeginTest();

            product.MakeUnavailable();

            Assert.IsEmpty(cart.GetCartItems());
            Assert.AreEqual(product, purchased.GetCartItems().First().Product);
        }
    }
}

