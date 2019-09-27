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
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/Pages/Contact.aspx");  //go to contact form

            //Fill and submit form
            driver.FindElement(By.CssSelector("[id$='txtFullName']")).SendKeys("TestName");
            driver.FindElement(By.CssSelector("[id$='txtContactEmail']")).SendKeys("test@email.com");
            driver.FindElement(By.CssSelector("[id$='rbPartener_0']")).Click();
            IWebElement ls = driver.FindElement(By.CssSelector("[id$='FormPanel']"));
            ls.FindElement(By.XPath("./div/div[2]/div[6]/div/div/div[1]/input")).Click();
            ls.FindElement(By.XPath("./div/div[2]/div[6]/div/div/div[1]/ul/li[2]")).Click();
            driver.FindElement(By.CssSelector("[id$='ContactByEmail']")).Click();
            driver.FindElement(By.CssSelector("[id$='btnSubmit']")).Click();

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/Lists/ContactForms/AllItems.aspx");  //go to contact list

            driver.FindElement(By.XPath("//*[@class='ms-listviewtable']/tbody/tr[last()]")).Click();  //check the last item submited
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.ListItem-title']/a")).Click();               //click Items on Ribbon
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.ListItem.Manage.ViewProperties']")));
            driver.FindElement(By.CssSelector("[id^='Ribbon.ListItem.Manage.ViewProperties']")).Click();         //click View Properties

            //check all the fields to have the correct data
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='ms-formtable']/tbody/tr[1]/td[2]")));
            ls = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[1]/td[2]"));
            Assert.AreEqual(ls.Text, "TestName");

            ls = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[7]/td[2]"));
            Assert.AreEqual(ls.Text, "test@email.com");

            ls = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[8]/td[2]"));
            Assert.AreEqual(ls.Text, "Yes");

            ls = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[9]/td[2]"));
            Assert.AreEqual(ls.Text, "Καταθέσεις");

            ls = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[11]/td[2]"));
            Assert.AreEqual(ls.Text, "Μέσω Email");

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/Lists/ContactForms/AllItems.aspx");  //go to the list

            driver.FindElement(By.XPath("//*[@class='ms-listviewtable']/tbody/tr[last()]")).Click();  //check the last element
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            driver.FindElement(By.XPath("//*[@id='Ribbon.ListItem-title']/a")).Click();     //click Items on Ribbon
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.ListItem.Manage.Delete']")));
            driver.FindElement(By.CssSelector("[id^='Ribbon.ListItem.Manage.Delete']")).Click();   //click to delete
            driver.SwitchTo().Alert().Accept();  //on the alert window choose yes
            Thread.Sleep(1000);                  //wait until everything is done to shut down
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
