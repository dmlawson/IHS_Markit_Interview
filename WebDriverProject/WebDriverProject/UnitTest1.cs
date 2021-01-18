using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using WebDriverProject.PageObjects;

namespace WebDriverProject
{
    [TestClass]
    public class UnitTest1
    {
        static DotNetFiddlePage page;

        [AssemblyInitialize]
        public static void SetUp(TestContext context)
        {
            page = new DotNetFiddlePage(new ChromeDriver());
            page.OpenPage();
        }
        
        [TestMethod]
        public void TestChromeDriver_1()
        {
            // Constants
            string expected = "Hello World";

            // Find and click the Run button
            page.GetRunButton().Click();

            // Find and read the output text
            string output = page.GetOutputBox().GetAttribute("textContent").Trim();
            // Normalize the string for comparison
            output = Regex.Replace(output, @"\u00A0", " ");
            // Verify that the two strings are equal
            Assert.AreEqual(expected, output, false);
            Console.WriteLine("Verified output matches expected value!");
        }
        
        [TestMethod]
        public void TestChromeDriver_2()
        {
            // Fine the NuGet Packages search bar and type "nUnit"
            page.GetPackageSearchBar().SendKeys("nUnit");

            //Move the mouse over the list item labeled "NUnit" and then click on version "3.12.0"
            page.ClickNUnitVersion3_12();

            Assert.AreEqual(page.GetSelectedPackageString(), "NUnit");
            Console.WriteLine("Successfully Verify NUnit Package was added!");
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
            page.ClosePage();
        }
    }
}
