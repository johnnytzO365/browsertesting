using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net;
using System.Threading;

namespace SeleniumTutorial
{
    class Link
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("C:\\Users\\spsetup\\Documents\\Visual Studio 2012\\Projects\\SeleniumTutorial\\.nuget\\selenium.chrome.webdriver.76.0.0\\driver");
        }
        [Test]
        public void CheckLinksOnGlobalNav()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");

            IList<IWebElement> links = driver.FindElements(By.ClassName("expanded"));
            foreach (IWebElement link in links)
            {

                var url = link.FindElement(By.CssSelector("a")).GetAttribute("href");
                IsLinkWorking(url);
            }
        }

        [Test]
        public void CheckRetailNode()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            var retail = driver.FindElement(By.ClassName("megamenu"));
            List<IWebElement> links = retail.FindElements(By.CssSelector("a")).ToList();
            Assert.AreEqual(34, links.Count);//μετράει τα links που είναι μέσα στο local navigation
        }

        [Test]
        public void CheckLinksOnRetail()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");

            IWebElement parentMenu = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[1]/div/div[2]/div/ul/li[1]/a"));
            parentMenu.Click();
            IList<IWebElement> links = driver.FindElements(By.ClassName("megamenu")).ToList();

            foreach (IWebElement link in links)
            {

                var url = link.FindElement(By.CssSelector("a")).GetAttribute("href");
                if (url != null)
                {
                    IsLinkWorking(url);//ελέγχει αν όλα τα links στο Retail 
                }

            }
        }

        [Test]
        public void CheckLinksOnBuss()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");

            IWebElement parentMenu = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[1]/div/div[2]/div/ul/li[2]/a"));
            parentMenu.Click();
            IList<IWebElement> links = driver.FindElements(By.ClassName("megamenu")).ToList();

            foreach (IWebElement link in links)
            {

                var url = link.FindElement(By.CssSelector("a")).GetAttribute("href");
                if (url != null)
                {
                    IsLinkWorking(url);//ελέγχει αν όλα τα links στο Buss
                }

            }
        }

        [Test]
        public void CheckLinksOnCorp()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");

            IWebElement parentMenu = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[1]/div/div[2]/div/ul/li[3]/a"));
            parentMenu.Click();
            IList<IWebElement> links = driver.FindElements(By.ClassName("megamenu")).ToList();

            foreach (IWebElement link in links)
            {

                var url = link.FindElement(By.CssSelector("a")).GetAttribute("href");
                if (url != null)
                {
                    IsLinkWorking(url);//ελέγχει όλα τα links στο Corp
                }

            }
        }

        [Test]
        public void CheckALink()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            IWebElement parentMenu = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[1]/div/div[2]/div/ul/li[1]/a"));
            parentMenu.Click();
            IWebElement brokenLink = parentMenu.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderCustomHeader_PlaceHolderCustomHeaderMain_ctl09_DefaultMobilePanel_tabsRepeater_ctl00_mainCategoriesRepeater_ctl06_MenuItemPanel']/ul/li[1]/a"));

            var url = brokenLink.GetAttribute("href");
            brokenLink.Click();

            Thread.Sleep(5000);
        }

        bool IsLinkWorking(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.UseDefaultCredentials = true;
            request.AllowAutoRedirect = true;

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("Is ok");
                    response.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (AssertionException e)
            {
                return false;
            }

        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

    }
}
