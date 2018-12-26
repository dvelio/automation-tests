using IDriver;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Tests
{
    public class MainTest
    {
        public Driver driver = new Driver();

        [OneTimeSetUp]
        public void Setup()
        {
            driver.SetDriver();
            driver.Maximize();
        }

        [OneTimeTearDown]
        public void Clean()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                driver.TakeScreenshot(TestContext.CurrentContext.Test.Name);

            driver.CleanUpDriver();

        }
    }
}
