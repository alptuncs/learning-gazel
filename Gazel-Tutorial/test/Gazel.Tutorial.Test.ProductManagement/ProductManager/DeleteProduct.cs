using Gazel.Tutorial.Test.ProductManagement;
using NUnit.Framework;

namespace Inventiv.ToDo.Test.UnitTest.TaskManagement
{
    [TestFixture]
    public class DeleteProduct : ProductManagementTestBase
    {
        [Test]
        public void GIVEN_there_exists_a_product__WHEN_product_is_deleted__THEN_product_is_deleted_and_removed_from_any_cart_that_contains_the_product()
        {
            var product = CreateProduct();

            var firstCart = CreateCart("firstCart");
            var secondCart = CreateCart("secondCart");

            productManager.AddProductToCart(product, 2, firstCart);
            productManager.AddProductToCart(product, 2, secondCart);

            BeginTest();

            productManager.DeleteProduct(product);

            Verify.ObjectIsDeleted(product);
            Assert.AreEqual(0, firstCart.GetCartItems().Count, "Product did not get removed from first cart");
            Assert.AreEqual(0, secondCart.GetCartItems().Count, "Product did not get removed from first cart");
        }
    }
}
