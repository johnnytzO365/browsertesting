﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net;
using System.Threading;
<<<<<<< HEAD
	
=======
using System.Configuration;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.ComponentModel;
using OpenQA.Selenium.IE;

>>>>>>> 7f1f7c4b2f9e8472b01787fd622d191db50ccaee
namespace SeleniumTutorial
{
    class Link
    {
        IWebDriver driver;
<<<<<<< HEAD
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("C:\\Users\\spsetup\\Desktop\\SeleniumTutorial\\.nuget\\selenium.chrome.webdriver.76.0.0\\driver");
=======
        WebDriverWait wait;
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]));
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']/option[2]")));
            driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']/option[3]")).Click();
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.FileName = ConfigurationManager.AppSettings["ScriptPath"];
                    myProcess.Start();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Thread.Sleep(4000);

>>>>>>> 7f1f7c4b2f9e8472b01787fd622d191db50ccaee
        }
        [Test]
        public void CheckLinksOnGlobalNav()
        {
<<<<<<< HEAD
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            
=======
            

>>>>>>> 7f1f7c4b2f9e8472b01787fd622d191db50ccaee
            IList<IWebElement> links = driver.FindElements(By.ClassName("expanded"));
            foreach (IWebElement link in links)
            {

                var url = link.FindElement(By.CssSelector("a")).GetAttribute("href");
                IsLinkWorking(url);
            }
        }

        [Test]
<<<<<<< HEAD
        public void CheckLinksOnRetail() 
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");

            IWebElement parentMenu = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[1]/div/div[2]/div/ul/li[1]/a"));
            parentMenu.Click();
            IList<IWebElement> links = driver.FindElements(By.ClassName("megamenu")).ToList();
           
=======
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

>>>>>>> 7f1f7c4b2f9e8472b01787fd622d191db50ccaee
            foreach (IWebElement link in links)
            {

                var url = link.FindElement(By.CssSelector("a")).GetAttribute("href");
<<<<<<< HEAD
                if (url != null) { 
                     IsLinkWorking(url);
                }
                
=======
                if (url != null)
                {
                    IsLinkWorking(url);//ελέγχει αν όλα τα links στο Retail 
                }

>>>>>>> 7f1f7c4b2f9e8472b01787fd622d191db50ccaee
            }
        }

        [Test]
        public void CheckLinksOnBuss()
        {
<<<<<<< HEAD
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
=======
         
>>>>>>> 7f1f7c4b2f9e8472b01787fd622d191db50ccaee

            IWebElement parentMenu = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[1]/div/div[2]/div/ul/li[2]/a"));
            parentMenu.Click();
            IList<IWebElement> links = driver.FindElements(By.ClassName("megamenu")).ToList();

            foreach (IWebElement link in links)
            {

                var url = link.FindElement(By.CssSelector("a")).GetAttribute("href");
                if (url != null)
                {
<<<<<<< HEAD
                    IsLinkWorking(url);
=======
                    IsLinkWorking(url);//ελέγχει αν όλα τα links στο Buss
>>>>>>> 7f1f7c4b2f9e8472b01787fd622d191db50ccaee
                }

            }
        }

        [Test]
        public void CheckLinksOnCorp()
        {
<<<<<<< HEAD
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
=======
            
>>>>>>> 7f1f7c4b2f9e8472b01787fd622d191db50ccaee

            IWebElement parentMenu = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[1]/div/div[2]/div/ul/li[3]/a"));
            parentMenu.Click();
            IList<IWebElement> links = driver.FindElements(By.ClassName("megamenu")).ToList();

            foreach (IWebElement link in links)
            {

                var url = link.FindElement(By.CssSelector("a")).GetAttribute("href");
                if (url != null)
                {
<<<<<<< HEAD
                    IsLinkWorking(url);
=======
                    IsLinkWorking(url);//ελέγχει όλα τα links στο Corp
>>>>>>> 7f1f7c4b2f9e8472b01787fd622d191db50ccaee
                }

            }
        }

        [Test]
        public void CheckALink()
        {
<<<<<<< HEAD
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            IWebElement parentMenu= driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[1]/div/div[2]/div/ul/li[1]/a"));
=======
            IWebElement parentMenu = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[1]/div/div[2]/div/ul/li[1]/a"));
>>>>>>> 7f1f7c4b2f9e8472b01787fd622d191db50ccaee
            parentMenu.Click();
            IWebElement brokenLink = parentMenu.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderCustomHeader_PlaceHolderCustomHeaderMain_ctl09_DefaultMobilePanel_tabsRepeater_ctl00_mainCategoriesRepeater_ctl06_MenuItemPanel']/ul/li[1]/a"));

            var url = brokenLink.GetAttribute("href");
            brokenLink.Click();

            Thread.Sleep(5000);
        }
<<<<<<< HEAD
       
=======

>>>>>>> 7f1f7c4b2f9e8472b01787fd622d191db50ccaee
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
<<<<<<< HEAD
            catch
            {
                return false;
            }
 
        }
      
=======
            catch (AssertionException e)
            {
                return false;
            }

        }

>>>>>>> 7f1f7c4b2f9e8472b01787fd622d191db50ccaee
        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

    }
}
