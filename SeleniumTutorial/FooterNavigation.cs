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
    class FooterNavigation
    {
        IWebDriver driver;
        WebDriverWait wait;
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("C:\\Users\\spsetup\\Documents\\Visual Studio 2012\\Projects\\SeleniumTutorial\\.nuget\\selenium.chrome.webdriver.76.0.0\\driver");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }

        [Test]
        public void CheckFooterNodeCount()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013");
            List<IWebElement> test = driver.FindElements(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[4]/div/ul[2]/li")).ToList(); //make a list with the elements on bottom
            Assert.AreEqual(3, test.Count);
        }

        [Test]
        public void CheckFooterSocialMediaNodeCount()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013");
            List<IWebElement> test = driver.FindElements(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[4]/div/div/a")).ToList();  //make a list with the elements on bottom
            Assert.AreEqual(3, test.Count);
        }

        [Test]
        public void CheckFooterTermStoreNodeCount()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            List<IWebElement> liItems = driver.FindElements(By.XPath("//*[@class='footer_menu clearfix']/li")).ToList();
        }

        /*
        [Test]
        public void CheckFooterAddAndDelete()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/_layouts/15/termstoremanager.aspx");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='_Div7']/span")));
            driver.FindElement(By.XPath("//*[@id='_Div7']/span")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='_Div8']/span")));
            driver.FindElement(By.XPath("//*[@id='_Div8']/span")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='_Div12']/span[2]/span[2]")));
            driver.FindElement(By.XPath("//*[@id='_Div12']/span[2]/span[2]")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='ctl00_PlaceHolderMain_TermECBMenuNew']/span[1]")));
            driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_TermECBMenuNew']/span[1]")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id$='newnodetemplate']")));
            IWebElement new_li = driver.FindElement(By.CssSelector("[id$='newnodetemplate']"));
            IWebElement new_input = new_li.FindElement(By.XPath("./div/span[2]/span/input"));
            new_input.SendKeys("Test" + Keys.Enter);

            CheckFooterTermStoreNodeCount();
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            IWebElement new_node = driver.FindElement(By.XPath("//*[@class='footer_menu clearfix']/li[last()]/a"));
            Assert.AreEqual(new_node.Text, "Test");

            CheckFooterTermStoreNodeCount();
      }
*/
        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
