using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using System.Configuration;
using System.IO;
using OpenQA.Selenium.Chrome;

namespace CopyPasteSPPages
{
    class Test
    {
        IWebDriver driver = new InternetExplorerDriver("C:\\Users\\e82331\\Desktop\\IEDriverServer");
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
        }

        [Test]
        public void CopyPaste()
        {
            driver.Navigate().GoToUrl("http://mynbgportal:86/InternalCom/Pages/Home.aspx");
            //String InternalComSelector = "[href='/Home/GetInternalComlist']";
            //wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector(InternalComSelector))));
            //IWebElement InternalCom = driver.FindElement(By.CssSelector(InternalComSelector));
            //InternalCom.Click();
            wait.Until(ExpectedConditions.AlertIsPresent());
            var alert = driver.SwitchTo().Alert();
            alert.SetAuthenticationCredentials("bank\\e82331", "123sindy^");
            alert.Accept();

            List<IWebElement> elements = driver.FindElements(By.CssSelector("div.row.node.article")).ToList();
            foreach (IWebElement element in elements)
            {
                IWebElement link = element.FindElement(By.XPath("./div/a"));
                String href = link.GetAttribute("href");
                if (href.EndsWith(".aspx"))
                {
                    link.Click();
                    String originalWindowHandle = driver.CurrentWindowHandle;
                    IWebDriver newDriver = driver.SwitchTo().Window(originalWindowHandle);
                    WebDriverWait newWait = new WebDriverWait(driver, TimeSpan.FromSeconds(40)); 
                    newWait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("article-content")));
                    IWebElement content = newDriver.FindElement(By.ClassName("article-content"));
                    String html = content.GetAttribute("outerHTML");
                    Console.WriteLine(html);
                    StreamWriter file = new System.IO.StreamWriter(@"C:\Users\e82331\Documents\htmls.csv");
                }
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
