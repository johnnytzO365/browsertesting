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
    class ContactForm
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
        public void CheckFormSubmit()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/Pages/Contact.aspx");

            //Fill and submit form
            driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_6d3e91d9_a2fa_4a30_8fb8_be04f07ca98b_txtFullName']")).SendKeys("TestName");
            driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_6d3e91d9_a2fa_4a30_8fb8_be04f07ca98b_txtContactEmail']")).SendKeys("test@email.com");
            driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_6d3e91d9_a2fa_4a30_8fb8_be04f07ca98b_rbPartener_0']")).Click();
            driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_6d3e91d9_a2fa_4a30_8fb8_be04f07ca98b_FormPanel']/div/div[2]/div[6]/div/div/div[1]/input")).Click();
            driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_6d3e91d9_a2fa_4a30_8fb8_be04f07ca98b_FormPanel']/div/div[2]/div[6]/div/div/div[1]/ul/li[2]")).Click();
            driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_6d3e91d9_a2fa_4a30_8fb8_be04f07ca98b_cbContactByEmail']")).Click();
            driver.FindElement(By.XPath("//*[@id='ctl00_SPWebPartManager1_g_6d3e91d9_a2fa_4a30_8fb8_be04f07ca98b_btnSubmit']")).Click();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/Lists/ContactForms/AllItems.aspx");

            driver.FindElement(By.XPath("//*[@id='{E75820FC-1EBE-4393-AC2E-69C48B26F864}-{6F01E980-FAAC-4B6F-8945-FD1324AB8966}']/tbody/tr[last()]")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.ListItem-title']/a")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.ListItem.Manage.ViewProperties-Large']")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.ListItem.Manage.ViewProperties-Large']")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='ms-formtable']/tbody/tr[1]/td[2]")));
            IWebElement ls = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[1]/td[2]"));
            Assert.AreEqual(ls.Text, "TestName");

            ls = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[7]/td[2]"));
            Assert.AreEqual(ls.Text, "test@email.com");

            ls = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[8]/td[2]"));
            Assert.AreEqual(ls.Text, "Yes");

            ls = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[9]/td[2]"));
            Assert.AreEqual(ls.Text, "Καταθέσεις");

            ls = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[11]/td[2]"));
            Assert.AreEqual(ls.Text, "Μέσω Email");

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/Lists/ContactForms/AllItems.aspx");

            driver.FindElement(By.XPath("//*[@id='{E75820FC-1EBE-4393-AC2E-69C48B26F864}-{6F01E980-FAAC-4B6F-8945-FD1324AB8966}']/tbody/tr[last()]")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.ListItem-title']/a")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.ListItem.Manage.Delete-Medium']")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.ListItem.Manage.Delete-Medium']")).Click();
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
