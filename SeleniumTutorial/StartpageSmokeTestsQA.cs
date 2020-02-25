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
    class StartpageSmokeTestsQA
    {

        IWebDriver driver;
        WebDriverWait wait;
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["QAServerName"]));
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
            IList<IWebElement> links = driver.FindElements(By.CssSelector("megamenu")).ToList();
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
        public void HeadersLinks()
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

        [Test]
        public void CheckLinksWebParts() {
            IWebElement parentMenu = driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_abf8d9a9_0af0_446b_ab87_9958a44b5ff9']/div/div[3]/h3"));
            parentMenu.Click();
            IList<IWebElement> links = driver.FindElements(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_abf8d9a9_0af0_446b_ab87_9958a44b5ff9']/div/div[3]/div/ul")).ToList();
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
        public void CheckAllLinks()
        {
            IWebElement main = driver.FindElement(By.ClassName("page"));
            IList<IWebElement> links = main.FindElements(By.TagName("a"));
            foreach (IWebElement link in links)
            {
                var url = link.GetAttribute("href");
                if (url != null)
                {
                    IsLinkWorking(url);//ελέγχει όλα τα links στο Corp
                }
            }


        }

        [Test]
        public void CheckAccordions() {
            //Header Accordion
            IWebElement parentMenu = driver.FindElement(By.XPath("//span[@id='DeltaPlaceHolderMain']/div/div/div/div/div/ul/li/a"));
            try
            {
                parentMenu.Click();
                Thread.Sleep(2000);
                parentMenu.Click();
                Thread.Sleep(2000);
                Console.WriteLine("Όμιλος");
            }

            catch
            {
                Console.WriteLine("Όμιλος πρόβλημα");
            }

            parentMenu = driver.FindElement(By.XPath("//span[@id='DeltaPlaceHolderMain']/div/div/div/div/div/ul/li[2]/a"));
            try
            {
                parentMenu.Click();
                Thread.Sleep(2000);
                parentMenu.Click();
                Thread.Sleep(2000);
                Console.WriteLine("Εταιρική Διακυβέρνηση");
            }
            catch
            {
                Console.WriteLine("Εταιρική Διακυβέρνηση πρόβλημα");
            }

            parentMenu = driver.FindElement(By.XPath("//span[@id='DeltaPlaceHolderMain']/div/div/div/div/div/ul/li[3]/a"));
            try
            {
                parentMenu.Click();
                Thread.Sleep(2000);
                parentMenu.Click();
                Thread.Sleep(2000);
                Console.WriteLine("Ενημέρωση Επενδυτών");
            }
            catch
            {
                Console.WriteLine("Ενημέρωση Επενδυτών πρόβλημα");
            }

            parentMenu = driver.FindElement(By.XPath("//span[@id='DeltaPlaceHolderMain']/div/div/div/div/div/ul/li[4]/a"));
            try
            {
                parentMenu.Click();
                Thread.Sleep(2000);
                parentMenu.Click();
                Thread.Sleep(2000);
                Console.WriteLine("Γραφείο Τύπου");
            }
            catch
            {
                Console.WriteLine("Γραφείο Τύπου πρόβλημα");
            }
            
            //Top Menu Accordion
            parentMenu = driver.FindElement(By.XPath("//span[@id='DeltaPlaceHolderMain']/div/div/div/div[2]/div/ul/li/a"));
            try
            {
                parentMenu.Click();
                Thread.Sleep(2000);
                parentMenu.Click();
                Thread.Sleep(2000);
                Console.WriteLine("Ιδιώτες");
            }
            catch
            {
                Console.WriteLine("Ιδιώτες πρόβλημα");
            }
            parentMenu = driver.FindElement(By.XPath("//span[@id='DeltaPlaceHolderMain']/div/div/div/div[2]/div/ul/li[2]/a"));
            try
            {
                parentMenu.Click();
                Thread.Sleep(2000);
                parentMenu.Click();
                Thread.Sleep(2000);
                Console.WriteLine("Επαγγελματίες & ΜΜΕ");
            }
            catch
            {
                Console.WriteLine("Επαγγελματίες & ΜΜΕ πρόβλημα");
            }

            parentMenu = driver.FindElement(By.XPath("//span[@id='DeltaPlaceHolderMain']/div/div/div/div[2]/div/ul/li[3]/a"));
            try
            {
                parentMenu.Click();
                Thread.Sleep(2000);
                parentMenu.Click();
                Thread.Sleep(2000);
                Console.WriteLine("Επιχειρήσεις & Financial Institutions");
            }
            catch
            {
                Console.WriteLine("Επιχειρήσεις & Financial Institutions πρόβλημα");
            }

            //DropDown Accordion
            parentMenu = driver.FindElement(By.XPath("//div[@id='ctl00_SPWebPartManager1_g_abf8d9a9_0af0_446b_ab87_9958a44b5ff9']/div/div[3]/h3"));
            try
            {
                parentMenu.Click();
                Thread.Sleep(2000);
                parentMenu.Click();
                Thread.Sleep(2000);
                Console.WriteLine("Πληροφορίες Εργαλεία");
            }
            catch
            {
                Console.WriteLine("Πληροφορίες Εργαλεία πρόβλημα");
            }
            parentMenu = driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_abf8d9a9_0af0_446b_ab87_9958a44b5ff9']/div/div[4]/h3"));
            try
            {
                parentMenu.Click();
                Thread.Sleep(2000);
                parentMenu.Click();
                Thread.Sleep(2000);
                Console.WriteLine("Χρήσιμοι Σύνδεσμοι");
            }
            catch
            {
                Console.WriteLine("Χρήσιμοι Σύνδεσμοι πρόβλημα");
            }
        }
        bool IsLinkWorking(string url)
        {
            try
            {
                url.TrimStart('/');
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.UseDefaultCredentials = true;
                request.AllowAutoRedirect = true;
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("Is ok");
                        Console.WriteLine(url);
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
            catch {
                Console.WriteLine("Not ok");
                Console.WriteLine(url);
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

