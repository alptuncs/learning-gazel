using NUnit.Framework;

namespace Gazel.Tutorial.Test.ProductManagement
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

            firstCart.AddProduct(product);
            secondCart.AddProduct(product);

            BeginTest();

            product.RemoveProduct();


            Assert.IsEmpty(firstCart.GetCartItems(), "Product did not get removed from first cart");
            Assert.IsEmpty(secondCart.GetCartItems(), "Product did not get removed from first cart");
        }
    }
}
