using IDriver;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Pages
{
    public class AccessoriesPage : Driver
    {
        public AccessoriesPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void GoTo()
        {
            GoTo("https://www.bugaboo.com/NL/en_NL/strollers/accessories?");
            WaitPageToLoad();
        }

        public void AcceptCookie()
        {
             FindElementUntilClickable(By.Id("acceptcookies")).Click();
        }

        public string GetProductCount()
        {
            IWebElement productsCounter = FindElementUntillVisible(By.ClassName("styles__TotalHits-sc-1wupe2m-6"));
            string[] productCount = productsCounter.Text.Split();
            string pomProductCount = productCount.First();

            return pomProductCount;
        }

        public List<IWebElement> GetAllProducts()
        {
            ScrollToBottom();
            HardWaitFromMilliSeconds(2000);
            IWebElement productsGrid = FindElement(By.CssSelector("#pageContainer > div"));
            IReadOnlyCollection<IWebElement> products = productsGrid.FindElements(By.ClassName("styles__Item-sc-8vglhj-0"));
            List<IWebElement> listProducts = new List<IWebElement>();
            ScrollToTop();
            foreach(IWebElement product in products)
            {
                listProducts.Add(product);
            }

            return listProducts;
        }

        public List<IWebElement> GetAllCheckboxOptions()
        {
            HardWaitFromMilliSeconds(1500);
            IReadOnlyCollection<IWebElement> options = FindElements(By.ClassName("styles__InputCheckBtnLabel-sc-1u0nltt-4"));
            List <IWebElement> optionsList= new List<IWebElement>();
            foreach(IWebElement option in options)
            {
                optionsList.Add(option);
            }
            return optionsList;
        }

        public List<IWebElement> GetPricesForAllLoadedProducts()
        {
            List<IWebElement> pricesList = new List<IWebElement>();
            foreach(IWebElement product in GetAllProducts())
            {
                IWebElement productPrice = product.FindElement(By.ClassName("styles__Price-sc-8vglhj-8"));
                pricesList.Add(productPrice);
            }

            return pricesList;
        }

        public List<IWebElement> GetColorListForAllLoadedProducts()
        {
            List<IWebElement> colorLost = new List<IWebElement>();
            foreach(IWebElement product in GetAllProducts())
            {
                IWebElement currentColorList = product.FindElement(By.ClassName("colors__Swatches-sc-1sha7t4-0"));
                colorLost.Add(currentColorList);
            }

            return colorLost;
        }

        public List<IWebElement> GetNamesForAllLoadedProducts()
        {
            List<IWebElement> namesList = new List<IWebElement>();
            foreach(IWebElement product in GetAllProducts())
            {
                IWebElement productName = product.FindElement(By.ClassName("styles__Title-sc-8vglhj-6"));
                namesList.Add(productName);
            }

            return namesList;
        }

        public List<IWebElement> GetColorsList()
        {
            IReadOnlyCollection<IWebElement> colorContainer = FindElement(By.ClassName("styles__FilterGroupSectionColorOptions-sc-1wupe2m-7")).
                FindElements(By.ClassName("colorFilter__Swatch-sc-1v4m65c-0"));
            List<IWebElement> colorsList = new List<IWebElement>();
            
            foreach (IWebElement color in colorContainer)
            {
                colorsList.Add(color);
            }

            return colorsList;
        }

        public int GetCurrentProductCount()
        {
            int count = 0;
            count = GetAllProducts().Count;

            return count;
        }

        public bool CanWeSearchByColors()
        {
            bool weCan = false;
            int allProductsCount = GetAllProducts().Count;

            foreach(IWebElement color in GetColorsList())
            {
                ScrollIntoView(color);
                HardWaitFromMilliSeconds(1000);
                color.Click();
                if (GetCurrentProductCount() <= allProductsCount)
                    weCan = true;
                ScrollIntoView(color);
                color.Click();
            }

            return weCan;
        }

        public bool DoesAllLoadedProductsHaveColors()
        {
            bool theyHave = false;

            foreach(IWebElement product in GetAllProducts())
            {
                if (GetAllProducts().Count == GetColorListForAllLoadedProducts().Count)
                    theyHave = true;
            }
            ScrollToTop();

            return theyHave;
        }

        public bool DoesAllLoadedProductsHavePrice()
        {
            bool theyHave = false;

            foreach(IWebElement product in GetAllProducts())
            {
                IWebElement productPrice = product.FindElement(By.ClassName("styles__Price-sc-8vglhj-8"));

                if (productPrice.Displayed)
                    theyHave = true;
            }

            return theyHave;
        }

        public bool DoesAllLoadedProductsHaveNames()
        {
            bool theyHave = false;

            foreach(IWebElement product in GetAllProducts())
            {
                if (GetAllProducts().Count == GetNamesForAllLoadedProducts().Count)
                    theyHave = true;
            }

            return theyHave;
        }

        public IWebElement GetClearBtn()
        {
            IWebElement clearBtn = FindElementUntilClickable(By.ClassName("styles__FilterClearBtn-sc-1wupe2m-4"));
            ScrollIntoView(clearBtn);

            return clearBtn;
        }

        public IWebElement GetMoreOptionsBtn()
        {
            IWebElement moreBtn = FindElementUntilClickable(By.ClassName("styles__ViewMore-sc-1wupe2m-13"));

            return moreBtn;
        }
        public bool CanWeSearchByAllCheckboxOptions()
        {
            bool correctOptions = false;
            int allProductsCount = GetAllProducts().Count;

            foreach (IWebElement option in GetAllCheckboxOptions())
            {
                ScrollIntoView(option);
                option.Click();

                if (GetProductCount() == GetAllProducts().Count.ToString() && DoesAllProductsAreLoadedCorrectly() &&
                    allProductsCount >= GetCurrentProductCount())
                    correctOptions = true;

                IWebElement clearBtn = FindElementUntilClickable(By.ClassName("styles__FilterClearBtn-sc-1wupe2m-4"));
                ScrollToTop();

                clearBtn.Click();
            }

            return correctOptions;
        }

        public bool DoesAllProductsAreLoadedCorrectly()
        {
            bool theyAre = false;

            foreach(IWebElement product in GetAllProducts())
            {
                ScrollIntoView(product);

                IWebElement currentProductColorList = product.FindElement(By.ClassName("colors__Swatches-sc-1sha7t4-0"));
                IWebElement currentProductName = product.FindElement(By.ClassName("styles__Title-sc-8vglhj-6"));
                IWebElement currentProductPriceValue = product.FindElement(By.ClassName("styles__Price-sc-8vglhj-8"));

                if (currentProductColorList.Displayed && currentProductName.Displayed && currentProductPriceValue.Displayed)
                    theyAre = true;
            }

            return theyAre;
        }

    }
}
