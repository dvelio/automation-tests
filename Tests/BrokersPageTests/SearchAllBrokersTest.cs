using Framework.Pages;
using NUnit.Framework;

namespace Tests.BrokersPageTests
{
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    public class SearchAllBrokersTest : MainTest
    {
        [Test]
        public void Search_All_Brokers()
        {
            BrokersPage brokersPage = new BrokersPage(driver.GetDriver());
            brokersPage.GoTo();
            brokersPage.AcceptCookie();
            brokersPage.ExpandBrokersList();

            Assert.True(brokersPage.CanSearchAllBrokers(), "We cannot search all brokers or in some broker there is a missing info(tels,adress,number of property's"); 
        }
    }
}
