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
using System.Configuration;

namespace SeleniumTutorial
{
    class HomeNews
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
        public void CheckHomeNewsWebPartEnglishText()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en");
            try
            {
                String newsSelector = "//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[2]/div/div/div/div/div/div/h3/a";
                IWebElement news = driver.FindElement(By.XPath(newsSelector));
                string displayText = news.Text;
                Assert.AreEqual("News & Announcements", displayText);
            }
            catch
            {
                Assert.Fail("Couldn't find News on HomePage!");
            }
        }

        [Test]
        public void CheckHomeNewsWebPartGreekText()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            try
            {
                String newsSelector = "//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[2]/div/div/div/div/div/div/h3/a";
                IWebElement news = driver.FindElement(By.XPath(newsSelector));
                string displayText = news.Text;
                Assert.AreEqual("Νέα & Ανακοινώσεις", displayText);
            }
            catch
            {
                Assert.Fail("Couldn't find News on HomePage!");
            }
        }

        [Test]
        public void CheckHomeNewsWebPartNodeCountEl()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            IWebElement news = null;
            try
            {
                String newsSelector = "[id^='ctl00_SPWebPartManager1']";
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector(newsSelector)));
                news = driver.FindElement(By.CssSelector(newsSelector));
            }
            catch
            {
                Assert.Fail("Couldn't find News on Home Page!");
            }
            List<IWebElement> listNodes = null;
            try
            {
                String listNodesSelector = "./div/div/div/div/ul/li";
                listNodes = news.FindElements(By.XPath(listNodesSelector)).ToList();  //count the elements on the list at el
            }
            catch
            {
                Assert.Fail("Couldn't find the list of the News on Home Page!");
            }
                
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/news/Pages/Forms/AllItems.aspx");  //count the approved pages
            
            List<IWebElement> listPages = null;
            try
            {
                String listPagesSelector = "//*[@id='onetidDoclibViewTbl0']/tbody/tr/td[9]";
                wait.Until(ExpectedConditions.ElementExists(By.XPath(listPagesSelector)));
                listPages = driver.FindElements(By.XPath(listPagesSelector)).ToList();
            }
            catch
            {
                Assert.Fail("Couldn't find the list of the Pages!");
            }
            int count = 0;
            for (int i = 0; i < listPages.Count; i++)
                if (String.Equals(listPages[i].Text, "Εγκεκριμένα"))
                    count++;

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el/news");  //count the elements on the list at el/news
                
            List<IWebElement> listNews = null;
            try
            {
                String listNewsSelector = "//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[3]/div[2]/div";
                wait.Until(ExpectedConditions.ElementExists(By.XPath(listNewsSelector)));
                listNews = driver.FindElements(By.XPath(listNewsSelector)).ToList();
            }
            catch
            {
                Assert.Fail("Couldn't find the list of News in News Page!");
            }

            Assert.AreEqual(count - 1, listNodes.Count, listNews.Count);  //compare, pages must be -1 because of Default.aspx
        }

        [Test]
        public void CheckHomeNewsWebPartNodeCountEn()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en");
            IWebElement news = null;
            try
            {
                String newsSelector = "[id^='ctl00_SPWebPartManager1']";
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector(newsSelector)));
                news = driver.FindElement(By.CssSelector(newsSelector));
            }
            catch
            {
                Assert.Fail("Couldn't find News on Home Page!");
            }
            List<IWebElement> listNodes = null;
            try
            {
                String listNodesSelector = "./div/div/div/div/ul/li";
                listNodes = news.FindElements(By.XPath(listNodesSelector)).ToList();  //count the elements on the list at el
            }
            catch
            {
                Assert.Fail("Couldn't find the list of the News on Home Page!");
            }

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/news/Pages/Forms/AllItems.aspx");  //count the approved pages

            List<IWebElement> listPages = null;
            try
            {
                String listPagesSelector = "//*[@id='onetidDoclibViewTbl0']/tbody/tr/td[9]";
                wait.Until(ExpectedConditions.ElementExists(By.XPath(listPagesSelector)));
                listPages = driver.FindElements(By.XPath(listPagesSelector)).ToList();
            }
            catch
            {
                Assert.Fail("Couldn't find the list of the Pages!");
            }
            int count = 0;
            for (int i = 0; i < listPages.Count; i++)
                if (String.Equals(listPages[i].Text, "Approved"))
                    count++;

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en/news");  //count the elements on the list at el/news

            List<IWebElement> listNews = null;
            try
            {
                String listNewsSelector = "//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[3]/div[2]/div";
                wait.Until(ExpectedConditions.ElementExists(By.XPath(listNewsSelector)));
                listNews = driver.FindElements(By.XPath(listNewsSelector)).ToList();
            }
            catch
            {
                Assert.Fail("Couldn't find the list of News in News Page!");
            }

            Assert.AreEqual(count - 1, listNodes.Count, listNews.Count);  //compare, pages must be -1 because of Default.aspx
        }

        [Test]
        public void CheckHomeNewsWebPartLinkEn()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en");
            String linkSelector = "//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[2]/div/div/div/div/div/div/h3/a";
            try
            {
                wait.Until(ExpectedConditions.ElementExists(By.XPath(linkSelector)));
                IWebElement link = driver.FindElement(By.XPath(linkSelector));
                link.Click();
                Assert.IsTrue(driver.Url.Contains("vm-sp2013/en/news"));
            }
            catch
            {
                Assert.Fail("Couldn't find what you are looking for!");
            }
        }

        [Test]
        public void CheckHomeNewsWebPartLinkEl()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            String linkSelector = "//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[2]/div/div/div/div/div/div/h3/a";
            try
            {
                wait.Until(ExpectedConditions.ElementExists(By.XPath(linkSelector)));
                IWebElement link = driver.FindElement(By.XPath(linkSelector));
                link.Click();
                Assert.IsTrue(driver.Url.Contains("vm-sp2013/el/news"));
            }
            catch
            {
                Assert.Fail("Couldn't find what you are looking for!");
            }
        }
        
        [Test]
        public void CheckHomeNewsAddAndDeleteEn()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/news/Pages/Forms/AllItems.aspx");

            if (CheckTestItem() == -0)
            {
                ClickFilesOnRibbon();
                DeleteItem();
            }

            ClickFilesOnRibbon();

            String newDocumentSelector = "[id^='Ribbon.Documents.New.NewDocument']";
            try
            {
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector(newDocumentSelector)));
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(newDocumentSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(newDocumentSelector)));
                IWebElement newDocument = driver.FindElement(By.CssSelector(newDocumentSelector));
                newDocument.FindElement(By.XPath("./a[1]/span")).Click();
            }
            catch
            {
                Assert.Fail("Couldn't find New Document on Ribbon!");
            }
            try
            {
                String titleSelector = "[name$='titleTextBox']";
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(titleSelector)));
                IWebElement Title = driver.FindElement(By.CssSelector(titleSelector));
                Title.SendKeys("Test");
            }
            catch
            {
                Assert.Fail("Couldn't find Title input box!");
            }
            try
            {
                String createSelector = "[id$='buttonCreatePage']";
                IWebElement Create = driver.FindElement(By.CssSelector(createSelector));
                Create.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Create button!");
            }

            CheckTestItem();

            ClickFilesOnRibbon();

            try
            {
                String CheckInSelector = "[id^='Ribbon.Documents.EditCheckout.CheckIn']";
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(CheckInSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CheckInSelector)));
                IWebElement CheckIn = driver.FindElement(By.CssSelector(CheckInSelector));
                CheckIn.Click();
            }
            catch
            {
                Assert.Fail("Couldn't Find Check In Button!");
            }

            IWebDriver CheckinDialog = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));  
            WebDriverWait CheckinWait = new WebDriverWait(CheckinDialog, TimeSpan.FromSeconds(40));

            try
            {
                String publishSelector = "ActionCheckinPublish";
                CheckinWait.Until(ExpectedConditions.ElementIsVisible(By.Id(publishSelector)));  //wait for dialog to appear
                CheckinWait.Until(ExpectedConditions.ElementToBeClickable(By.Id(publishSelector)));
                IWebElement publish = CheckinDialog.FindElement(By.Id(publishSelector));
                publish.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Publish Major Version Radio Button");
            }

            try
            {
                string OKSelector = "//*[@id='CheckinOk']";
                IWebElement OK = CheckinDialog.FindElement(By.XPath(OKSelector));
                OK.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find OK button!");
            }

            CheckTestItem();

            ClickFilesOnRibbon();

            try
            {
                String approve_rejectSelector = "[id^='Ribbon.Documents.Workflow.Moderate']";
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(approve_rejectSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(approve_rejectSelector)));
                IWebElement approve_reject = driver.FindElement(By.CssSelector(approve_rejectSelector));
                approve_reject.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Approve-Reject button on Ribbon");
            }

            IWebDriver ApproveDialog = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait ApproveWait = new WebDriverWait(ApproveDialog, TimeSpan.FromSeconds(40));

            try
            {
                String approveSelector = "[id$='RadioBtnApprovalStatus_0']";
                ApproveWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(approveSelector)));  //wait for dialog window to appear
                ApproveWait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(approveSelector)));
                IWebElement approve = ApproveDialog.FindElement(By.CssSelector(approveSelector));
                approve.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Approve Radio Button!");
            }

            try
            {
                String OKSelector = "[id$='RptControls_BtnSubmit']";
                IWebElement OK = ApproveDialog.FindElement(By.CssSelector(OKSelector));
                OK.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find OK button!");
            }

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);

            CheckHomeNewsWebPartNodeCountEn();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/news/Pages/Forms/AllItems.aspx");

            CheckTestItem();

            ClickFilesOnRibbon();

            DeleteItem();

            CheckHomeNewsWebPartNodeCountEn();
        }
        
        [Test]
        public void CheckHomeNewsAddAndDeleteEl()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/news/Pages/Forms/AllItems.aspx");

            if (CheckTestItem() == 0)
            {
                ClickFilesOnRibbon();
                DeleteItem();
            }

            ClickFilesOnRibbon();

            String newDocumentSelector = "[id^='Ribbon.Documents.New.NewDocument']";
            try
            {
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector(newDocumentSelector)));
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(newDocumentSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(newDocumentSelector)));
                IWebElement newDocument = driver.FindElement(By.CssSelector(newDocumentSelector));
                newDocument.FindElement(By.XPath("./a[1]/span")).Click();
            }
            catch
            {
                Assert.Fail("Couldn't find New Document on Ribbon!");
            }
            try
            {
                String titleSelector = "[name$='titleTextBox']";
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(titleSelector)));
                IWebElement Title = driver.FindElement(By.CssSelector(titleSelector));
                Title.SendKeys("Test");
            }
            catch
            {
                Assert.Fail("Couldn't find Title input box!");
            }
            try
            {
                String createSelector = "[id$='buttonCreatePage']";
                IWebElement Create = driver.FindElement(By.CssSelector(createSelector));
                Create.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Create button!");
            }

            CheckTestItem();

            ClickFilesOnRibbon();

            try
            {
                String CheckInSelector = "[id^='Ribbon.Documents.EditCheckout.CheckIn']";
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(CheckInSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CheckInSelector)));
                IWebElement CheckIn = driver.FindElement(By.CssSelector(CheckInSelector));
                CheckIn.Click();
            }
            catch
            {
                Assert.Fail("Couldn't Find Check In Button!");
            }

            IWebDriver CheckinDialog = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait CheckinWait = new WebDriverWait(CheckinDialog, TimeSpan.FromSeconds(40));

            try
            {
                String publishSelector = "ActionCheckinPublish";
                CheckinWait.Until(ExpectedConditions.ElementIsVisible(By.Id(publishSelector)));  //wait for dialog to appear
                CheckinWait.Until(ExpectedConditions.ElementToBeClickable(By.Id(publishSelector)));
                IWebElement publish = CheckinDialog.FindElement(By.Id(publishSelector));
                publish.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Publish Major Version Radio Button");
            }

            try
            {
                string OKSelector = "//*[@id='CheckinOk']";
                IWebElement OK = CheckinDialog.FindElement(By.XPath(OKSelector));
                OK.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find OK button!");
            }

            CheckTestItem();

            ClickFilesOnRibbon();

            try
            {
                String approve_rejectSelector = "[id^='Ribbon.Documents.Workflow.Moderate']";
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(approve_rejectSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(approve_rejectSelector)));
                IWebElement approve_reject = driver.FindElement(By.CssSelector(approve_rejectSelector));
                approve_reject.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Approve-Reject button on Ribbon");
            }

            IWebDriver ApproveDialog = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait ApproveWait = new WebDriverWait(ApproveDialog, TimeSpan.FromSeconds(40));

            try
            {
                String approveSelector = "[id$='RadioBtnApprovalStatus_0']";
                ApproveWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(approveSelector)));  //wait for dialog window to appear
                ApproveWait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(approveSelector)));
                IWebElement approve = ApproveDialog.FindElement(By.CssSelector(approveSelector));
                approve.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Approve Radio Button!");
            }

            try
            {
                String OKSelector = "[id$='RptControls_BtnSubmit']";
                IWebElement OK = ApproveDialog.FindElement(By.CssSelector(OKSelector));
                OK.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find OK button!");
            }

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);

            CheckHomeNewsWebPartNodeCountEl();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/news/Pages/Forms/AllItems.aspx");

            CheckTestItem();

            ClickFilesOnRibbon();

            DeleteItem();

            CheckHomeNewsWebPartNodeCountEl();
        }
        
        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        public void ClickFilesOnRibbon()
        {
            try
            {
                String filesSelector = "//*[@id='Ribbon.Document-title']/a";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(filesSelector)));  //wait for ribbon to appear
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(filesSelector)));
                IWebElement files = driver.FindElement(By.XPath(filesSelector));
                files.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Files on Ribbon!");
            }
        }

        public int CheckTestItem()
        {
            List<IWebElement> list = null;
            try
            {
                String listSelector = "//*[@id='onetidDoclibViewTbl0']/tbody/tr/td";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(listSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(listSelector)));   //wait for the list to appear
                list = driver.FindElements(By.XPath(listSelector)).ToList();
            }
            catch
            {
                Assert.Fail("Couldn't find list table!");
            }
            int i;
            for (i = 0; i < list.Count; i++)
            {
                IWebElement item = list[i];
                if (item.Text == "Test")
                {
                    item.Click();
                    break;
                }
            }

            if (i == list.Count)
                return -1;
            else
                return 0;
        }

        public void DeleteItem()
        {
            try
            {
                String deleteSelector = "[id^='Ribbon.Documents.Manage.Delete']";
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(deleteSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(deleteSelector)));
                IWebElement delete = driver.FindElement(By.CssSelector(deleteSelector));
                delete.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Delete Button!");
            }

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);
        }
    }
}
