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
            List<IWebElement> ItemsInFooter = null;
            try
            {
                String ItemsInFooterSelector = "//*[@class='footer_menu clearfix']/li";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(ItemsInFooterSelector)));
                ItemsInFooter = driver.FindElements(By.XPath(ItemsInFooterSelector)).ToList();
            }
            catch
            {
                Assert.Fail("Couldn't find the Footer items in home page!");
            }

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/_layouts/15/termstoremanager.aspx");
            ClickSiteCollection();
            ClickFooter();
            ClickEl();

            List<IWebElement> ItemsInTermStore = null;
            try
            {
                String ItemsInTermStoreSelector = "//*[@id='TaxonomyRootID']/ul/li[5]/ul/li/ul/li/ul/li";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(ItemsInTermStoreSelector)));
                ItemsInTermStore = driver.FindElements(By.XPath(ItemsInTermStoreSelector)).ToList();
            }
            catch
            {
                Assert.Fail("Couldn't find elements below el Term!");
            }

            Assert.AreEqual(ItemsInFooter.Count, ItemsInTermStore.Count);
        }
        
        [Test]
        public void CheckFooterAddAndDelete()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/_layouts/15/termstoremanager.aspx");

            ClickSiteCollection();
            ClickFooter();
            /*ClickEl();

            try
            {
                String TestItemSelector = "//[@title='Test']";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(TestItemSelector)));
                IWebElement TestItem = driver.FindElement(By.XPath(TestItemSelector));
                if (TestItem != null)
                {
                    try
                    {
                        String TestArrowSelector = "../../span[2]/";
                        IWebElement TestArrow = TestItem.FindElement(By.XPath(TestArrowSelector));
                        TestArrow.Click();
                    }
                    catch
                    {
                        Assert.Fail("Couldn't find down arrow next to Test term!");
                    }
                    try
                    {
                        String deleteTermSelector = "//*[@id='ctl00_PlaceHolderMain_LeafTermECBMenuDelete']/span[1]";
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(deleteTermSelector)));
                        IWebElement deleteTerm = driver.FindElement(By.XPath(deleteTermSelector));
                        deleteTerm.Click();
                    }
                    catch
                    {
                        Assert.Fail("Couldn't find Delete Term option!");
                    }
                }
            }
            catch
            {
                //Assert.I();
            }
            //{

            //}
            /*
            List<IWebElement> ItemsInTermStore = null;  //check if we already have a Test Term and delete it
            try
            {
                String ItemsInTermStoreSelector = "//*[@id='TaxonomyRootID']/ul/li[5]/ul/li/ul/li/ul/li";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(ItemsInTermStoreSelector)));
                ItemsInTermStore = driver.FindElements(By.XPath(ItemsInTermStoreSelector)).ToList();
            }
            catch
            {
                Assert.Fail("Couldn't find elements below el Term!");
            }
            for(int i=0;i<ItemsInTermStore.Count;i++)
                if (ItemsInTermStore[i].Text == "Test")
                {
                    DeleteItem(ItemsInTermStore);
                    break;
                }*/

            try
            {
                String elArrowSelector = "//*[@id='_Div12']/span[2]/span[2]";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(elArrowSelector)));
                IWebElement elArrow = driver.FindElement(By.XPath(elArrowSelector));
                elArrow.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find down arrow next to el Term!");
            }
            try
            {
                String createTermSelector = "//*[@id='ctl00_PlaceHolderMain_TermECBMenuNew']/span[1]";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(createTermSelector)));
                IWebElement createTerm = driver.FindElement(By.XPath(createTermSelector));
                createTerm.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Create Term option!");
            }
            try
            {
                String new_liSelector = "[id$='newnodetemplate']";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(new_liSelector)));
                IWebElement new_li = driver.FindElement(By.CssSelector(new_liSelector));
                String new_inputSelector = "./div/span[2]/span/input";
                IWebElement new_input = new_li.FindElement(By.XPath(new_inputSelector));
                new_input.SendKeys("Test" + Keys.Enter);
            }
            catch
            {
                Assert.Fail("Couldn't create the Test Term!");
            }

            CheckFooterTermStoreNodeCount();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");  //Check that the last node of footer is the newly created Test node
            try
            {
                String lastNodeSelector = "//*[@class='footer_menu clearfix']/li[last()]/a";
                IWebElement lastNode = driver.FindElement(By.XPath(lastNodeSelector));
                Assert.AreEqual(lastNode.Text, "Test");
            }
            catch
            {
                Assert.Fail("Couldn't find last node on footer!");
            }

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/_layouts/15/termstoremanager.aspx");

            ClickSiteCollection();
            ClickFooter();
            ClickEl();

            try
            {
                String TestArrowSelector = "//*[@id='TaxonomyRootID']/ul/li[5]/ul/li/ul/li/ul/li[last()]/div/span[2]/span[2]";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(TestArrowSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(TestArrowSelector)));
                IWebElement TestArrow = driver.FindElement(By.XPath(TestArrowSelector));
                TestArrow.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find down arrow next to Test term!");
            }
            try
            {
                String deleteTermSelector = "//*[@id='ctl00_PlaceHolderMain_LeafTermECBMenuDelete']/span[1]";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(deleteTermSelector)));
                IWebElement deleteTerm = driver.FindElement(By.XPath(deleteTermSelector));
                deleteTerm.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Delete Term option!");
            }

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();

            CheckFooterTermStoreNodeCount();
        }
        
        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        public void ClickSiteCollection()
        {
            try
            {
                String SiteCollectionSelector = "//*[@id='_Div7']/span";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(SiteCollectionSelector)));
                IWebElement SiteCollection = driver.FindElement(By.XPath(SiteCollectionSelector));
                SiteCollection.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Site Collection Term!");
            }
        }

        public void ClickFooter()
        {
            try
            {
                String FooterSelector = "//*[@id='_Div8']/span";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(FooterSelector)));
                IWebElement Footer = driver.FindElement(By.XPath(FooterSelector));
                Footer.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Footer Navigation Term!");
            }
        }

        public void ClickEl()
        {
            try
            {
                String elSelector = "//*[@id='_Div12']/span";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(elSelector)));
                IWebElement el = driver.FindElement(By.XPath(elSelector));
                el.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find el Term!");
            }
        }
    }
}
