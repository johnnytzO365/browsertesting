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
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }

        [Test]
        public void CheckFormSubmitEl()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/greek/Pages/Contact.aspx");  //go to contact form

            //Fill and submit form
            IWebElement name = driver.FindElement(By.CssSelector("[id$='txtFullName']"));
            if (name != null)
                name.SendKeys("TestName");
            else
                Assert.Fail("Couldn't find Name Field!");

            IWebElement email =driver.FindElement(By.CssSelector("[id$='txtContactEmail']"));
            if (email != null)
                email.SendKeys("test@email.com");
            else
                Assert.Fail("Couldn't find Email Field!");

            IWebElement partener = driver.FindElement(By.CssSelector("[id$='rbPartener_0']"));
            if (partener != null)
                partener.Click();
            else
                Assert.Fail("Couldn't find Partener Radio Button!");

            IWebElement temp = driver.FindElement(By.CssSelector("[id$='FormPanel']"));
            IWebElement choice = null;
            if (temp != null)
            {
                IWebElement arrow = temp.FindElement(By.XPath("./div/div[2]/div[6]/div/div/div[1]/input"));
                if (arrow != null)
                    arrow.Click();
                else
                    Assert.Fail("Couldn't find arrow next to Choice!");

                choice = temp.FindElement(By.XPath("./div/div[2]/div[6]/div/div/div[1]/ul/li[2]"));
                if (choice != null)
                    choice.Click();
                else
                    Assert.Fail("Couldn't find your choice in the dropdown menu!");
            }
            else
                Assert.Fail("Couldn't find what you are looking for!");

            IWebElement contactBy = driver.FindElement(By.CssSelector("[id$='ContactByEmail']"));
            if (contactBy != null)
                contactBy.Click();
            else
                Assert.Fail("Couldn't find contact by email radio button!");

            IWebElement submit = driver.FindElement(By.CssSelector("[id$='btnSubmit']"));
            if (submit != null)
                submit.Click();
            else
                Assert.Fail("Couldn't find Submit Button!");

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/Lists/ContactForms/AllItems.aspx");  //go to contact list

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@class='ms-listviewtable']/tbody/tr[last()]")));
            IWebElement lastItem = driver.FindElement(By.XPath("//*[@class='ms-listviewtable']/tbody/tr[last()]"));
            if (lastItem != null)
                lastItem.Click();
            else
                Assert.Fail("Couldn't find last item on list!");

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            IWebElement itemsOnRibbon = driver.FindElement(By.XPath("//*[@id='Ribbon.ListItem-title']/a"));
            if (itemsOnRibbon != null)
                itemsOnRibbon.Click();
            else
                Assert.Fail("Couldn't find Items button on Ribbon!");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.ListItem.Manage.ViewProperties']")));
            IWebElement viewProperties = driver.FindElement(By.CssSelector("[id^='Ribbon.ListItem.Manage.ViewProperties']"));
            if (viewProperties != null)
                viewProperties.Click();
            else
                Assert.Fail("Couldn't find View Properties button on Ribbon!");

            //check all the fields to have the correct data
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='ms-formtable']/tbody/tr[1]/td[2]")));
            name = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[1]/td[2]"));
            Assert.AreEqual(name.Text, "TestName");

            email = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[7]/td[2]"));
            Assert.AreEqual(email.Text, "test@email.com");

            partener = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[8]/td[2]"));
            Assert.AreEqual(partener.Text, "Yes");

            choice = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[9]/td[2]"));
            Assert.AreEqual(choice.Text, "Καταθέσεις");

            contactBy = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[11]/td[2]"));
            Assert.AreEqual(contactBy.Text, "Μέσω Email");

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/Lists/ContactForms/AllItems.aspx");  //go to the list

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@class='ms-listviewtable']/tbody/tr[last()]")));
            lastItem = driver.FindElement(By.XPath("//*[@class='ms-listviewtable']/tbody/tr[last()]"));
            lastItem.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            itemsOnRibbon = driver.FindElement(By.XPath("//*[@id='Ribbon.ListItem-title']/a"));
            itemsOnRibbon.Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.ListItem.Manage.Delete']")));
            IWebElement delete = driver.FindElement(By.CssSelector("[id^='Ribbon.ListItem.Manage.Delete']"));
            if (delete != null)
                delete.Click();
            else
                Assert.Fail("Couldn't find delete button on ribbon!");

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);
        }

        [Test]
        public void CheckFormSubmitEn()
        {
            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/english/Pages/Contact.aspx");  //go to contact form

            //Fill and submit form
            IWebElement name = driver.FindElement(By.CssSelector("[id$='txtFullName']"));
            if (name != null)
                name.SendKeys("TestName");
            else
                Assert.Fail("Couldn't find Name Field!");

            IWebElement email = driver.FindElement(By.CssSelector("[id$='txtContactEmail']"));
            if (email != null)
                email.SendKeys("test@email.com");
            else
                Assert.Fail("Couldn't find Email Field!");

            IWebElement partener = driver.FindElement(By.CssSelector("[id$='rbPartener_0']"));
            if (partener != null)
                partener.Click();
            else
                Assert.Fail("Couldn't find Partener Radio Button!");

            IWebElement temp = driver.FindElement(By.CssSelector("[id$='FormPanel']"));
            IWebElement choice = null;
            if (temp != null)
            {
                IWebElement arrow = temp.FindElement(By.XPath("./div/div[2]/div[6]/div/div/div[1]/input"));
                if (arrow != null)
                    arrow.Click();
                else
                    Assert.Fail("Couldn't find arrow next to Choice!");

                choice = temp.FindElement(By.XPath("./div/div[2]/div[6]/div/div/div[1]/ul/li[2]"));
                if (choice != null)
                    choice.Click();
                else
                    Assert.Fail("Couldn't find your choice in the dropdown menu!");
            }
            else
                Assert.Fail("Couldn't find what you are looking for!");

            IWebElement contactBy = driver.FindElement(By.CssSelector("[id$='ContactByEmail']"));
            if (contactBy != null)
                contactBy.Click();
            else
                Assert.Fail("Couldn't find contact by email radio button!");

            IWebElement submit = driver.FindElement(By.CssSelector("[id$='btnSubmit']"));
            if (submit != null)
                submit.Click();
            else
                Assert.Fail("Couldn't find Submit Button!");

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/Lists/ContactForms/AllItems.aspx");  //go to contact list

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@class='ms-listviewtable']/tbody/tr[last()]")));
            IWebElement lastItem = driver.FindElement(By.XPath("//*[@class='ms-listviewtable']/tbody/tr[last()]"));
            if (lastItem != null)
                lastItem.Click();
            else
                Assert.Fail("Couldn't find last item on list!");

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            IWebElement itemsOnRibbon = driver.FindElement(By.XPath("//*[@id='Ribbon.ListItem-title']/a"));
            if (itemsOnRibbon != null)
                itemsOnRibbon.Click();
            else
                Assert.Fail("Couldn't find Items button on Ribbon!");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.ListItem.Manage.ViewProperties']")));
            IWebElement viewProperties = driver.FindElement(By.CssSelector("[id^='Ribbon.ListItem.Manage.ViewProperties']"));
            if (viewProperties != null)
                viewProperties.Click();
            else
                Assert.Fail("Couldn't find View Properties button on Ribbon!");

            //check all the fields to have the correct data
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='ms-formtable']/tbody/tr[1]/td[2]")));
            name = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[1]/td[2]"));
            Assert.AreEqual(name.Text, "TestName");

            email = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[7]/td[2]"));
            Assert.AreEqual(email.Text, "test@email.com");

            partener = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[8]/td[2]"));
            Assert.AreEqual(partener.Text, "Yes");

            choice = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[9]/td[2]"));
            Assert.AreEqual(choice.Text, "Deposits");

            contactBy = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[11]/td[2]"));
            Assert.AreEqual(contactBy.Text, "By e-mail");

            driver.Navigate().GoToUrl("http://spsetup:p@ssw0rd@vm-sp2013/Lists/ContactForms/AllItems.aspx");  //go to the list

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@class='ms-listviewtable']/tbody/tr[last()]")));
            lastItem = driver.FindElement(By.XPath("//*[@class='ms-listviewtable']/tbody/tr[last()]"));
            lastItem.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='Ribbon.ListItem-title']/a")));
            itemsOnRibbon = driver.FindElement(By.XPath("//*[@id='Ribbon.ListItem-title']/a"));
            itemsOnRibbon.Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[id^='Ribbon.ListItem.Manage.Delete']")));
            IWebElement delete = driver.FindElement(By.CssSelector("[id^='Ribbon.ListItem.Manage.Delete']"));
            if (delete != null)
                delete.Click();
            else
                Assert.Fail("Couldn't find delete button on ribbon!");

            wait.Until(ExpectedConditions.AlertIsPresent());
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
