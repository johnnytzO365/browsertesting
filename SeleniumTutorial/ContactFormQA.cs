using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Configuration;
using System.Diagnostics;
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
            options.AddArguments("headless");
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"],options);
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
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
                //Assert.Fail("Couldn't find submit button!");
            }
            Thread.Sleep(3000);
            driver.Quit();

            //Check if inserted with OK values
            driver = new InternetExplorerDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["authQAServerName"]) + "Lists/ContactForms/AllItems.aspx");  //go to contact list
            Login();
            try
            {
                string sortCreatedSelector = "[id$='Created']";
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(sortCreatedSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(sortCreatedSelector)));
                IWebElement sortCreated = driver.FindElement(By.CssSelector(sortCreatedSelector));
                sortCreated.Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(sortCreatedSelector)));
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
                Assert.AreEqual(partener.Text, "Ναι");

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
            Thread.Sleep(3000);
            driver.Quit();

            //Check if item submitted with desired values
            driver = new InternetExplorerDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["authQAServerName"]) + "Lists/ContactForms/AllItems.aspx");  //go to contact list
            Login();
            try
            {
                string sortCreatedSelector = "[id$='Created']";
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(sortCreatedSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(sortCreatedSelector)));
                IWebElement sortCreated = driver.FindElement(By.CssSelector(sortCreatedSelector));
                sortCreated.Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(sortCreatedSelector)));
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
                Assert.AreEqual(partener.Text, "Ναι");

                string choiceSelector = "//*[@class='ms-formtable']/tbody/tr[9]/td[2]";
                IWebElement choice = driver.FindElement(By.XPath(choiceSelector));
                Assert.AreEqual(choice.Text, "Deposits");

                string contactBySelector = "//*[@class='ms-formtable']/tbody/tr[11]/td[2]";
                IWebElement contactBy = driver.FindElement(By.XPath(contactBySelector));
                Assert.AreEqual(contactBy.Text, "By e-mail");
            }
            catch
            {
                Assert.Fail("Something doesn't match!");
            }
        }

        [Test]
        public void CheckComplaintFormSubmitEl()
        {
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["QAServerName"]) + "el/contact/complaint-form");  //go to contact form

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
                string phoneSelector = "[id$='txtPhone']";
                IWebElement phone = driver.FindElement(By.CssSelector(phoneSelector));
                phone.SendKeys("2102101010");
            }
            catch
            {
                Assert.Fail("Couldn't find Phone Field!");
            }
            try
            {
                string partenerSelector = "[id$='rbPartener_1']";
                IWebElement partener = driver.FindElement(By.CssSelector(partenerSelector));
                partener.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Partener Radio Button!");
            }
            try
            {
                string contactSelector = "[id$='ContactInterestRadioButtonList_0']";
                IWebElement contact = driver.FindElement(By.CssSelector(contactSelector));
                contact.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find Contact Radio Button!");
            }
            try
            {
                string contactBySelector = "[id$='ContactByPhone']";
                IWebElement contactBy = driver.FindElement(By.CssSelector(contactBySelector));
                contactBy.Click();
            }
            catch
            {
                Assert.Fail("Couldn't find contact by phone radio button!");
            }
            try
            {
                string aggreementSelector = "[id$='aggreementCheckBox']";
                IWebElement aggreement = driver.FindElement(By.CssSelector(aggreementSelector));
                aggreement.Click();
            }
            catch
            {
                //Assert.Fail("Couldn't find submit button!");
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
            Thread.Sleep(3000);
            driver.Quit();

            //Check if inserted with OK values
            driver = new InternetExplorerDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            driver.Navigate().GoToUrl((ConfigurationManager.AppSettings["authQAServerName"]) + "Lists/ComplaintList/AllItems.aspx");  //go to contact list
            Login();
            try
            {
                string sortCreatedSelector = "[id$='Created'";
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(sortCreatedSelector)));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(sortCreatedSelector)));
                IWebElement sortCreated = driver.FindElement(By.CssSelector(sortCreatedSelector));
                sortCreated.Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(sortCreatedSelector)));
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

                string emailSelector = "//*[@class='ms-formtable']/tbody/tr[2]/td[2]";
                IWebElement email = driver.FindElement(By.XPath(emailSelector));
                Assert.AreEqual(email.Text, "2102101010");

                string partenerSelector = "//*[@class='ms-formtable']/tbody/tr[8]/td[2]";
                IWebElement partener = driver.FindElement(By.XPath(partenerSelector));
                Assert.AreEqual(partener.Text, "Όχι");

                string choiceSelector = "//*[@class='ms-formtable']/tbody/tr[11]/td[2]";
                IWebElement choice = driver.FindElement(By.XPath(choiceSelector));
                Assert.AreEqual(choice.Text, "Τηλεφωνικά");

                string contactBySelector = "//*[@class='ms-formtable']/tbody/tr[14]/td[2]";
                IWebElement contactBy = driver.FindElement(By.XPath(contactBySelector));
                Assert.AreEqual(contactBy.Text, "Ναι");
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

        private void Login()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']")));
                Console.WriteLine("2");
                driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']")).Click();
            }
            catch 
            {
            }
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']/option[2]")));
                driver.FindElement(By.XPath("//*[@id='ctl00_PlaceHolderMain_ClaimsLogonSelector']/option[3]")).Click();
            }
            catch
            {
            }
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.FileName = ConfigurationManager.AppSettings["ScriptPath"];
                    myProcess.Start();

                }
            }
            catch
            {
            }
        }
    }
}
