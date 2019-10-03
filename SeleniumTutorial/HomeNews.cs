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
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
        }

        [Test]
        public void CheckHomeNewsWebPartEnglishText()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en");
            IWebElement news = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[2]/div/div/div/div/div/div/h3/a"));
            if (news != null)
            {
                string displayText = news.Text;
                Assert.AreEqual("News & Announcements", displayText);
            }
            else
                Assert.Fail("Couldn't find what you are looking for!");
        }

        [Test]
        public void CheckHomeNewsWebPartGreekText()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            IWebElement news = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[2]/div/div/div/div/div/div/h3/a"));
            if (news != null)
            {
                string displayText = news.Text;
                Assert.AreEqual("Νέα & Ανακοινώσεις", displayText);
            }
            else
                Assert.Fail("Couldn't find what you are looking for!");
        }

        [Test]
        public void CheckHomeNewsWebPartNodeCountEl()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            IWebElement news = driver.FindElement(By.CssSelector("[id^='ctl00_SPWebPartManager1']"));
            if (news != null)
            {
                List<IWebElement> listNodes = news.FindElements(By.XPath("./div/div/div/div/ul/li")).ToList();  //count the elements on the list at el

                driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/news/Pages/Forms/AllItems.aspx");  //count the approved pages
                List<IWebElement> listPages = driver.FindElements(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr/td[9]")).ToList();
                int count = 0;
                for (int i = 0; i < listPages.Count; i++)
                    if (String.Equals(listPages[i].Text, "Εγκεκριμένα"))
                        count++;

                driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el/news");  //count the elements on the list at el/news
                List<IWebElement> listNews = driver.FindElements(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[3]/div[2]/div")).ToList();

                Assert.AreEqual(count - 1, listNodes.Count, listNews.Count);  //compare, pages must be -1 because of Default.aspx
            }
            else
                Assert.Fail("Couldn't find what you are looking for!");
        }

        [Test]
        public void CheckHomeNewsWebPartNodeCountEn()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en");
            IWebElement news = driver.FindElement(By.CssSelector("[id^='ctl00_SPWebPartManager1']"));
            if (news != null)
            {
                List<IWebElement> listNodes = news.FindElements(By.XPath("./div/div/div/div/ul/li")).ToList();  //count the elements on the list at el

                driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/news/Pages/Forms/AllItems.aspx");  //count the approved pages
                List<IWebElement> listPages = driver.FindElements(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr/td[9]")).ToList();
                int count = 0;
                for (int i = 0; i < listPages.Count; i++)
                    if (String.Equals(listPages[i].Text, "Approved"))
                        count++;

                driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en/news");  //count the elements on the list at el/news
                List<IWebElement> listNews = driver.FindElements(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[3]/div[2]/div")).ToList();

                Assert.AreEqual(count - 1, listNodes.Count, listNews.Count);  //compare, pages must be -1 because of Default.aspx
            }
            else
                Assert.Fail("Couldn't find what you are looking for!");
        }

        [Test]
        public void CheckHomeNewsWebPartLinkEn()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en");
            IWebElement link = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[2]/div/div/div/div/div/div/h3/a"));
            if (link != null)
            {
                link.Click();
                Assert.IsTrue(driver.Url.Contains("vm-sp2013/en/news"));
            }
            else
                Assert.Fail("Couldn't find what you are looking for!");
        }

        [Test]
        public void CheckHomeNewsWebPartLinkEl()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            IWebElement link = driver.FindElement(By.XPath("//*[@id='DeltaPlaceHolderMain']/div/div[3]/div[2]/div/div/div/div/div/div/h3/a"));
            if (link != null)
            {
                link.Click();
                Assert.IsTrue(driver.Url.Contains("vm-sp2013/el/news"));
            }
            else
                Assert.Fail("Couldn't find what you are looking for!");
        }
        
        [Test]
        public void CheckHomeNewsAddAndDeleteEn()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/news/Pages/Forms/AllItems.aspx");

            ClickFilesOnRibbon();

            Thread.Sleep(1000);
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.New.NewDocument']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.New.NewDocument']")));
            IWebElement newDocument = driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.New.NewDocument']"));
            if (newDocument != null)
                newDocument.FindElement(By.XPath("./a[1]/span")).Click();
            else
                Assert.Fail("Couldn't find New Document on Ribbon!");

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[name$='titleTextBox']")));  //wait for the new form to appear

            IWebElement Title = driver.FindElement(By.CssSelector("[name$='titleTextBox']"));
            if(Title != null)
                Title.SendKeys("Test");
             else
                Assert.Fail("Couldn't find Title input box!");

            IWebElement Create = driver.FindElement(By.CssSelector("[id$='buttonCreatePage']"));
            if(Create!=null)
                Create.Click();
            else
                Assert.Fail("Couldn't find Create button!");

            CheckTestItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.EditCheckout.CheckIn']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.EditCheckout.CheckIn']")));
            IWebElement CheckIn = driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.EditCheckout.CheckIn']"));
            if (CheckIn != null)
                CheckIn.Click();
            else
                Assert.Fail("Couldn't Find Check In Button!");

            IWebDriver CheckinDialog = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));  
            WebDriverWait CheckinWait = new WebDriverWait(CheckinDialog, TimeSpan.FromSeconds(40));

            CheckinWait.Until(ExpectedConditions.ElementIsVisible(By.Id("ActionCheckinPublish")));  //wait for dialog to appear
            CheckinWait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ActionCheckinPublish")));
            IWebElement publish = CheckinDialog.FindElement(By.Id("ActionCheckinPublish"));
            if (publish != null)
                publish.Click();
            else
                Assert.Fail("Couldn't find Publish Major Version Radio Button");

            IWebElement OK = CheckinDialog.FindElement(By.XPath("//*[@id='CheckinOk']"));
            if (OK != null)
                OK.Click();
            else
                Assert.Fail("Couldn't find OK button!");

            CheckTestItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.Workflow.Moderate']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.Workflow.Moderate']")));
            IWebElement approve_reject = driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.Workflow.Moderate']"));
            if (approve_reject != null)
                approve_reject.Click();
            else
                Assert.Fail("Couldn't find Approve-Reject button on Ribbon");

            IWebDriver ApproveDialog = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait ApproveWait = new WebDriverWait(ApproveDialog, TimeSpan.FromSeconds(40));

            ApproveWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id$='RadioBtnApprovalStatus_0']")));  //wait for dialog window to appear
            ApproveWait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id$='RadioBtnApprovalStatus_0']")));
            IWebElement approve = ApproveDialog.FindElement(By.CssSelector("[id$='RadioBtnApprovalStatus_0']"));
            if (approve != null)
                approve.Click();
            else
                Assert.Fail("Couldn't find Approve Radio Button!");

            OK = ApproveDialog.FindElement(By.CssSelector("[id$='RptControls_BtnSubmit']"));
            if (OK != null)
                OK.Click();
            else
                Assert.Fail("Couldn't find OK button!");

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);

            CheckHomeNewsWebPartNodeCountEn();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/news/Pages/Forms/AllItems.aspx");

            CheckTestItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.Manage.Delete']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.Manage.Delete']")));
            IWebElement delete = driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.Manage.Delete']"));
            if (delete != null)
                delete.Click();
            else
                Assert.Fail("Couldn't find Delete Button!");

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);

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
            IWebElement newDocument = driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.New.NewDocument']"));
            if (newDocument != null)
                newDocument.FindElement(By.XPath("./a[1]/span")).Click();
            else
                Assert.Fail("Couldn't find New Document on Ribbon!");

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[name$='titleTextBox']")));  //wait for the new form to appear

            IWebElement Title = driver.FindElement(By.CssSelector("[name$='titleTextBox']"));
            if(Title != null)
                Title.SendKeys("Test");
             else
                Assert.Fail("Couldn't find Title input box!");

            IWebElement Create = driver.FindElement(By.CssSelector("[id$='buttonCreatePage']"));
            if(Create!=null)
                Create.Click();
            else
                Assert.Fail("Couldn't find Create button!");

            CheckTestItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.EditCheckout.CheckIn']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.EditCheckout.CheckIn']")));
            IWebElement CheckIn = driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.EditCheckout.CheckIn']"));
            if (CheckIn != null)
                CheckIn.Click();
            else
                Assert.Fail("Couldn't Find Check In Button!");

            IWebDriver CheckinDialog = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));  
            WebDriverWait CheckinWait = new WebDriverWait(CheckinDialog, TimeSpan.FromSeconds(40));

            CheckinWait.Until(ExpectedConditions.ElementIsVisible(By.Id("ActionCheckinPublish")));  //wait for dialog to appear
            CheckinWait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ActionCheckinPublish")));
            IWebElement publish = CheckinDialog.FindElement(By.Id("ActionCheckinPublish"));
            if (publish != null)
                publish.Click();
            else
                Assert.Fail("Couldn't find Publish Major Version Radio Button");

            IWebElement OK = CheckinDialog.FindElement(By.XPath("//*[@id='CheckinOk']"));
            if (OK != null)
                OK.Click();
            else
                Assert.Fail("Couldn't find OK button!");

            CheckTestItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.Workflow.Moderate']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.Workflow.Moderate']")));
            IWebElement approve_reject = driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.Workflow.Moderate']"));
            if (approve_reject != null)
                approve_reject.Click();
            else
                Assert.Fail("Couldn't find Approve-Reject button on Ribbon");

            IWebDriver ApproveDialog = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait ApproveWait = new WebDriverWait(ApproveDialog, TimeSpan.FromSeconds(40));

            ApproveWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id$='RadioBtnApprovalStatus_0']")));  //wait for dialog window to appear
            ApproveWait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id$='RadioBtnApprovalStatus_0']")));
            IWebElement approve = ApproveDialog.FindElement(By.CssSelector("[id$='RadioBtnApprovalStatus_0']"));
            if (approve != null)
                approve.Click();
            else
                Assert.Fail("Couldn't find Approve Radio Button!");

            OK = ApproveDialog.FindElement(By.CssSelector("[id$='RptControls_BtnSubmit']"));
            if (OK != null)
                OK.Click();
            else
                Assert.Fail("Couldn't find OK button!");

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);

            CheckHomeNewsWebPartNodeCountEl();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/news/Pages/Forms/AllItems.aspx");

            CheckTestItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id^='Ribbon.Documents.Manage.Delete']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.Documents.Manage.Delete']")));
            IWebElement delete = driver.FindElement(By.CssSelector("[id^='Ribbon.Documents.Manage.Delete']"));
            if (delete != null)
                delete.Click();
            else
                Assert.Fail("Couldn't find Delete Button!");

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);

            CheckHomeNewsWebPartNodeCountEl();
        }
        
        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        public void ClickFilesOnRibbon()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Document-title']/a")));  //wait for ribbon to appear
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Document-title']/a")));
            IWebElement files = driver.FindElement(By.XPath("//*[@id='Ribbon.Document-title']/a"));
            if (files != null)
                files.Click();
            else
                Assert.Fail("Couldn't find Files on Ribbon!");
        }

        public void CheckTestItem()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody")));   //wait for the list to appear
            List<IWebElement> list = driver.FindElements(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr/td")).ToList();
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
                Assert.Fail("Couldn't find item named \"Test\"");
        }
    }
}
