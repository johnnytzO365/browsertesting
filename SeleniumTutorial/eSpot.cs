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
        string HomeUrlEl = "http://spsetup:p@ssw0rd@vm-sp2013/el";
        string HomeUrlEn = "http://spsetup:p@ssw0rd@vm-sp2013/en";
        string UrlForEspot = "http://spsetup:p@ssw0rd@vm-sp2013/greek/the-group/press-office/e-spot/views-news/Pages/Forms/AllItems.aspx";
        string UrlForEspotEn = "http://spsetup:p@ssw0rd@vm-sp2013/english/the-group/press-office/e-spot/views-news/Pages/Forms/AllItems.aspx";
        string ButtonForNewPage = "[id*='Ribbon.Documents.New.NewDocument']";
        string TitleOfNewItemPlaceHolder = "ctl00$PlaceHolderMain$pageTitleSection$ctl01$titleTextBox";
        string LastItemOnList = "//*[@id='onetidDoclibViewTbl0']/tbody/tr[last()]/td[1]";
        string CheckOutButton = "//*[@id='Ribbon.Documents.EditCheckout.CheckIn-Medium']";
        string PublishButton = "ActionCheckinPublish";
        string ApproveRadioButton = "ctl00_PlaceHolderMain_approveDescription_ctl01_RadioBtnApprovalStatus_0";
        string ApproveButton = "//*[@id='Ribbon.Documents.Workflow.Moderate-Medium']";
        string EspotWebPart = "//*[@id='ctl00_SPWebPartManager1_g_24ad1d81_af05_410e_95e6_34e91ebb74b2']/div/div/div[1]/div";
        string DeleteButton = "//*[@id='Ribbon.Documents.Manage.Delete-Medium']";

        [Test]
        public void TestForEspotNews()
        {
            driver.Navigate().GoToUrl(UrlForEspot);
            driver.Manage().Window.Maximize();
            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(ButtonForNewPage)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(ButtonForNewPage)));
            driver.FindElement(By.CssSelector(ButtonForNewPage));


            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")));
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")).Click();     //Click New Document

            wait.Until(ExpectedConditions.ElementIsVisible(By.Name(TitleOfNewItemPlaceHolder)));
            driver.FindElement(By.Name(TitleOfNewItemPlaceHolder)).SendKeys("TestSelenium12");
            driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_ctl00_RptControls_buttonCreatePage']")).Click();    //Click Create

            ChooseLastItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(CheckOutButton)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(CheckOutButton)));
            driver.FindElement(By.XPath(CheckOutButton)).Click();  //Choose Check in

            IWebDriver dialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait dialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            dialogWait.Until(ExpectedConditions.ElementIsVisible(By.Id(PublishButton)));
            dialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id(PublishButton)));
            dialogDriver.FindElement(By.Id(PublishButton)).Click();  //Click Major Version

            dialogWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='CheckinOk']")));
            dialogDriver.FindElement(By.XPath("//*[@id='CheckinOk']")).Click();  //Click OK

            ChooseLastItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(ApproveButton)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(ApproveButton)));
            driver.FindElement(By.XPath(ApproveButton)).Click();  //Choose Approve-Reject

            IWebDriver newDialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait newDialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            newDialogWait.Until(ExpectedConditions.ElementIsVisible(By.Id(ApproveRadioButton)));
            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id(ApproveRadioButton)));
            newDialogDriver.FindElement(By.Id(ApproveRadioButton)).Click();  //Click Approve

            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ctl00_PlaceHolderMain_ctl00_RptControls_BtnSubmit")));
            newDialogDriver.FindElement(By.Id("ctl00_PlaceHolderMain_ctl00_RptControls_BtnSubmit")).Click();  //Click OK

            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl(HomeUrlEl);
            var retail = driver.FindElement(By.XPath(EspotWebPart));
            List<IWebElement> links = retail.FindElements(By.CssSelector("a")).ToList();
            Assert.AreEqual(6, links.Count);
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl(UrlForEspot);

            ChooseLastItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(DeleteButton)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(DeleteButton)));
            driver.FindElement(By.XPath(DeleteButton)).Click();  //Choose Delete

            driver.SwitchTo().Alert().Accept();
    
            
            Thread.Sleep(2000);
        }

        [Test]    
        public void TestForEspotNewsEN()
        {
            
            driver.Navigate().GoToUrl(UrlForEspotEn);
            driver.Manage().Window.Maximize();
            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(ButtonForNewPage)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(ButtonForNewPage)));
            driver.FindElement(By.CssSelector(ButtonForNewPage));


            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")));
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[1]/span")).Click();     //Click New Document

            wait.Until(ExpectedConditions.ElementIsVisible(By.Name(TitleOfNewItemPlaceHolder)));
            driver.FindElement(By.Name(TitleOfNewItemPlaceHolder)).SendKeys("TestSelenium12");
            driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_ctl00_RptControls_buttonCreatePage']")).Click();    //Click Create

            ChooseLastItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(CheckOutButton)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(CheckOutButton)));
            driver.FindElement(By.XPath(CheckOutButton)).Click();  //Choose Check in

            IWebDriver dialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait dialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            dialogWait.Until(ExpectedConditions.ElementIsVisible(By.Id(PublishButton)));
            dialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id(PublishButton)));
            dialogDriver.FindElement(By.Id(PublishButton)).Click();  //Click Major Version

            dialogWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='CheckinOk']")));
            dialogDriver.FindElement(By.XPath("//*[@id='CheckinOk']")).Click();  //Click OK

            ChooseLastItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(ApproveButton)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(ApproveButton)));
            driver.FindElement(By.XPath(ApproveButton)).Click();  //Choose Approve-Reject

            IWebDriver newDialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
            WebDriverWait newDialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            newDialogWait.Until(ExpectedConditions.ElementIsVisible(By.Id(ApproveRadioButton)));
            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id(ApproveRadioButton)));
            newDialogDriver.FindElement(By.Id(ApproveRadioButton)).Click();  //Click Approve

            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ctl00_PlaceHolderMain_ctl00_RptControls_BtnSubmit")));
            newDialogDriver.FindElement(By.Id("ctl00_PlaceHolderMain_ctl00_RptControls_BtnSubmit")).Click();  //Click OK

            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl(HomeUrlEn);
            var retail = driver.FindElement(By.XPath(EspotWebPart));
            List<IWebElement> links = retail.FindElements(By.CssSelector("a")).ToList();
            Assert.AreEqual(3, links.Count);
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl(UrlForEspotEn);

            ChooseLastItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(DeleteButton)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(DeleteButton)));
            driver.FindElement(By.XPath(DeleteButton)).Click();  //Choose Delete

            driver.SwitchTo().Alert().Accept();


            Thread.Sleep(2000);
        }


        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        public void ChooseLastItem() {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(LastItemOnList)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(LastItemOnList)));
            driver.FindElement(By.XPath(LastItemOnList)).Click();  //Choose the last element of the list
        }

        public void ClickFilesOnRibbon()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.Document-title']/a")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.Document-title']/a")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.Document-title']/a")).Click();       //Click Files on Ribbon
        }
    }

}
