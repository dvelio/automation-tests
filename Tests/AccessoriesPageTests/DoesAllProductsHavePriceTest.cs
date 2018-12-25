using Framework.Pages;
using NUnit.Framework;

namespace Tests.AccessoriesPageTests
{
    [TestFixture,Parallelizable(ParallelScope.Fixtures)]
    public class DoesAllProductsHavePriceTest : MainTest
    {
        [Test]
        public void Does_All_Products_Have_Price()
        {
            AccessoriesPage accessoriesPage = new AccessoriesPage(driver.GetDriver());
            accessoriesPage.GoTo();
            accessoriesPage.AcceptCookie();

            Assert.True(accessoriesPage.DoesAllLoadedProductsHavePrice(), "Some products may don't contain price value");
        }
    }
}
