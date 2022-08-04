using Gazel;
using Gazel.UnitTesting;
using Gazel.Tutorial.Module.ProductManagement;
using NUnit.Framework;

namespace Gazel.Tutorial.Test.ProductManagement
{
    [TestFixture]
    public class CartItemTests : TestBase
    {
        static CartItemTests()
        {
            Config.RootNamespace = "Inventiv";
        }
    }
}