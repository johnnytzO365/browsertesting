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
using System.Configuration;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.ComponentModel;
using OpenQA.Selenium.IE;

namespace SeleniumTutorial
{
    class PublicSiteTest
    {

        IWebDriver driver;
        WebDriverWait wait;
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]));
            Thread.Sleep(2000);
        }
        [Test]
        public void CheckLinksOnGlobalNav()
        {


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

            var retail = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[1]/div/div[1]/div/ul/li[1]/ul"));
            List<IWebElement> links = retail.FindElements(By.CssSelector("a")).ToList();
            Assert.AreEqual(8, links.Count);//μετράει τα links που είναι μέσα στο local navigation
        }

        [Test]
        public void CheckLinksOnRetail()
        {

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

