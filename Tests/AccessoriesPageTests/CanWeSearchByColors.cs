using Framework.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.AccessoriesPageTests
{
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    public class CanWeSearchByColors : MainTest
    {
        [Test]
        public void Can_We_Search_By_Color()
        {
            AccessoriesPage accessoriesPage = new AccessoriesPage(driver.GetDriver());
            accessoriesPage.GoTo();

            Assert.True(accessoriesPage.CanWeSearchByColors(), "We cannot search by colors");
        }
    }
}
