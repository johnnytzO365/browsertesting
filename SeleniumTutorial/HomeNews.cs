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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }

        [Test]
        public void CheckHomeNewsWebPartEnglishText()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en");
            IWebElement ls = driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_14ee3e30_d47d_43be_8486_acfbfbf97584']/div/h3/a"));
            string displayText = ls.Text;
            Assert.AreEqual("News & Announcements", displayText);
        }

        [Test]
        public void CheckHomeNewsWebPartGreekText()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            IWebElement ls = driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_14ee3e30_d47d_43be_8486_acfbfbf97584']/div/h3/a"));
            string displayText = ls.Text;
            Assert.AreEqual("Νέα & Ανακοινώσεις", displayText);

        }

        [Test]
        public void CheckHomeNewsWebPartNodeCountEl()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            List<IWebElement> listNodes = driver.FindElements(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_14ee3e30_d47d_43be_8486_acfbfbf97584']/div/div/div/div/ul/li")).ToList();

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
            List<IWebElement> listNodes = driver.FindElements(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_14ee3e30_d47d_43be_8486_acfbfbf97584']/div/div/div/div/ul/li")).ToList();

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
            IWebElement ls = driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_14ee3e30_d47d_43be_8486_acfbfbf97584']/div/h3/a"));

            ls.Click();
            Assert.IsTrue(driver.Url.Contains("vm-sp2013/en/news"));
        }

        [Test]
        public void CheckHomeNewsWebPartLinkEl()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            IWebElement ls = driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_14ee3e30_d47d_43be_8486_acfbfbf97584']/div/h3/a"));

            ls.Click();
            Assert.IsTrue(driver.Url.Contains("vm-sp2013/el/news"));
        }

        [Test]
        public void CheckHomeNewsAddAndDeleteEn()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/news/Pages/Forms/AllItems.aspx");

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")));
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")).Click();     //Click New Document

            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("ctl00$PlaceHolderMain$pageTitleSection$ctl01$titleTextBox")));
            driver.FindElement(By.Name("ctl00$PlaceHolderMain$pageTitleSection$ctl01$titleTextBox")).SendKeys("Test");
            driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_ctl00_RptControls_buttonCreatePage']")).Click();    //Click Create

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            driver.FindElement(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")).Click();  //Choose the last element of the list

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.EditCheckout.CheckIn-Small']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.EditCheckout.CheckIn-Small']")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.EditCheckout.CheckIn-Small']")).Click();  //Choose Check in

            IWebDriver dialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait dialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            dialogWait.Until(ExpectedConditions.ElementIsVisible(By.Id("ActionCheckinPublish")));
            dialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ActionCheckinPublish")));
            dialogDriver.FindElement(By.Id("ActionCheckinPublish")).Click();  //Click Major Version

            dialogWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='CheckinOk']")));
            dialogDriver.FindElement(By.XPath("//*[@id='CheckinOk']")).Click();  //Click OK

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            driver.FindElement(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")).Click();  //Choose the last element of the list

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.Workflow.Moderate-Small']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.Workflow.Moderate-Small']")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.Workflow.Moderate-Small']")).Click();  //Choose Approve-Reject

            IWebDriver newDialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait newDialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            newDialogWait.Until(ExpectedConditions.ElementIsVisible(By.Id("ctl00_PlaceHolderMain_approveDescription_ctl01_RadioBtnApprovalStatus_0")));
            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ctl00_PlaceHolderMain_approveDescription_ctl01_RadioBtnApprovalStatus_0")));
            newDialogDriver.FindElement(By.Id("ctl00_PlaceHolderMain_approveDescription_ctl01_RadioBtnApprovalStatus_0")).Click();  //Click Approve

            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ctl00_PlaceHolderMain_ctl00_RptControls_BtnSubmit")));
            newDialogDriver.FindElement(By.Id("ctl00_PlaceHolderMain_ctl00_RptControls_BtnSubmit")).Click();  //Click OK

            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);

            CheckHomeNewsWebPartNodeCountEn();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/news/Pages/Forms/AllItems.aspx");

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            driver.FindElement(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")).Click();  //Choose the last element of the list

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.Manage.Delete-Small']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.Manage.Delete-Small']")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.Manage.Delete-Small']")).Click();  //Choose Delete

            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);

            CheckHomeNewsWebPartNodeCountEn();
        }

        [Test]
        public void CheckHomeNewsAddAndDeleteEl()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/news/Pages/Forms/AllItems.aspx");

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Medium']/a[1]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Medium']/a[1]")));
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Medium']/a[1]")).Click();     //Click New Document

            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("ctl00$PlaceHolderMain$pageTitleSection$ctl01$titleTextBox")));
            driver.FindElement(By.Name("ctl00$PlaceHolderMain$pageTitleSection$ctl01$titleTextBox")).SendKeys("Test");
            driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_ctl00_RptControls_buttonCreatePage']")).Click();    //Click Create

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            driver.FindElement(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")).Click();  //Choose the last element of the list

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.EditCheckout.CheckIn-Small']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.EditCheckout.CheckIn-Small']")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.EditCheckout.CheckIn-Small']")).Click();  //Choose Check in

            IWebDriver dialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait dialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            dialogWait.Until(ExpectedConditions.ElementIsVisible(By.Id("ActionCheckinPublish")));
            dialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ActionCheckinPublish")));
            dialogDriver.FindElement(By.Id("ActionCheckinPublish")).Click();  //Click Major Version

            dialogWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='CheckinOk']")));
            dialogDriver.FindElement(By.XPath("//*[@id='CheckinOk']")).Click();  //Click OK

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            driver.FindElement(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")).Click();  //Choose the last element of the list

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.Workflow-PopupAnchor-Large']/span[2]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.Workflow-PopupAnchor-Large']/span[2]")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.Workflow-PopupAnchor-Large']/span[2]")).Click();  //Choose Approve-Reject

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.Workflow.Moderate-Medium']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.Workflow.Moderate-Medium']")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.Workflow.Moderate-Medium']")).Click();  //Choose Approve-Reject

            IWebDriver newDialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait newDialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            newDialogWait.Until(ExpectedConditions.ElementIsVisible(By.Id("ctl00_PlaceHolderMain_approveDescription_ctl01_RadioBtnApprovalStatus_0")));
            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ctl00_PlaceHolderMain_approveDescription_ctl01_RadioBtnApprovalStatus_0")));
            newDialogDriver.FindElement(By.Id("ctl00_PlaceHolderMain_approveDescription_ctl01_RadioBtnApprovalStatus_0")).Click();  //Click Approve

            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ctl00_PlaceHolderMain_ctl00_RptControls_BtnSubmit")));
            newDialogDriver.FindElement(By.Id("ctl00_PlaceHolderMain_ctl00_RptControls_BtnSubmit")).Click();  //Click OK

            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);

            CheckHomeNewsWebPartNodeCountEl();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/news/Pages/Forms/AllItems.aspx");

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            driver.FindElement(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")).Click();  //Choose the last element of the list

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.Manage.Delete-Small']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.Manage.Delete-Small']")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.Manage.Delete-Small']")).Click();  //Choose Delete

            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);

            CheckHomeNewsWebPartNodeCountEl();
        }

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
