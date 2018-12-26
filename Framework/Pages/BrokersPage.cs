using IDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace Framework.Pages
{
    public class BrokersPage : Driver
    {
        public BrokersPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void GoTo()
        {
            GoTo("https://www.yavlena.com/broker/");
        }

        public void AcceptCookie()
        {
            WebDriverWait wait = new WebDriverWait(this, TimeSpan.FromSeconds(10));

            IWebElement cookie = FindElement(By.ClassName("hide-cookies-message"));
            cookie.Click();
            wait.Until(ExpectedConditions.StalenessOf(cookie));
        }

        public IWebElement GetExpandBtn()
        {
            WaitUntilClickable(By.ClassName("load-more-results-list"));
            IWebElement expandBtn = FindElement(By.ClassName("load-more-results-list"));

            return expandBtn;
        }

        public void WaitLoaderToDisaper()
        {
            IWebElement loader = FindElement(By.ClassName("brokers-loading"));
            while(loader.Displayed)
            WaitUntilElementDisapearsBy(By.ClassName("brokers-loading"));
        }

        public void ExpandBrokersList()
        {
             WaitLoaderToDisaper();
             ScrollIntoView(GetExpandBtn());
             HardWaitFromMilliSeconds(500);
             GetExpandBtn().Click();
             ScrollToBottom();
        }

        public List<string> GetAllNames()
        {
            HardWaitFromMilliSeconds(500);
            List<string> brokersNames = new List<string>();
            IReadOnlyCollection<IWebElement> pomNames = FindElements(By.ClassName("name"));
            foreach (IWebElement element in pomNames)
            {
                brokersNames.Add(element.Text);
            }
            return brokersNames;
        }

        public IWebElement GetSearchBtn()
        {
            WaitUntilElementExist(By.ClassName("input-search"));
            IWebElement searchBtn = FindElementUntilClickable(By.ClassName("input-search"));

            return searchBtn;
        }

        public IWebElement GetClearBtn()
        {
            WaitUntilElementExist(By.ClassName("clear-all-dropdowns"));
            IWebElement clearBtn = FindElement(By.ClassName("clear-all-dropdowns"));

            return clearBtn;
        }

        public IWebElement GetCurrentAdress()
        {
            WaitUntilElementExist(By.CssSelector("#brokers-grid-holder > div > div > article:nth-child(1) > div > div > div.header-group > div"));
            IWebElement adress = FindElement(By.CssSelector("#brokers-grid-holder > div > div > article:nth-child(1) > div > div > div.header-group > div"));

            return adress;
        }

        public IWebElement GetCurrentFirstTel()
        {
            WaitUntilElementExist(By.CssSelector("#brokers-grid-holder > div > div > article > div > div > div.tel-group > span:nth-child(2) > a"));
            IWebElement firstTel = FindElement(By.CssSelector("#brokers-grid-holder > div > div > article > div > div > div.tel-group > span:nth-child(2) > a"));

            return firstTel;
        }

        public IWebElement GetCurrentSecondTel()
        {
            WaitUntilElementExist(By.CssSelector("#brokers-grid-holder > div > div > article > div > div > div.tel-group > span:nth-child(3) > a"));
            IWebElement secondTel = FindElement(By.CssSelector("#brokers-grid-holder > div > div > article > div > div > div.tel-group > span:nth-child(3) > a"));

            return secondTel;
        }

        public IWebElement GetCurrentPlacesAmount()
        {
            WaitUntilElementExist(By.CssSelector("#brokers-grid-holder > div > div > article > div > div > div.position > a"));
            IWebElement placesAmount = FindElement(By.CssSelector("#brokers-grid-holder > div > div > article > div > div > div.position > a"));

            return placesAmount;
        }

        public string GetCurrentEmpCount()
        {
            IWebElement cardCountElement = FindElement(By.ClassName("broker-list-holder"));
            string cardCounter = cardCountElement.GetAttribute("data-total-count");

            return cardCounter;
        }

        public bool CanSearchAllBrokers()
        {
            bool weCan = false;

            foreach (string name in GetAllNames())
            {

                GetSearchBtn().SendKeys(name);
                WaitLoaderToDisaper();
                HardWaitFromMilliSeconds(2000);

                if (GetCurrentAdress().Displayed && GetCurrentEmpCount() == "1" && GetCurrentFirstTel().Displayed && GetCurrentSecondTel().Displayed && GetCurrentPlacesAmount().Displayed)
                    weCan = true;
                else
                {
                    weCan = false;
                    break;
                };
                GetSearchBtn().Clear();
                HardWaitFromMilliSeconds(500);
                WaitLoaderToDisaper();
            }
            return weCan;
        }
    }
}
