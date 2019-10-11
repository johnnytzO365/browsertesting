using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using System.Configuration;

namespace SeleniumTutorial
{
    class NewsPage
    {
        IWebDriver driver;
        WebDriverWait wait;
        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
        }

        [Test]
        public void CheckNewsOrderEl()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]) + "el/news");

            List<IWebElement> NewsListDates = null;
            try
            {
                wait.Until(ExpectedConditions.ElementExists(By.ClassName("date")));
                NewsListDates = driver.FindElements(By.ClassName("date")).ToList();
            }
            catch
            {
                Assert.Fail("Couldn't find Dates of News Articles!");
            }
            List<DateTime> allDates = new List<DateTime>();
            
            for (int i = 0; i < NewsListDates.Count; i++)
            {
                String thisDateString = NewsListDates[i].Text;
                if (thisDateString != null && thisDateString != "")
                {
                    DateTime thisDate = Convert.ToDateTime(thisDateString);
                    allDates.Add(thisDate);
                }
            }
            for (int i = 1; i < allDates.Count; i++)
            {
                if(allDates[i-1]<allDates[i])
                    Assert.Fail("The Dates are not in the correct order!");
            }
        }

        [Test]
        public void CheckNewsOrderEn()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]) + "en/news");

            List<IWebElement> NewsListDates = null;
            try
            {
                wait.Until(ExpectedConditions.ElementExists(By.ClassName("date")));
                NewsListDates = driver.FindElements(By.ClassName("date")).ToList();
            }
            catch
            {
                Assert.Fail("Couldn't find Dates of News Articles!");
            }
            Console.WriteLine("OK");
            List<DateTime> allDates = new List<DateTime>();

            for (int i = 0; i < NewsListDates.Count; i++)
            {
                String thisDateString = NewsListDates[i].Text;
                if (thisDateString != null && thisDateString != "")
                {
                    DateTime thisDate = Convert.ToDateTime(thisDateString);
                    allDates.Add(thisDate);
                }
            }
            for (int i = 1; i < allDates.Count; i++)
            {
                if (allDates[i - 1] < allDates[i])
                    Assert.Fail("The Dates are not in the correct order!");
            }
        }

        [Test]
        public void CheckNewsSearchByKeyWordEl()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]) + "el/news");

            try
            {
                String SearchSelector = "//*[@id='ctl00_PlaceHolderCustomMainContentContainer_PlaceHolderMain_ctl00_DefaultMobilePanel_searchH3']/span";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(SearchSelector)));
                IWebElement Search = driver.FindElement(By.XPath(SearchSelector));
                Search.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Search Arrow to Click!");
            }
            try
            {
                String keyWordSelector = "[id$='KeywordTextBox']";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(keyWordSelector)));
                IWebElement keyWord = driver.FindElement(By.CssSelector(keyWordSelector));
                keyWord.SendKeys("NewsTest");
            }
            catch
            {
                Assert.Fail("Couldn't find Key Word Input Box!");
            }
            try
            {
                String searchSubmitSelector = "[id$='SubmitButton']";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(searchSubmitSelector)));
                IWebElement searchSubmit = driver.FindElement(By.CssSelector(searchSubmitSelector));
                searchSubmit.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Search Button!");
            }
            try
            {
                String titleSelector = "[class='title field']";
                List<IWebElement> title = driver.FindElements(By.CssSelector(titleSelector)).ToList();
                for (int i = 0; i < title.Count; i++)
                {
                    IWebElement text = title[i].FindElement(By.XPath("./h3/a"));
                    String titleText = text.Text;
                    Assert.True(titleText.Contains("NewsTest"));
                }
            }
            catch
            {
                Assert.Fail("Couldn't find News Titles!");
            }
        }

        [Test]
        public void CheckNewsSearchByKeyWordEn()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]) + "en/news");

            try
            {
                String SearchSelector = "//*[@id='ctl00_PlaceHolderCustomMainContentContainer_PlaceHolderMain_ctl00_DefaultMobilePanel_searchH3']/span";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(SearchSelector)));
                IWebElement Search = driver.FindElement(By.XPath(SearchSelector));
                Search.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Search Arrow to Click!");
            }
            try
            {
                String keyWordSelector = "[id$='KeywordTextBox']";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(keyWordSelector)));
                IWebElement keyWord = driver.FindElement(By.CssSelector(keyWordSelector));
                keyWord.SendKeys("NewsTest");
            }
            catch
            {
                Assert.Fail("Couldn't find Key Word Input Box!");
            }
            try
            {
                String searchSubmitSelector = "[id$='SubmitButton']";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(searchSubmitSelector)));
                IWebElement searchSubmit = driver.FindElement(By.CssSelector(searchSubmitSelector));
                searchSubmit.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Search Button!");
            }
            try
            {
                String titleSelector = "[class='title field']";
                List<IWebElement> title = driver.FindElements(By.CssSelector(titleSelector)).ToList();
                for (int i = 0; i < title.Count; i++)
                {
                    IWebElement text = title[i].FindElement(By.XPath("./h3/a"));
                    String titleText = text.Text;
                    Assert.True(titleText.Contains("NewsTest"));
                }
            }
            catch
            {
                Assert.Fail("Couldn't find News Titles!");
            }
        }

        [Test]
        public void CheckNewsSearchByDateEl()
        {

        }

        [Test]
        public void CheckNewsSearchByDateEn()
        {

        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
