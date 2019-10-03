using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SeleniumTutorial
{
    class HomeNews
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("C:\\Users\\spsetup\\Documents\\Visual Studio 2012\\Projects\\SeleniumTutorial\\.nuget\\selenium.chrome.webdriver.76.0.0\\driver");
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }

        [Test]
        public void CheckHomeNewsWebPartEnglishText()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en");
            IWebElement ls = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[2]/div/div/div/div/div/div/h3/a"));
            string displayText = ls.Text;
            Assert.AreEqual("News & Announcements", displayText);
        }

        [Test]
        public void CheckHomeNewsWebPartGreekText()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            IWebElement ls = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[2]/div/div/div/div/div/div/h3/a"));
            string displayText = ls.Text;
            Assert.AreEqual("Νέα & Ανακοινώσεις", displayText);
        }

        [Test]
        public void CheckHomeNewsWebPartNodeCountEl()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            IWebElement ls = driver.FindElement(By.CssSelector("[id^='ctl00_SPWebPartManager1']"));
            List<IWebElement> listNodes = ls.FindElements(By.XPath("./div/div/div/div/ul/li")).ToList();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/news/Pages/Forms/AllItems.aspx");
            List<IWebElement> listPages = driver.FindElements(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr/td[9]")).ToList();
            int count = 0;
            for (int i = 0; i < listPages.Count; i++)
                if (String.Equals(listPages[i].Text, "Εγκεκριμένα"))
                    count++;

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el/news");
            List<IWebElement> listNews = driver.FindElements(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[3]/div[2]/div")).ToList();

            Assert.AreEqual(count - 1, listNodes.Count, listNews.Count);
        }

        [Test]
        public void CheckHomeNewsWebPartNodeCountEn()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en");
            IWebElement ls = driver.FindElement(By.CssSelector("[id^='ctl00_SPWebPartManager1']"));
            List<IWebElement> listNodes = ls.FindElements(By.XPath("./div/div/div/div/ul/li")).ToList();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/news/Pages/Forms/AllItems.aspx");
            List<IWebElement> listPages = driver.FindElements(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr/td[9]")).ToList();
            int count = 0;
            for (int i = 0; i < listPages.Count; i++)
                if (String.Equals(listPages[i].Text, "Approved"))
                    count++;

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en/news");
            List<IWebElement> listNews = driver.FindElements(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[3]/div[2]/div")).ToList();

            Assert.AreEqual(count - 1, listNodes.Count, listNews.Count);
        }

        [Test]
        public void CheckHomeNewsWebPartLinkEn()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en");
            IWebElement ls = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[2]/div/div/div/div/div/div/h3/a"));
            ls.Click();
            Assert.IsTrue(driver.Url.Contains("vm-sp2013/en/news"));
        }

        [Test]
        public void CheckHomeNewsWebPartLinkEl()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            IWebElement ls = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[2]/div/div/div/div/div/div/h3/a")); 
            ls.Click();
            Assert.IsTrue(driver.Url.Contains("vm-sp2013/el/news"));
        }
        /*
        [Test]
        public void CheckHomeNewsAddAndDeleteEn()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/news/Pages/Forms/AllItems.aspx");

            ClickFilesOnRibbon();

            Thread.Sleep(1000);
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.New.NewDocument']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.New.NewDocument']")));
            IWebElement ls = driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.New.NewDocument']"));
            driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.New.NewDocument']"));
            ls.FindElement(By.XPath("./a[1]/span")).Click();     //Click New Document

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[name$='titleTextBox']")));
            driver.FindElement(By.CssSelector("[name$='titleTextBox']")).SendKeys("Test");
            driver.FindElement(By.CssSelector("[id$='buttonCreatePage']")).Click();    //Click Create

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));
            ls = driver.FindElement(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody"));
            ls.FindElement(By.CssSelector("[title='Test']")).Click();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.EditCheckout.CheckIn']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.EditCheckout.CheckIn']")));
            driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.EditCheckout.CheckIn']")).Click();  //Choose Check in

            IWebDriver dialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait dialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            dialogWait.Until(ExpectedConditions.ElementIsVisible(By.Id("ActionCheckinPublish")));
            dialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ActionCheckinPublish")));
            dialogDriver.FindElement(By.Id("ActionCheckinPublish")).Click();  //Click Major Version

            dialogWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='CheckinOk']")));
            dialogDriver.FindElement(By.XPath("//*[@id='CheckinOk']")).Click();  //Click OK

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));
            ls = driver.FindElement(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody"));
            ls.FindElement(By.CssSelector("[title='Test']")).Click();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.Workflow.Moderate']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.Workflow.Moderate']")));
            driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.Workflow.Moderate']")).Click();  //Choose Approve-Reject

            IWebDriver newDialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait newDialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            newDialogWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id$='RadioBtnApprovalStatus_0']")));
            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id$='RadioBtnApprovalStatus_0']")));
            newDialogDriver.FindElement(By.CssSelector("[id$='RadioBtnApprovalStatus_0']")).Click();  //Click Approve

            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id$='RptControls_BtnSubmit']")));
            newDialogDriver.FindElement(By.CssSelector("[id$='RptControls_BtnSubmit']")).Click();  //Click OK

            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);

            CheckHomeNewsWebPartNodeCountEn();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/news/Pages/Forms/AllItems.aspx");

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));
            ls = driver.FindElement(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody"));
            ls.FindElement(By.CssSelector("[title='Test']")).Click();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.Manage.Delete']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.Manage.Delete']")));
            driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.Manage.Delete']")).Click();  //Choose Delete

            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);

            CheckHomeNewsWebPartNodeCountEn();
        }
        
        [Test]
        public void CheckHomeNewsAddAndDeleteEl()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/news/Pages/Forms/AllItems.aspx");

            ClickFilesOnRibbon();

            Thread.Sleep(1000);
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.New.NewDocument']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.New.NewDocument']")));
            IWebElement ls = driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.New.NewDocument']"));
            driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.New.NewDocument']"));
            ls.FindElement(By.XPath("./a[1]/span")).Click();     //Click New Document

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[name$='titleTextBox']")));
            driver.FindElement(By.CssSelector("[name$='titleTextBox']")).SendKeys("Test");
            driver.FindElement(By.CssSelector("[id$='buttonCreatePage']")).Click();    //Click Create

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));
            List<IWebElement> list = driver.FindElements(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr/td")).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                IWebElement temp = list[i];
                if (temp.Text == "Test")
                {
                    temp.Click();
                    break;
                }
            }

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.EditCheckout.CheckIn']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.EditCheckout.CheckIn']")));
            driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.EditCheckout.CheckIn']")).Click();  //Choose Check in

            IWebDriver dialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait dialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            dialogWait.Until(ExpectedConditions.ElementIsVisible(By.Id("ActionCheckinPublish")));
            dialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ActionCheckinPublish")));
            dialogDriver.FindElement(By.Id("ActionCheckinPublish")).Click();  //Click Major Version

            dialogWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='CheckinOk']")));
            dialogDriver.FindElement(By.XPath("//*[@id='CheckinOk']")).Click();  //Click OK

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));
            list = driver.FindElements(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr/td")).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                IWebElement temp = list[i];
                if (temp.Text == "Test")
                {
                    temp.Click();
                    break;
                }
            }


            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.Workflow.Moderate']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.Workflow.Moderate']")));
            driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.Workflow.Moderate']")).Click();  //Choose Approve-Reject

            IWebDriver newDialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait newDialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            newDialogWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id$='RadioBtnApprovalStatus_0']")));
            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id$='RadioBtnApprovalStatus_0']")));
            newDialogDriver.FindElement(By.CssSelector("[id$='RadioBtnApprovalStatus_0']")).Click();  //Click Approve

            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id$='RptControls_BtnSubmit']")));
            newDialogDriver.FindElement(By.CssSelector("[id$='RptControls_BtnSubmit']")).Click();  //Click OK

            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);

            CheckHomeNewsWebPartNodeCountEl();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/news/Pages/Forms/AllItems.aspx");

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));
            ls = driver.FindElement(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody"));
            ls.FindElement(By.CssSelector("[title='Test']")).Click();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.Manage.Delete']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.Manage.Delete']")));
            driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.Manage.Delete']")).Click();  //Choose Delete

            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);

            CheckHomeNewsWebPartNodeCountEl();
        }
        */
        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        public void ClickFilesOnRibbon()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Document-title']/a")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Document-title']/a")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.Document-title']/a")).Click();       //Click Files on Ribbon
        }
    }
}
