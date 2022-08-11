using NUnit.Framework;

namespace Tutorial.Business.Test.ProductManagement
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
        public void GIVEN_there_are_no_products__WHEN_user_creates_a_product_with_negative_stock__THEN_the_system_gives_an_error()
        {
            BeginTest();

            Assert.Throws<Exception>(() => productManager.CreateProduct("test", 1.TRY(), -1));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void GIVEN_there_are_no_products__WHEN_user_creates_a_product_with_non_positive_price__THEN_the_system_gives_an_error(int nonPositivePrice)
        {
            BeginTest();

            Assert.Throws<Exception>(() => productManager.CreateProduct("test", nonPositivePrice.TRY(), 1));
        }

        [TestCase(null)]
        [TestCase(" ")]
        public void GIVEN_there_are_no_products__WHEN_user_creates_a_product_with_null_or_white_space_name__THEN_the_system_gives_an_error(string nullOrWhiteSpace)
        {
            BeginTest();

            Assert.Throws<Exception>(() => productManager.CreateProduct(name: nullOrWhiteSpace, 1.TRY(), 1));
        }

        [Test]
        public void GIVEN_there_exists_an_available_product__WHEN_user_creates_a_product_with_same_name__THEN_the_system_gives_an_error()
        {
            var availableProduct = CreateProduct(name: "availableProduct", available: true);

            BeginTest();

            Assert.Throws<Exception>(() => productManager.CreateProduct(name: "availableProduct", 1.TRY(), 1));
        }

        [Test]
        public void GIVEN_there_exists_an_unavailable_product__WHEN_user_creates_a_product_with_same_name__THEN_the_product_is_created()
        {
            var unavailableProduct = CreateProduct(name: "unavailableProduct", available: false);

            BeginTest();

            var actual = CreateProduct(name: "unavailableProduct");

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual(unavailableProduct.Name, actual.Name);
        }

        [Test]
        public void GIVEN_there_exists_a_product_within_carts__WHEN_its_price_is_revised_THEN_new_price_should_be_reflected_to_all_unpurchased_carts()
        {
            var product = CreateProduct();
            var unpurchased = CreateCart(products: new[] { product });
            var purchased = CreateCart(purchased: true, products: new[] { product });

            BeginTest();

            var newPrice = product.Price + 1.TRY();
            var actual = product.RevisePrice(newPrice);


            Assert.AreEqual(newPrice, actual.Price);
            Assert.AreEqual(newPrice, unpurchased.GetCartItems().First().Product.Price);
            Assert.AreEqual(product.Price, purchased.GetCartItems().First().Product.Price);
        }
    }
}
