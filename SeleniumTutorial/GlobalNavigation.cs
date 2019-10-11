using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
        }

        [Test]        
        public void CheckNBGNavigationNodeCount()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]));
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
