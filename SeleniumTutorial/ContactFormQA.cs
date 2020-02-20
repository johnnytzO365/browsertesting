using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Configuration;
using System.Threading;

namespace SeleniumTutorial
{
    class ContactFormQA
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("enable-automation");
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"],options);
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }

        [Test]
        public void CheckFormSubmitEl()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["QAServerName"]) + "el/contact/contact-form");  //go to contact form

            //Fill and submit form
            try
            {
                string nameSelector = "[id$='txtFullName']";
                IWebElement name = driver.FindElement(By.CssSelector(nameSelector));
                name.SendKeys("TestName");
            }
            catch
            {
                Assert.Fail("Couldn't find Name Field!");
            }
            try
            {
                string emailSelector = "[id$='txtContactEmail']";
                IWebElement email = driver.FindElement(By.CssSelector(emailSelector));
                email.SendKeys("test@email.com");
            }
            catch
            {
                Assert.Fail("Couldn't find Email Field!");
            }
            try
            {
                string partenerSelector = "[id$='rbPartener_0']";
                IWebElement partener = driver.FindElement(By.CssSelector(partenerSelector));
                partener.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Partener Radio Button!");
            }
            IWebElement arrow = null;
            try
            {
                string arrowSelector = "minict_wrapper";
                arrow = driver.FindElement(By.ClassName(arrowSelector));
                arrow.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find arrow next to Choice!");
            }
            try
            {
                string choiceSelector = "./ul/li[2]";
                IWebElement choice = arrow.FindElement(By.XPath(choiceSelector));
                choice.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find your choice in the dropdown menu!");
            }
            try
            {
                string contactBySelector = "[id$='ContactByEmail']";
                IWebElement contactBy = driver.FindElement(By.CssSelector(contactBySelector));
                contactBy.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find contact by email radio button!");
            }
            try
            {
                string submitSelector = "[id$='btnSubmit']";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(submitSelector)));
                IWebElement submit = driver.FindElement(By.CssSelector(submitSelector));
                submit.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find submit button!");
            }
            Thread.Sleep(3000);
            driver.Quit();
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["authQAServerName"]) + "Lists/ContactForms/AllItems.aspx");  //go to contact list
            try
            {
                string sortCreatedSelector = "diidSort2Created";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(sortCreatedSelector)));
                IWebElement sortCreated = driver.FindElement(By.Id(sortCreatedSelector));
                sortCreated.Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(sortCreatedSelector)));
                sortCreated.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find sort button!");
            }
            try
            {
                string firstItemSelector = "//*[@class='ms-listviewtable']/tbody/tr";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(firstItemSelector)));
                IWebElement firstItem = driver.FindElement(By.XPath(firstItemSelector));
                firstItem.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find last item on list!");
            }
            try
            {
                string itemsOnRibbonSelector = "//*[@id='Ribbon.ListItem-title']/a";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(itemsOnRibbonSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(itemsOnRibbonSelector)));
                IWebElement itemsOnRibbon = driver.FindElement(By.XPath(itemsOnRibbonSelector));
                itemsOnRibbon.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Items button on Ribbon!");
            }
            try
            {
                string viewProperiesSelector = "[id^='Ribbon.ListItem.Manage.ViewProperties']";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(viewProperiesSelector)));
                IWebElement viewProperties = driver.FindElement(By.CssSelector(viewProperiesSelector));
                viewProperties.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find View Properties button on Ribbon!");
            }

            //check all the fields to have the correct data
            try
            {
                string nameSelector = "//*[@class='ms-formtable']/tbody/tr[1]/td[2]";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(nameSelector)));
                IWebElement name = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[1]/td[2]"));
                Assert.AreEqual(name.Text, "TestName");

                string emailSelector = "//*[@class='ms-formtable']/tbody/tr[7]/td[2]";
                IWebElement email = driver.FindElement(By.XPath(emailSelector));
                Assert.AreEqual(email.Text, "test@email.com");

                string partenerSelector = "//*[@class='ms-formtable']/tbody/tr[8]/td[2]";
                IWebElement partener = driver.FindElement(By.XPath(partenerSelector));
                Assert.AreEqual(partener.Text, "Yes");

                string choiceSelector = "//*[@class='ms-formtable']/tbody/tr[9]/td[2]";
                IWebElement choice = driver.FindElement(By.XPath(choiceSelector));
                Assert.AreEqual(choice.Text, "Καταθέσεις");

                string contactBySelector = "//*[@class='ms-formtable']/tbody/tr[11]/td[2]";
                IWebElement contactBy = driver.FindElement(By.XPath(contactBySelector));
                Assert.AreEqual(contactBy.Text, "Μέσω Email");
            }
            catch
            {
                Assert.Fail("Something doesn't match!");
            }
        }

        [Test]
        public void CheckFormSubmitEn()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["QAServerName"]) + "en/contact/contact-form");  //go to contact form

            //Fill and submit form
            try
            {
                string nameSelector = "[id$='txtFullName']";
                IWebElement name = driver.FindElement(By.CssSelector(nameSelector));
                name.SendKeys("TestName");
            }
            catch
            {
                Assert.Fail("Couldn't find Name Field!");
            }
            try
            {
                string emailSelector = "[id$='txtContactEmail']";
                IWebElement email = driver.FindElement(By.CssSelector(emailSelector));
                email.SendKeys("test@email.com");
            }
            catch
            {
                Assert.Fail("Couldn't find Email Field!");
            }
            try
            {
                string partenerSelector = "[id$='rbPartener_0']";
                IWebElement partener = driver.FindElement(By.CssSelector(partenerSelector));
                partener.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Partener Radio Button!");
            }
            IWebElement arrow = null;
            try
            {
                string arrowSelector = "minict_wrapper";
                arrow = driver.FindElement(By.ClassName(arrowSelector));
                arrow.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find arrow next to Choice!");
            }
            try
            {
                string choiceSelector = "./ul/li[2]";
                IWebElement choice = arrow.FindElement(By.XPath(choiceSelector));
                choice.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find your choice in the dropdown menu!");
            }
            try
            {
                string contactBySelector = "[id$='ContactByEmail']";
                IWebElement contactBy = driver.FindElement(By.CssSelector(contactBySelector));
                contactBy.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find contact by email radio button!");
            }
            try
            {
                string submitSelector = "[id$='btnSubmit']";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(submitSelector)));
                IWebElement submit = driver.FindElement(By.CssSelector(submitSelector));
                submit.Click();
            }
            catch
            {
                //Assert.Fail("Couldn't find submit button!");
            }
            driver.Quit();
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["authQAServerName"]) + "Lists/ContactForms/AllItems.aspx");  //go to contact list
            try
            {
                string sortCreatedSelector = "diidSort2Created";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(sortCreatedSelector)));
                IWebElement sortCreated = driver.FindElement(By.Id(sortCreatedSelector));
                sortCreated.Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(sortCreatedSelector)));
                sortCreated.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find sort button!");
            }
            try
            {
                string firstItemSelector = "//*[@class='ms-listviewtable']/tbody/tr";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(firstItemSelector)));
                IWebElement firstItem = driver.FindElement(By.XPath(firstItemSelector));
                firstItem.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find last item on list!");
            }
            try
            {
                string itemsOnRibbonSelector = "//*[@id='Ribbon.ListItem-title']/a";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(itemsOnRibbonSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(itemsOnRibbonSelector)));
                IWebElement itemsOnRibbon = driver.FindElement(By.XPath(itemsOnRibbonSelector));
                itemsOnRibbon.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Items button on Ribbon!");
            }
            try
            {
                string viewProperiesSelector = "[id^='Ribbon.ListItem.Manage.ViewProperties']";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(viewProperiesSelector)));
                IWebElement viewProperties = driver.FindElement(By.CssSelector(viewProperiesSelector));
                viewProperties.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find View Properties button on Ribbon!");
            }

            //check all the fields to have the correct data
            try
            {
                string nameSelector = "//*[@class='ms-formtable']/tbody/tr[1]/td[2]";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(nameSelector)));
                IWebElement name = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[1]/td[2]"));
                Assert.AreEqual(name.Text, "TestName");

                string emailSelector = "//*[@class='ms-formtable']/tbody/tr[7]/td[2]";
                IWebElement email = driver.FindElement(By.XPath(emailSelector));
                Assert.AreEqual(email.Text, "test@email.com");

                string partenerSelector = "//*[@class='ms-formtable']/tbody/tr[8]/td[2]";
                IWebElement partener = driver.FindElement(By.XPath(partenerSelector));
                Assert.AreEqual(partener.Text, "Yes");

                string choiceSelector = "//*[@class='ms-formtable']/tbody/tr[9]/td[2]";
                IWebElement choice = driver.FindElement(By.XPath(choiceSelector));
                Assert.AreEqual(choice.Text, "Καταθέσεις");

                string contactBySelector = "//*[@class='ms-formtable']/tbody/tr[11]/td[2]";
                IWebElement contactBy = driver.FindElement(By.XPath(contactBySelector));
                Assert.AreEqual(contactBy.Text, "Μέσω Email");
            }
            catch
            {
                Assert.Fail("Something doesn't match!");
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
