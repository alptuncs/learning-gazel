using Gazel.Tutorial.Test.ProductManagement;
using NUnit.Framework;

namespace Inventiv.ToDo.Test.UnitTest.TaskManagement
{
    [TestFixture]
    public class AddRemoveProduct : ProductManagementTestBase
    {
        [TestCase(5)]
        [TestCase(1)]
        public void GIVEN_there_exists_a_product__WHEN_user_adds_certain_amount_of_that_product_to_cart__THEN_product_is_added_to_cart(int amount)
        {
            var product = CreateProduct();
            var cart = CreateCart();

            BeginTest();

            productManager.AddProductToCart(product, amount, cart);

            Assert.AreEqual(1, cart.GetCartItems().Count, "Product has not been added to Cart");
        }

        [Test]
        public void GIVEN_there_exists_a_product__WHEN_user_adds_more_than_stock_to_cart__THEN_system_gives_an_error()
        {
            var product = CreateProduct();
            var cart = CreateCart();

            BeginTest();

            Assert.Throws<Exception>(() => productManager.AddProductToCart(product, product.Stock + 1, cart));
        }

        [Test]
        public void GIVEN_there_exists_a_product_in_cart__WHEN_user_removes_product_from_cart__THEN_product_is_removed_from_cart()
        {
            var product = CreateProduct();
            var cart = CreateCart();

            productManager.AddProductToCart(product, 2, cart);

            BeginTest();

            productManager.RemoveProductFromCart(product, cart);

            Assert.AreEqual(0, cart.GetCartItems().Count, "Product has not been removed from Cart");
        }

        [Test]
        public void GIVEN_there_exists_products_in_cart__WHEN_user_empties_his_cart__THEN_all_products_are_removed_from_cart()
        {
            var firstProduct = CreateProduct("Çerezza");
            var secondProduct = CreateProduct("Capri Sun");
            var thirdProduct = CreateProduct("Lays");

            var cart = CreateCart();

            productManager.AddProductToCart(firstProduct, 2, cart);
            productManager.AddProductToCart(secondProduct, 2, cart);
            productManager.AddProductToCart(thirdProduct, 2, cart);

            BeginTest();

            productManager.RemoveAllProductsFromCart(cart);

            Assert.AreEqual(0, cart.GetCartItems().Count, "Products have not been removed from Cart");
        }
    }
}