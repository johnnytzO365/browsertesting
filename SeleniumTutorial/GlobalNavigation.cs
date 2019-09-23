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
    class GlobalNavigation
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("C:\\Users\\spsetup\\Documents\\visual studio 2012\\Projects\\SeleniumTutorial\\.nuget\\selenium.chrome.webdriver.76.0.0\\driver");
        }

        [Test]        
        public void CheckNBGNavigationNodeCount()
        {
            driver.Navigate().GoToUrl("http://vm-sp2013");
            List<IWebElement> test = driver.FindElements(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[1]/div/div[2]/div/ul/li")).ToList();            
            Assert.AreEqual(3, test.Count);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
