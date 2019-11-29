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

namespace CopyPasteSPPages
{
    class Test
    {
        IWebDriver ie = new InternetExplorerDriver("C:\\Users\\e82331\\Documents\\IEDriverServer");
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            ie.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(ie, TimeSpan.FromSeconds(40));
        }

        [Test]
        public void CopyPaste()
        {
            ie.Navigate().GoToUrl("http://mynbgportal:86/InternalCom/Pages/Home.aspx");
            wait.Until(ExpectedConditions.AlertIsPresent());
            var alert = ie.SwitchTo().Alert();
            alert.SetAuthenticationCredentials("bank\\e82331", "123sindy^");
            alert.Accept();

            List<IWebElement> elements = ie.FindElements(By.CssSelector("div.row.node.article")).ToList();
            foreach (IWebElement element in elements)
            {
                IWebElement link = element.FindElement(By.XPath("./div/a"));
                String href = link.GetAttribute("href");
                if (href.EndsWith(".aspx"))
                {
                    link.Click();

                    var newWindow = ie.SwitchTo().ActiveElement();
                    wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[class='article-content']")));
                    IWebElement content = newWindow.FindElement(By.CssSelector("[class='article-content']"));
                    String html = content.GetAttribute("outerHTML");
                    Console.WriteLine(html);
                    StreamWriter file = new System.IO.StreamWriter(@"C:\Users\e82331\Documents\htmls.csv");
                }
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            ie.Quit();
        }
    }
}
