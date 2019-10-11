using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTutorial
{
    class LanguageSelector
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
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
