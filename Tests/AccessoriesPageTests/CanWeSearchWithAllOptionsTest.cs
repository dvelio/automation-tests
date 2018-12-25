using Framework.Pages;
using NUnit.Framework;


namespace Tests.AccessoriesPageTests
{
    [TestFixture,Parallelizable(ParallelScope.Fixtures)]
    public class CanWeSearchWithAllOptionsTest : MainTest
    {
        [Test]
        public void Can_We_Seach_With_All_Options()
        {
            AccessoriesPage accessoriesPage = new AccessoriesPage(driver.GetDriver());
            accessoriesPage.GoTo();
            accessoriesPage.AcceptCookie();

            Assert.True(accessoriesPage.CanWeSearchByAllCheckboxOptions(), "We cannot search by all options");
        }
    }
}
