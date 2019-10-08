using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace SeleniumTutorial
{
    class NewsPage
    {
        IWebDriver driver;
        WebDriverWait wait;
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("C:\\Users\\spsetup\\Documents\\Visual Studio 2012\\Projects\\SeleniumTutorial\\.nuget\\selenium.chrome.webdriver.76.0.0\\driver");
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
        }

        [Test]
        public void CheckNewsOrder()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el/news");

            List<IWebElement> NewsListDates = null;
            try
            {
                NewsListDates = driver.FindElements(By.ClassName("date field")).ToList();
            }
            catch
            {
                Assert.Fail("Couldn't find Dates of News Articles!");
            }
            for (int i = 0; i < NewsListDates.Count; i++)
            {
                Console.WriteLine(NewsListDates[i]);
            }

        }

        [Test]
        public void CheckNewsSearchWithKeyWord()
        {

        }

        [Test]
        public void CheckNewsSearchWithDate()
        {

        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
