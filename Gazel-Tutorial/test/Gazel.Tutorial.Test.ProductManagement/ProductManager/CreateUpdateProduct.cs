using Gazel.Tutorial.Test.ProductManagement;
using NUnit.Framework;

namespace Inventiv.ToDo.Test.UnitTest.TaskManagement
{
    [TestFixture]
    public class CreateUpdateProduct : ProductManagementTestBase
    {
        [TestCase("Capri Sun", 5.99F, 20)]
        [TestCase("Çerezza Kokteyl", 12.49F, 15)]
        public void GIVEN_there_exists_a_product_manager__WHEN_user_creates_a_product_with_name_price_and_stock__THEN_the_product_is_created_and_added_to_database(string name, float price, int stock)
        {
            BeginTest();

            var actual = CreateProduct(name, price, stock);

            Verify.ObjectIsPersisted(actual);
            Assert.AreEqual(price, actual.Price, "Price assignment is wrong");
            Assert.AreEqual(stock, actual.Stock, "Stock assignment is wrong");
            Assert.AreEqual(name, actual.ProductName, "Name assignment is wrong");
        }

        [TestCase("Updated Product Name")]
        public void GIVEN_there_exists_a_product__WHEN_user_updates_product_name__Then_name_changes_but_product_price_and_stock_does_not_change(string name)
        {
            var product = CreateProduct();
            var price = product.Price;
            var stock = product.Stock;
            var productName = product.ProductName;

            BeginTest();

            product.UpdateProduct(name);

            Assert.AreNotEqual(productName, product.ProductName, "ProductName has not changed");
            Assert.AreEqual(price, product.Price, "Price has changed");
            Assert.AreEqual(stock, product.Stock, "Stock has changed");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void GIVEN_there_exists_a_product__WHEN_user_updates_product_name_with_empty_name_or_null__THEN_product_name_does_not_change(string name)
        {
            var product = CreateProduct();
            var productName = product.ProductName;

            BeginTest();

            product.UpdateProduct(name);

            Assert.AreEqual(productName, product.ProductName, "Product name has changed, when it shouldn't have");
        }
    }
}
