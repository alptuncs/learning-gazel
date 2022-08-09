using Gazel.Tutorial.Test.ProductManagement;
using NUnit.Framework;

namespace Inventiv.ToDo.Test.UnitTest.TaskManagement
{
    [TestFixture]
    public class AddRemoveProduct : ProductManagementTestBase
    {
        [TestCase(5)]
        [TestCase(1)]
        public void GIVEN_there_exists_a_product__WHEN_user_adds_certain_amount_of_that_product_to_cart__THEN_product_is_added_to_cart_with_given_amount(int amount)
        {
            var product = CreateProduct();
            var cart = CreateCart();

            BeginTest();

            cart.AddToCart(product, amount);

            Assert.AreEqual(1, cart.GetCartItems().Count, "Product has not been added to Cart");
            Assert.AreEqual(amount, cart.GetCartItems()[0].Amount, "Product has not been added to Cart");
        }

        [Test]
        public void GIVEN_there_exists_a_product__WHEN_user_adds_certain_product_to_cart_without_giving_amount__THEN_product_is_added_to_cart()
        {
            var product = CreateProduct();
            var cart = CreateCart();

            BeginTest();

            cart.AddToCart(product);

            Assert.AreEqual(1, cart.GetCartItems().Count, "Product has not been added to Cart");
        }

        [Test]
        public void GIVEN_there_exists_a_product__WHEN_user_adds_more_than_stock_to_cart__THEN_system_gives_an_error()
        {
            var product = CreateProduct("test", 4.99F, 20);
            var cart = CreateCart();

            BeginTest();

            Assert.Throws<Exception>(() => cart.AddToCart(product, 21));
        }

        [Test]
        public void GIVEN_there_exists_a_product_in_cart__WHEN_user_removes_product_from_cart__THEN_product_is_removed_from_cart()
        {
            var product = CreateProduct();
            var cart = CreateCart();

            cart.AddToCart(product);

            BeginTest();

            cart.RemoveFromCart(product);

            Assert.IsEmpty(cart.GetCartItems(), "Product has not been removed from Cart");
        }

        [Test]
        public void GIVEN_there_exists_products_in_cart__WHEN_user_empties_his_cart__THEN_all_products_are_removed_from_cart()
        {
            var firstProduct = CreateProduct("first");
            var secondProduct = CreateProduct("second");
            var thirdProduct = CreateProduct("third");

            var cart = CreateCart();

            cart.AddToCart(firstProduct);
            cart.AddToCart(secondProduct);
            cart.AddToCart(thirdProduct);

            BeginTest();

            cart.RemoveAllProducts();

            Assert.IsEmpty(cart.GetCartItems(), "Products have not been removed from Cart");
        }
    }
}