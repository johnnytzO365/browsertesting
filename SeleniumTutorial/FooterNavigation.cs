using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTutorial
{
    class FooterNavigation
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("C:\\Users\\spsetup\\Documents\\Visual Studio 2012\\Projects\\SeleniumTutorial\\.nuget\\selenium.chrome.webdriver.76.0.0\\driver");
        }

        [Test]
        public void CheckNBGFooterNavigationNodeCount()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013");
            List<IWebElement> test = driver.FindElements(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[4]/div/ul[2]/li")).ToList();
            Assert.AreEqual(3, test.Count);
        }

        [Test]
        public void CheckNBGFooterSocialMediaNodeCount()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013");
            List<IWebElement> test = driver.FindElements(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[4]/div/div/a")).ToList();
            Assert.AreEqual(3, test.Count);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
