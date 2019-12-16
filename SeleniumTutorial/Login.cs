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
    class Login
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
            driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']/option[2]")).Click();
        }

        [Test]
        public void TestLOgin()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]));
            Thread.Sleep(5000);
        }
        [Test]

        public void TestLOginAD()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]));
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']/option[2]")));
            driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']/option[2]")).Click();

        }

        [Test]
        public void TestLOginQA()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]));
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']/option[2]")));
            driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']/option[3]")).Click();

            //Thread.Sleep(10000);
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
            
            Thread.Sleep(500000);
        }
        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
