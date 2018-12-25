using Framework.Pages;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Tests.AccessoriesPageTests
{
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    public class AreAllLoadedProductsDisplayedCorrectly : MainTest
    {
        [Test]
        public void Are_All_Loaded_Products_Displayed_Correctly()
        {
            AccessoriesPage accessoriesPage = new AccessoriesPage(driver.GetDriver());
            accessoriesPage.GoTo();

            Assert.True(accessoriesPage.DoesAllProductsAreLoadedCorrectly(), "Missing elements in some products (colors,price,name");
        }
    }
}
