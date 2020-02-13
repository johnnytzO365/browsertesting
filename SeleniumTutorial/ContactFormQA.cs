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
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
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
                string recaptchaSelector = "recaptcha-checkbox-border";
                IWebElement recaptcha = driver.FindElement(By.ClassName(recaptchaSelector));
                recaptcha.Click();
                Thread.Sleep(1000);
            }
            catch
            {
                Assert.Fail("Couldn't find recaptcha checkbox!");
            }
            try
            {
                string submitSelector = "[id$='btnSubmit']";
                IWebElement submit = driver.FindElement(By.CssSelector(submitSelector));
                submit.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Submit Button!");
            }
            /*
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]) + "Lists/ContactForms/AllItems.aspx");  //go to contact list

            try
            {
                String lastItemSelector = "//*[@class='ms-listviewtable']/tbody/tr[last()]";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(lastItemSelector)));
                IWebElement lastItem = driver.FindElement(By.XPath(lastItemSelector));
                lastItem.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find last item on list!");
            }
            try
            {
                String itemsOnRibbonSelector = "//*[@id='Ribbon.ListItem-title']/a";
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
                String viewProperiesSelector = "[id^='Ribbon.ListItem.Manage.ViewProperties']";
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
                String nameSelector = "//*[@class='ms-formtable']/tbody/tr[1]/td[2]";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(nameSelector)));
                IWebElement name = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[1]/td[2]"));
                Assert.AreEqual(name.Text, "TestName");

                String emailSelector = "//*[@class='ms-formtable']/tbody/tr[7]/td[2]";
                IWebElement email = driver.FindElement(By.XPath(emailSelector));
                Assert.AreEqual(email.Text, "test@email.com");

                String partenerSelector = "//*[@class='ms-formtable']/tbody/tr[8]/td[2]";
                IWebElement partener = driver.FindElement(By.XPath(partenerSelector));
                Assert.AreEqual(partener.Text, "Yes");

                String choiceSelector = "//*[@class='ms-formtable']/tbody/tr[9]/td[2]";
                IWebElement choice = driver.FindElement(By.XPath(choiceSelector));
                Assert.AreEqual(choice.Text, "Καταθέσεις");

                String contactBySelector = "//*[@class='ms-formtable']/tbody/tr[11]/td[2]";
                IWebElement contactBy = driver.FindElement(By.XPath(contactBySelector));
                Assert.AreEqual(contactBy.Text, "Μέσω Email");
            }
            catch
            {
                Assert.Fail("Something doesn't match!");
            }

            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]) + "/Lists/ContactForms/AllItems.aspx");  //go to the list

            try
            {
                String lastItemSelector = "//*[@class='ms-listviewtable']/tbody/tr[last()]";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(lastItemSelector)));
                IWebElement lastItem = driver.FindElement(By.XPath(lastItemSelector));
                lastItem.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find last item on list!");
            }
            try
            {
                String itemsOnRibbonSelector = "//*[@id='Ribbon.ListItem-title']/a";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(itemsOnRibbonSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(itemsOnRibbonSelector)));
                IWebElement itemsOnRibbon = driver.FindElement(By.XPath(itemsOnRibbonSelector));
                itemsOnRibbon.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Items on Ribbon!");
            }
            try
            {
                String deleteSelector = "[id^='Ribbon.ListItem.Manage.Delete']";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(deleteSelector)));
                IWebElement delete = driver.FindElement(By.CssSelector(deleteSelector));
                delete.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find delete button on ribbon!");
            }

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);*/
        }

        [Test]
        public void CheckFormSubmitEn()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]) + "english/Pages/Contact.aspx");  //go to contact form

            //Fill and submit form
            try
            {
                String nameSelector = "[id$='txtFullName']";
                IWebElement name = driver.FindElement(By.CssSelector(nameSelector));
                name.SendKeys("TestName");
            }
            catch
            {
                Assert.Fail("Couldn't find Name Field!");
            }
            try
            {
                String emailSelector = "[id$='txtContactEmail']";
                IWebElement email = driver.FindElement(By.CssSelector(emailSelector));
                email.SendKeys("test@email.com");
            }
            catch
            {
                Assert.Fail("Couldn't find Email Field!");
            }
            try
            {
                String partenerSelector = "[id$='rbPartener_0']";
                IWebElement partener = driver.FindElement(By.CssSelector(partenerSelector));
                partener.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Partener Radio Button!");
            }
            IWebElement temp = null;
            try
            {
                String tempSelector = "[id$='FormPanel']";
                temp = driver.FindElement(By.CssSelector(tempSelector));
            }
            catch
            {
                Assert.Fail("Couldn't find FormPanel element!");
            }
            try
            {
                String arrowSelector = "./div/div[2]/div[6]/div/div/div[1]/input";
                IWebElement arrow = temp.FindElement(By.XPath(arrowSelector));
                arrow.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find arrow next to Choice!");
            }
            try
            {
                String choiceSelector = "./div/div[2]/div[6]/div/div/div[1]/ul/li[2]";
                IWebElement choice = temp.FindElement(By.XPath(choiceSelector));
                choice.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find your choice in the dropdown menu!");
            }
            try
            {
                String contactBySelector = "[id$='ContactByEmail']";
                IWebElement contactBy = driver.FindElement(By.CssSelector(contactBySelector));
                contactBy.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find contact by email radio button!");
            }
            try
            {
                String submitSelector = "[id$='btnSubmit']";
                IWebElement submit = driver.FindElement(By.CssSelector(submitSelector));
                submit.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Submit Button!");
            }

            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]) + "Lists/ContactForms/AllItems.aspx");  //go to contact list

            try
            {
                String lastItemSelector = "//*[@class='ms-listviewtable']/tbody/tr[last()]";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(lastItemSelector)));
                IWebElement lastItem = driver.FindElement(By.XPath(lastItemSelector));
                lastItem.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find last item on list!");
            }
            try
            {
                String itemsOnRibbonSelector = "//*[@id='Ribbon.ListItem-title']/a";
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
                String viewProperiesSelector = "[id^='Ribbon.ListItem.Manage.ViewProperties']";
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
                String nameSelector = "//*[@class='ms-formtable']/tbody/tr[1]/td[2]";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(nameSelector)));
                IWebElement name = driver.FindElement(By.XPath("//*[@class='ms-formtable']/tbody/tr[1]/td[2]"));
                Assert.AreEqual(name.Text, "TestName");

                String emailSelector = "//*[@class='ms-formtable']/tbody/tr[7]/td[2]";
                IWebElement email = driver.FindElement(By.XPath(emailSelector));
                Assert.AreEqual(email.Text, "test@email.com");

                String partenerSelector = "//*[@class='ms-formtable']/tbody/tr[8]/td[2]";
                IWebElement partener = driver.FindElement(By.XPath(partenerSelector));
                Assert.AreEqual(partener.Text, "Yes");

                String choiceSelector = "//*[@class='ms-formtable']/tbody/tr[9]/td[2]";
                IWebElement choice = driver.FindElement(By.XPath(choiceSelector));
                Assert.AreEqual(choice.Text, "Deposits");

                String contactBySelector = "//*[@class='ms-formtable']/tbody/tr[11]/td[2]";
                IWebElement contactBy = driver.FindElement(By.XPath(contactBySelector));
                Assert.AreEqual(contactBy.Text, "By e-mail");
            }
            catch
            {
                Assert.Fail("Something doesn't match!");
            }

            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["ServerName"]) + "/Lists/ContactForms/AllItems.aspx");  //go to the list

            try
            {
                String lastItemSelector = "//*[@class='ms-listviewtable']/tbody/tr[last()]";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(lastItemSelector)));
                IWebElement lastItem = driver.FindElement(By.XPath(lastItemSelector));
                lastItem.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find last item on list!");
            }
            try
            {
                String itemsOnRibbonSelector = "//*[@id='Ribbon.ListItem-title']/a";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(itemsOnRibbonSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(itemsOnRibbonSelector)));
                IWebElement itemsOnRibbon = driver.FindElement(By.XPath(itemsOnRibbonSelector));
                itemsOnRibbon.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Items on Ribbon!");
            }
            try
            {
                String deleteSelector = "[id^='Ribbon.ListItem.Manage.Delete']";
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(deleteSelector)));
                IWebElement delete = driver.FindElement(By.CssSelector(deleteSelector));
                delete.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find delete button on ribbon!");
            }

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);
        }
    }
}
