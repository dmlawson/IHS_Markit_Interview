using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Threading;
using SeleniumExtras.PageObjects;

namespace WebDriverProject.PageObjects
{

    class DotNetFiddlePage
    {
        readonly string test_url = "https://dotnetfiddle.net/";
        private IWebDriver driver;

        public DotNetFiddlePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(test_url);
            driver.Manage().Window.Maximize();
        }

        public void ClosePage()
        {
            driver.Close();
            driver.Quit();
        }

        public IWebElement GetRunButton()
        {
            return driver.FindElement(By.Id("run-button"));
        }

        public IWebElement GetOutputBox()
        {
            return driver.FindElement(By.Id("output"));
        }

        public IWebElement GetPackageSearchBar()
        {
            string ccs_search_bar_str = "#CodeForm > div.main.docked > div.sidebar.unselectable > div:nth-child(5) > div > div > input";
            return driver.FindElement(By.CssSelector(ccs_search_bar_str));
        }

        private IReadOnlyList<IWebElement> GetSearchResults()
        {
            return driver.FindElements(By.CssSelector("#menu li"));
        }

        private IReadOnlyList<IWebElement> GetVersionList(IWebElement nunit_package)
        {
            return nunit_package.FindElements(By.CssSelector("ul li"));
        }

        public void ClickNUnitVersion3_12()
        {
            Actions builder = new Actions(driver);
            IReadOnlyList<IWebElement> package_list_items = GetSearchResults();
            IWebElement n_unit_list_item = null;

            // iterate through each item in the list, searching for "NUnit"
            foreach (IWebElement list_item in package_list_items)
            {
                if (list_item.Text == "NUnit")
                {
                    n_unit_list_item = list_item;
                    break;
                }
            }

            // Move the mouse over the "NUnit" list item
            builder.MoveToElement(n_unit_list_item).Build().Perform();

            IReadOnlyList<IWebElement> versions_list = GetVersionList(n_unit_list_item);

            // wait for the versions list to load
            while (versions_list.Count <= 1)
            {
                Thread.Sleep(1000);
                versions_list = GetVersionList(n_unit_list_item);
            }

            // iterate through the versions list to find the correct version
            IWebElement version_list_item = null;
            foreach (IWebElement list_item in versions_list)
            {
                if (list_item.Text == "3.12.0")
                {
                    version_list_item = list_item;
                    break;
                }
            }

            // Move mouse to version "3.12.0" and click it
            builder.MoveToElement(version_list_item).Click().Build().Perform();
        }

        public string GetSelectedPackageString()
        {
            return driver.FindElement(By.ClassName("package-name")).Text;
        }
    }
}
