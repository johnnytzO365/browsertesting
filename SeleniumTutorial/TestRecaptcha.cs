using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.Threading;

namespace SeleniumTutorial
{
    class TestRecaptcha
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }

        [Test]
        public void Recaptcha3()
        {
            driver.Navigate().GoToUrl("https://micrositesqa.nbg.gr/TradeFinance");
            driver.FindElement(By.CssSelector("[placeholder='Ονοματεπώνυμο- Επωνυμία*']")).SendKeys("Bousiou");
            driver.FindElement(By.CssSelector("[placeholder='Email']")).SendKeys("sindy_bo@hotmail.com");
            IWebElement radio = driver.FindElement(By.Id("mat-radio-3"));
            radio.FindElement(By.XPath("./label/div")).Click();
            IWebElement checkbox = driver.FindElement(By.Id("mat-checkbox-2"));
            checkbox.FindElement(By.XPath("./label/div")).Click();
            driver.FindElement(By.ClassName("find-btn")).Click();
            Thread.Sleep(3000);
            try
            {
                driver.FindElement(By.ClassName("find-btn")).Click();
            }
            catch { }
            Thread.Sleep(10000);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
