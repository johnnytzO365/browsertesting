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
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTutorial
{
    class Banner
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
        string UrlForBanner = "http://spsetup:p@ssw0rd@vm-sp2013/greek/Banners/Forms/AllItems.aspx?InitialTabId=Ribbon.Document&VisibilityContext=WSSTabPersistence";
        string ButtonForNewPage = "//*[@id='Ribbon.Documents.New.NewDocument-Large']/a[2]";
        string ButtonSlider = "//*[@id='Ribbon.Document.All.NewDocument.Menu.ContentTypes.1-Menu32']";
        string ImgInputFieldClickButt = "//*[@id='ctl00_PlaceHolderMain_UploadDocumentSection_ctl05_InputFile']";
        string TitleInputField = "//*[@id='Title_fa564e0f-0c70-4ab9-b863-0177e6ddd247_$TextField']";
        string FirstItemOnList = "//*[@id='onetidDoclibViewTbl0']/tbody/tr[1]/td[1]";
        string CheckOutButton = "//*[@id='Ribbon.Documents.Workflow.Publish-Large']";
        string PublishButton = "CheckinOk";
        string ApproveRadioButton = "ctl00_PlaceHolderMain_approveDescription_ctl01_RadioBtnApprovalStatus_0";
        string ApproveButton = "//*[@id='Ribbon.Documents.Workflow.Moderate-Medium']";
        string PopUpMenu = "ms-dlgFrame";
        string DeleteButton = "//*[@id='Ribbon.Documents.Manage.Delete-Medium']";

        [Test]
        public void AddBannerEl() 
        {
           
            driver.Navigate().GoToUrl(UrlForBanner);
            driver.Manage().Window.Maximize();
            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(ButtonForNewPage)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(ButtonForNewPage)));
            driver.FindElement(By.XPath(ButtonForNewPage)).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(ButtonSlider)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(ButtonSlider)));
            Thread.Sleep(1000);
            driver.FindElement(By.XPath(ButtonSlider)).Click();
            
            IWebDriver dialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName(PopUpMenu)));
            WebDriverWait dialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            dialogWait.Until(ExpectedConditions.ElementIsVisible(By.XPath(ImgInputFieldClickButt)));
            dialogWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(ImgInputFieldClickButt)));
            IWebElement uploadElement = dialogDriver.FindElement(By.Id("ctl00_PlaceHolderMain_UploadDocumentSection_ctl05_InputFile"));
            uploadElement.SendKeys(@"C:\Users\spsetup\Pictures\plateforme-travail-collaboratif-securise.jpg");
            dialogDriver.FindElement(By.Id("ctl00_PlaceHolderMain_ctl03_RptControls_btnOK")).Click();//click ok for upload

            Thread.Sleep(1000);
            IWebElement slideElementChoose = driver.FindElement(By.XPath("//*[@id='ctl00_ctl41_g_f280e4d1_4e87_4625_add5_1a09b7d5f83a_ctl00_ctl02_ctl00_ctl01_ctl00_ContentTypeChoice']"));//choose slide
            slideElementChoose.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='ctl00_ctl41_g_f280e4d1_4e87_4625_add5_1a09b7d5f83a_ctl00_ctl02_ctl00_ctl01_ctl00_ContentTypeChoice']/option[2]")));//choose ete
            IWebElement nbgSlide= driver.FindElement(By.XPath("//*[@id='ctl00_ctl41_g_f280e4d1_4e87_4625_add5_1a09b7d5f83a_ctl00_ctl02_ctl00_ctl01_ctl00_ContentTypeChoice']/option[2]"));
            nbgSlide.Click();
            IWebElement titleElement = driver.FindElement(By.XPath(TitleInputField));
            titleElement.SendKeys("zTestTitle");
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='ShowLearnMore_b09d5dab-b552-4e29-b6cb-a888479b0d80_$BooleanField']")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='ShowLearnMore_b09d5dab-b552-4e29-b6cb-a888479b0d80_$BooleanField']")));
            IWebElement moreElement = driver.FindElement(By.XPath("//*[@id='ShowLearnMore_b09d5dab-b552-4e29-b6cb-a888479b0d80_$BooleanField']"));
            moreElement.Click();
            IWebElement sliderText = driver.FindElement(By.XPath("//*[@id='SliderTabTitle_e3884de0-9459-4937-baee-751b2af4bdc8_$TextField']"));
            sliderText.SendKeys("zTestTitle");
            IWebElement sliderPlayTime = driver.FindElement(By.XPath("//*[@id='SliderAutoplayDuration_6e202718-aa6e-41c2-8d38-3f0643d2eee7_$NumberField']"));
            sliderPlayTime.SendKeys("5");
            IWebElement pageLookUp = driver.FindElement(By.XPath("//*[@id='PageLookup_b193cfc6-ea26-4a22-a60a-14aabd4c7940_$LookupField']"));
            pageLookUp.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='PageLookup_b193cfc6-ea26-4a22-a60a-14aabd4c7940_$LookupField']/option[4]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='PageLookup_b193cfc6-ea26-4a22-a60a-14aabd4c7940_$LookupField']/option[4]")));
            IWebElement chooseElement = driver.FindElement(By.XPath("//*[@id='PageLookup_b193cfc6-ea26-4a22-a60a-14aabd4c7940_$LookupField']/option[4]"));
            chooseElement.Click();

            IWebElement saveButton = driver.FindElement(By.XPath("//*[@id='ctl00_ctl41_g_f280e4d1_4e87_4625_add5_1a09b7d5f83a_ctl00_ctl02_ctl00_toolBarTbl_RightRptControls_ctl00_ctl00_diidIOSaveItem']"));
            saveButton.Click();

            ChooseItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(CheckOutButton)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(CheckOutButton)));
            driver.FindElement(By.XPath(CheckOutButton)).Click();  //Choose Check in
            Thread.Sleep(200);

            IWebDriver dialogDriver2 = driver.SwitchTo().Frame(driver.FindElement(By.ClassName(PopUpMenu)));
            WebDriverWait dialogWait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            dialogWait2.Until(ExpectedConditions.ElementIsVisible(By.Id(PublishButton)));
            dialogWait2.Until(ExpectedConditions.ElementToBeClickable(By.Id(PublishButton)));
            dialogDriver2.FindElement(By.Id(PublishButton)).Click();  //Click Major Version

            ChooseItem();

            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(ApproveButton)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(ApproveButton)));
            driver.FindElement(By.XPath(ApproveButton)).Click();  //Choose Approve-Reject

            IWebDriver newDialogDriver = driver.SwitchTo().Frame(driver.FindElement(By.ClassName(PopUpMenu)));
            WebDriverWait newDialogWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            newDialogWait.Until(ExpectedConditions.ElementIsVisible(By.Id(ApproveRadioButton)));
            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id(ApproveRadioButton)));
            newDialogDriver.FindElement(By.Id(ApproveRadioButton)).Click();  //Click Approve

            newDialogWait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ctl00_PlaceHolderMain_ctl00_RptControls_BtnSubmit")));
            newDialogDriver.FindElement(By.Id("ctl00_PlaceHolderMain_ctl00_RptControls_BtnSubmit")).Click();  //Click OK

            driver.SwitchTo().Alert().Accept();
            driver.Navigate().GoToUrl(HomeUrlEl);
            Thread.Sleep(2000);

            driver.Navigate().GoToUrl(UrlForBanner);
            ChooseItem();
            ClickFilesOnRibbon();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(DeleteButton)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(DeleteButton)));
            driver.FindElement(By.XPath(DeleteButton)).Click();

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
        public void ChooseItem()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(FirstItemOnList)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(FirstItemOnList)));
            driver.FindElement(By.XPath(FirstItemOnList)).Click();  //Choose the last element of the list
        }
    }
}
