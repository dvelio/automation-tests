using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;

namespace IDriver
{
    public class Driver : IWebDriver
    {
        public IWebDriver driver;

        private ChromeOptions chromeOptions = new ChromeOptions();

        public ReadOnlyCollection<string> WindowHandles => throw new NotImplementedException();

        string IWebDriver.Url { get => driver.Url; set => throw new WebDriverException(); }

        string IWebDriver.Title => driver.Title;

        string IWebDriver.PageSource => driver.PageSource;

        string IWebDriver.CurrentWindowHandle => driver.CurrentWindowHandle;

        public Driver() { }

        private static string GetChromeNodeAdress()
        {
            var chromeNodeAdress = "http://172.18.232.177:1122/wd/hub";
            return chromeNodeAdress;
        }

        public void SetDriver()
        {
            driver = new ChromeDriver(SetChromeOptions(chromeOptions));
            //driver = new RemoteWebDriver(new Uri(GetChromeNodeAdress()) , chromeOptions);
        }

        public IWebDriver GetDriver()
        {
            return driver;
        }

        public ChromeOptions SetChromeOptions(ChromeOptions opt)
        {
            opt.AddArgument("--disable-extensions");
            opt.AddArgument("--disable-infobars");
            opt.AddArgument("--disable-popup-blocking");

            opt.ToCapabilities();

            return opt;
        }

        public void GoTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public IWebElement FindElementUntilClickable(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                var result = wait.Until(ExpectedConditions.ElementIsVisible(by));
                wait.Until(ExpectedConditions.ElementToBeClickable(by));
                return result;
            }
            catch (ElementNotSelectableException)
            {
                return null;
            }
        }

        public IWebElement FindElementUntillVisible(By byBy)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                var result = wait.Until(ExpectedConditions.ElementIsVisible(byBy));
                return result;
            }
            catch (ElementNotVisibleException)
            {
                return null;
            }
        }

        public void SetAttributeOnElementBy(IWebElement element, string attrToChange , string attArg)
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].style='"+attrToChange+ ": "+attArg+";'", element);
        }

        public void HardWaitFromMilliSeconds(int time)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(time));
        }

        public void ScrollIntoView(IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public void ScrollUntilVisible()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollBy(0,250)");

        }

        public void WaitUntilElementExist(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                wait.Until(ExpectedConditions.ElementExists((by)));

            }
            catch
            {
                wait.Until(ExpectedConditions.ElementExists((by)));
            }
        }

        public void WaitPageToLoad()
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        public void Maximize()
        {
            driver.Manage().Window.Maximize();

        }

        public void Close()
        {
            driver.Close();
        }

        public void ImplicityWait()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void CleanUpDriver()
        {
            driver.Close();
            driver.Dispose();
        }

        public void Quit()
        {
            driver.Quit();
        }

        public IOptions Manage()
        {
            return driver.Manage();
        }

        public INavigation Navigate()
        {
            return driver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            return driver.SwitchTo();
        }

        public IWebElement FindElement(By by)
        {
            return driver.FindElement(by);
        }

        public void ScrollToBottom()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
            HardWaitFromMilliSeconds(500);
        }

        public void ScrollToTop()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,0)");
            HardWaitFromMilliSeconds(500);
        }

        public void WaitUntilClickable(By what)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(what));
            }
            catch (StaleElementReferenceException)
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(what));
            }
        }

        public void WaitUntilElementDisapearsBy(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));

            }
            catch
            {
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
            }

        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            try
            {
                return driver.FindElements(by);
            }
            catch(ElementNotVisibleException)
            {
                return null;
            }

        }

        public void WaitElements(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            try
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
            }
            catch(StaleElementReferenceException)
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
            }
        }

        public void WaitElementUntilVisible(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            bool pom = false;
            while (pom)
            {
                if (element.Displayed)
                    pom = true;
            }

        }

        public void Dispose()
        {
            driver.Dispose();
        }

        private string GetSolutionPath()
        {
            string projectFolder = AppDomain.CurrentDomain.BaseDirectory;
            while (projectFolder.Contains(@"Tests\"))
            {
                var parentDir = Directory.GetParent(projectFolder);
                projectFolder = parentDir.ToString();
            }
            return projectFolder;
        }

        public Screenshot TakeScreenshot(object testName)
        {
            Screenshot ss;
            ss = ((ITakesScreenshot)driver).GetScreenshot();

            DirectoryInfo dirr = new DirectoryInfo(Path.Combine(GetSolutionPath(), @"Screenshots\"));
            foreach (FileInfo f in dirr.GetFiles())
            {
                if (f.Name == testName + ".png")
                {
                    f.Delete();
                    break;
                }
            }
            string path = Path.Combine(GetSolutionPath(), @"Screenshots\");
            ss.SaveAsFile(path + testName + ".png");

            return ss;
        }
    }
}
