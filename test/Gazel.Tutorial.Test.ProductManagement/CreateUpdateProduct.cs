using NUnit.Framework;

namespace Gazel.Tutorial.Test.ProductManagement
{
    [TestFixture]
    public class CreateUpdateProduct : ProductManagementTestBase
    {
        [Test]
        public void GIVEN_there_are_no_products__WHEN_user_creates_a_product_with_name__price_and_stock__THEN_the_product_is_created()
        {
            BeginTest();

            var actual = productManager.CreateProduct("Çerezza", 3.TRY(), 100);

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual("Çerezza", actual.Name);
            Assert.AreEqual(3.TRY(), actual.Price);
            Assert.AreEqual(100, actual.Stock);
        }

        [Test]
        public void GIVEN_there_exists_a_product_within_carts__WHEN_its_price_is_revised_THEN_new_price_should_be_reflected_to_all_unpurchased_carts()
        {
            var product = CreateProduct(price: 3.TRY());
            var unpurchased = CreateCart(products: new[] { product });
            var purchased = CreateCart(purchased: true, products: new[] { product });

            BeginTest();

            var actual = product.RevisePrice(4.TRY());

            Assert.AreEqual(4.TRY(), actual.Price);
            Assert.AreEqual(4.TRY(), unpurchased.GetCartItems().First().Product.Price);
            Assert.AreEqual(3.TRY(), purchased.GetCartItems().First().Product.Price);
        }
    }
}
