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
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
        }

        [Test]
        public void CheckFooterTermStoreNodeCount()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            List<IWebElement> ItemsInFooter = driver.FindElements(By.XPath("//*[@class='footer_menu clearfix']/li")).ToList();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/_layouts/15/termstoremanager.aspx");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='_Div7']/span")));
            IWebElement SiteCollection = driver.FindElement(By.XPath("//*[@id='_Div7']/span"));
            if (SiteCollection != null)
                SiteCollection.Click();
            else
                Assert.Fail("Couldn't find Site Collection Term!");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='_Div8']/span")));
            IWebElement Footer = driver.FindElement(By.XPath("//*[@id='_Div8']/span"));
            if (Footer != null)
                Footer.Click();
            else
                Assert.Fail("Couldn't find Footer Navigation Term!");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='_Div12']/span[2]/span[2]")));
            IWebElement el = driver.FindElement(By.XPath("//*[@id='_Div12']/span"));
            if (el != null)
                el.Click();
            else
                Assert.Fail("Couldn't find el Term!");

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("TaxonomyRootID")));
            IWebElement Taxonomy = driver.FindElement(By.Id("TaxonomyRootID"));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='TaxonomyRootID']/ul/li[5]/ul/li/ul/li/ul/li")));
            List<IWebElement> ItemsInTermStore = Taxonomy.FindElements(By.XPath("./ul/li[5]/ul/li/ul/li/ul/li")).ToList();

            Assert.AreEqual(ItemsInFooter.Count, ItemsInTermStore.Count);
        }
        /*
        [Test]
        public void CheckFooterAddAndDelete()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/_layouts/15/termstoremanager.aspx");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='_Div7']/span")));
            IWebElement SiteCollection = driver.FindElement(By.XPath("//*[@id='_Div7']/span"));
            if (SiteCollection != null)
                SiteCollection.Click();
            else
                Assert.Fail("Couldn't find Site Collection Term!");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='_Div8']/span")));
            IWebElement Footer = driver.FindElement(By.XPath("//*[@id='_Div8']/span"));
            if (Footer != null)
                Footer.Click();
            else
                Assert.Fail("Couldn't find Footer Navigation Term!");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='_Div12']/span[2]/span[2]")));
            IWebElement elArrow = driver.FindElement(By.XPath("//*[@id='_Div12']/span[2]/span[2]"));
            if (elArrow != null)
                elArrow.Click();
            else
                Assert.Fail("Couldn't find down arrow next to el Term!");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='ctl00_PlaceHolderMain_TermECBMenuNew']/span[1]")));
            IWebElement createTerm = driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_TermECBMenuNew']/span[1]"));
            if (createTerm != null)
                createTerm.Click();
            else
                Assert.Fail("Couldn't find Create Term option!");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id$='newnodetemplate']")));
            IWebElement new_li = driver.FindElement(By.CssSelector("[id$='newnodetemplate']"));
            IWebElement new_input = new_li.FindElement(By.XPath("./div/span[2]/span/input"));
            new_input.SendKeys("Test" + Keys.Enter);

            CheckFooterTermStoreNodeCount();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");  //Check that the last node of footer is the newly created Test node
            IWebElement lastNode = driver.FindElement(By.XPath("//*[@class='footer_menu clearfix']/li[last()]/a"));
            if (lastNode != null)
                Assert.AreEqual(lastNode.Text, "Test");
            else
                Assert.Fail("Couldn't find last node on footer!");

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/_layouts/15/termstoremanager.aspx");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='_Div7']/span")));
            SiteCollection = driver.FindElement(By.XPath("//*[@id='_Div7']/span"));
            SiteCollection.Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='_Div8']/span")));
            Footer = driver.FindElement(By.XPath("//*[@id='_Div8']/span"));
            Footer.Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='_Div12']/span[2]/span[2]")));
            IWebElement el = driver.FindElement(By.XPath("//*[@id='_Div12']/span"));
            if (el != null)
                el.Click();
            else
                Assert.Fail("Couldn't find el Term!");

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='TaxonomyRootID']/ul/li[5]/ul/li/ul/li/ul/li[last()]/div/span[2]/span[2]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='TaxonomyRootID']/ul/li[5]/ul/li/ul/li/ul/li[last()]/div/span[2]/span[2]")));
            IWebElement TestArrow = driver.FindElement(By.XPath("//*[@id='TaxonomyRootID']/ul/li[5]/ul/li/ul/li/ul/li[last()]/div/span[2]/span[2]"));
            if (TestArrow != null)
                TestArrow.Click();
            else
                Assert.Fail("Couldn't find down arrow next to Test term!");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='ctl00_PlaceHolderMain_LeafTermECBMenuDelete']/span[1]")));
            IWebElement deleteTerm = driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_LeafTermECBMenuDelete']/span[1]"));
            if (deleteTerm != null)
                deleteTerm.Click();
            else
                Assert.Fail("Couldn't find Delete Term option!");

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();

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
