using NUnit.Framework;

namespace Tutorial.Business.Test.ProductManagement
{
    [TestFixture]
    public class CreateCart : ProductManagementTestBase
    {
        [TestCase(null)]
        [TestCase(" ")]
        public void GIVEN_there_are_no_carts__WHEN_user_creates_a_cart_with_null_or_white_space_name__THEN_the_system_gives_an_error(string nullOrWhiteSpace)
        {
            BeginTest();

            Assert.Throws<Exception>(() => CreateCart(userName: nullOrWhiteSpace));
        }
    }
}