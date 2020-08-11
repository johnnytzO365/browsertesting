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
    class LanguageSelector
    {
        IWebDriver driver;
        WebDriverWait wait;
        [SetUp]
        public void StartBrowser()
        {
            driver = new InternetExplorerDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
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
        }

        [Test]
        public void CheckSelectorTextInEnglishRootSite()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]) + "el");
            List<IWebElement> links = driver.FindElements(By.TagName("a")).ToList();
            IWebElement ls = driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderCustomHeader_PlaceHolderCustomHeaderTop_ctl00_ctl00_LangSwitchButton']"));
            string displayText = ls.Text;
            Assert.AreEqual("EN", displayText);            
        }

        [Test]
        public void CheckLanguageChangeFriendlyURLs()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]) + "en");
            IWebElement ls = driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderCustomHeader_PlaceHolderCustomHeaderTop_ctl00_ctl00_LangSwitchButton']"));
            string displayText = ls.Text;
            Assert.AreEqual("EL", displayText);
            ls.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.Until(ExpectedConditions.UrlContains("el"));
            ls = driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderCustomHeader_PlaceHolderCustomHeaderTop_ctl00_ctl00_LangSwitchButton']"));
            Assert.AreEqual("EN", ls.Text);
            Assert.IsTrue(driver.Url.Contains("/el"));
        }

        [Test]
        public void CheckLanguageChangeNormalURLs()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]) + "english/Pages/Default.aspx");
            IWebElement ls = driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderCustomHeader_PlaceHolderCustomHeaderTop_ctl00_ctl00_LangSwitchButton']"));
            string displayText = ls.Text;
            Assert.AreEqual("EL", displayText);
            ls.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.Until(ExpectedConditions.UrlContains("/greek"));
            ls = driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderCustomHeader_PlaceHolderCustomHeaderTop_ctl00_ctl00_LangSwitchButton']"));
            Assert.AreEqual("EN", ls.Text);
            Assert.IsTrue(driver.Url.Contains("/greek"));
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
