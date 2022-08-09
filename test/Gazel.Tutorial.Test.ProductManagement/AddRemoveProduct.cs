using NUnit.Framework;

namespace Gazel.Tutorial.Test.ProductManagement
{
    [TestFixture]
    public class AddRemoveProduct : ProductManagementTestBase
    {
        [Test]
        public void GIVEN_there_exists_a_product_and_a_cart_WHEN_user_adds_the_product_to_the_cart__THEN_the_product_shows_up_in_cart_with_the_amount_of_one()
        {
            var product = CreateProduct();
            var cart = CreateCart();

            BeginTest();

            cart.AddProduct(product);

            var actual = cart.GetCartItems().FirstOrDefault();

            Assert.IsNotNull(actual);
            Assert.AreEqual(product, actual?.Product);
            Assert.AreEqual(1, actual?.Amount);
        }

        [Test]
        public void GIVEN_there_exists_a_product_and_a_cart__WHEN_user_specifies_amount_while_adding_the_product__THEN_product_is_added_to_cart_with_the_given_amount()
        {
            var product = CreateProduct();
            var cart = CreateCart();

            BeginTest();

            cart.AddProduct(product, 2);

            var cartItem = cart.GetCartItems().First();

            Assert.AreEqual(2, cartItem.Amount);
        }

        [Test]
        public void GIVEN_there_exists_a_product_in_cart__WHEN_user_removes_product_from_cart__THEN_product_is_removed_from_cart()
        {
            var removedProduct = CreateProduct();
            var leftProduct = CreateProduct();
            var cart = CreateCart(products: new[] { removedProduct, leftProduct });

            BeginTest();

            cart.RemoveProduct(removedProduct);

            var items = cart.GetCartItems();

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(leftProduct, items.First().Product);
        }

        [Test]
        public void GIVEN_there_exists_a_non_empty_cart__WHEN_user_empties_it__THEN_all_products_are_removed()
        {
            var cart = CreateCart(empty: false);

            BeginTest();

            cart.RemoveAllProducts();

            Assert.IsEmpty(cart.GetCartItems());
        }
    }
}
