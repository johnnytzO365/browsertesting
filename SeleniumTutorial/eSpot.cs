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
    class eSpot
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
        public void TestForEspotNews()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/the-group/press-office/e-spot/views-news/Pages/Forms/AllItems.aspx");

            ClickFilesOnRibbon();
            
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id*='Ribbon.Documents.New.NewDocument']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id*='Ribbon.Documents.New.NewDocument']")));
            driver.FindElement(By.CssSelector("[id*='Ribbon.Documents.New.NewDocument']"));


            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")));
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")).Click();     //Click New Document

            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("ctl00$PlaceHolderMain$pageTitleSection$ctl01$titleTextBox")));
            driver.FindElement(By.Name("ctl00$PlaceHolderMain$pageTitleSection$ctl01$titleTextBox")).SendKeys("TestSelenium12");
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
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/el");
            var retail = driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_24ad1d81_af05_410e_95e6_34e91ebb74b2']/div/div/div[1]/div"));
            List<IWebElement> links = retail.FindElements(By.CssSelector("a")).ToList();
            Assert.AreEqual(9, links.Count);
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/the-group/press-office/e-spot/views-news/Pages/Forms/AllItems.aspx");

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            driver.FindElement(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")).Click();  //Choose the last element of the list

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.Manage.Delete-Small']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.Manage.Delete-Small']")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.Manage.Delete-Small']")).Click();  //Choose Delete

            driver.SwitchTo().Alert().Accept();
    
            
            Thread.Sleep(2000);
        }

        [Test]    
        public void TestForEspotNewsEN()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/the-group/press-office/e-spot/views-news/Pages/Forms/AllItems.aspx");

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[id*='Ribbon.Documents.New.NewDocument']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id*='Ribbon.Documents.New.NewDocument']")));
            driver.FindElement(By.CssSelector("[id*='Ribbon.Documents.New.NewDocument']"));


            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")));
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")).Click();     //Click New Document

            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("ctl00$PlaceHolderMain$pageTitleSection$ctl01$titleTextBox")));
            driver.FindElement(By.Name("ctl00$PlaceHolderMain$pageTitleSection$ctl01$titleTextBox")).SendKeys("TestSelenium12");
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
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/en");
            var retail = driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_24ad1d81_af05_410e_95e6_34e91ebb74b2']/div/div/div[1]/div"));
            List<IWebElement> links = retail.FindElements(By.CssSelector("a")).ToList();
            Assert.AreEqual(3, links.Count);
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/the-group/press-office/e-spot/views-news/Pages/Forms/AllItems.aspx");

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")));
            driver.FindElement(By.XPath("//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]")).Click();  //Choose the last element of the list

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.Manage.Delete-Small']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.Manage.Delete-Small']")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.Manage.Delete-Small']")).Click();  //Choose Delete

            driver.SwitchTo().Alert().Accept();


            Thread.Sleep(2000);
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
