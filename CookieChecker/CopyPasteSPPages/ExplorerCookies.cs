using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.IE;
using System.Configuration;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using CopyPasteSPPages;
using System.IO;

namespace CookieChecker
{
    
    class ExplorerCookies
    {
        IWebDriver driver = new InternetExplorerDriver(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["DriverPath"]);
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }


        [Test]
        public void CookiesBarAcceptAll()
        {
            
            String line;
           StreamReader infile = new StreamReader(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["DomainsNames"]);

            Application oXL;
            Workbook oWB;
            Worksheet oSheet;
            oXL = new Application();
            oXL.Visible = true;
            oWB = oXL.Workbooks.Add("");
            oSheet = oWB.ActiveSheet;

            oSheet.Cells[1, 1] = "Domain";
            oSheet.Cells[1, 2] = "Cookies";

            int counter = 2;
            while ((line = infile.ReadLine()) != null)
            {
                int c2 = 3;
                driver.Navigate().GoToUrl(line);
                try
                {
                    wait.Until(ExpectedConditions.AlertIsPresent());
                    var alert = driver.SwitchTo().Alert();

                    string username = "bank\\" + ConfigurationManager.AppSettings["username"];
                    string password = ConfigurationManager.AppSettings["password"];
                    alert.SetAuthenticationCredentials(username,password);
                    alert.Accept();
                }
                catch
                {
                }

                try
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(By.Id("cookGRALL")));//publicSite CookiesBar
                    driver.FindElement(By.Id("cookGRALL")).Click();
                    driver.Navigate().Refresh();
                    Thread.Sleep(5000);
                }
                catch
                {
                    try
                    {
                        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ccc-notify-accept")));//wizz
                        driver.FindElement(By.Id("ccc-notify-accept")).Click();
                        driver.Navigate().Refresh();
                        Thread.Sleep(5000);
                    }
                    catch
                    {
                        try
                        {
                            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("checkbox_icon")));//developers
                            driver.FindElement(By.CssSelector("checkbox_icon")).Click();
                            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("btn electro")));
                            driver.FindElement(By.CssSelector("btn electro")).Click();
                            driver.Navigate().Refresh();
                        }
                        catch
                        {
                            try
                            {
                                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='cookie-bar']/div/div[2]/div[2]/button")));//act4Greece
                                driver.FindElement(By.XPath("//*[@id='cookie-bar']/div/div[2]/div[2]/button")).Click();
                                driver.Navigate().Refresh();
                                Thread.Sleep(5000);
                            }
                            catch
                            {

                            }
                        }
                    }

                }
                int number = driver.Manage().Cookies.AllCookies.Count;
                List<Cookie> cooks = driver.Manage().Cookies.AllCookies.ToList();
                Thread.Sleep(5000);
                oSheet.Cells[counter, 1] = line;
                oSheet.Cells[counter, 2] = number;
                foreach (Cookie cook in cooks)
                {
                    oSheet.Cells[counter, c2] = cook.Name;
                    c2++;
                }
                counter++;
                driver.Manage().Cookies.DeleteAllCookies();
               
            }

            infile.Close();

            oXL.Visible = false;
            oXL.UserControl = false;
            oXL.DisplayAlerts = false;
            oXL.EnableEvents = false;
            oXL.DisplayAlerts = false;
            oWB.SaveAs(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["IEOutputFileAc"], Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();

            String path = Utilities.UploadToTeamSite(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["IEOutputFileAc"]);
            String result = Utilities.TestFile(ConfigurationManager.AppSettings["teamSiteUrl"]+"CookieCheckerResults/Sample/IE-CookiesAccept.xlsx", AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["IEOutputFileAc"]);
            if (!(result.Equals("OK")))
            {
                Utilities.SendEmail(path, "Explorer Cookies -> Acccept",result);
            }
        }

        [Test]
        public void CookiesBarRejectAll()
        {
            String line;
            StreamReader infile = new StreamReader(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["DomainsNames"]);

            //IWebDriver driver = new InternetExplorerDriver(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["DriverPath"]);

            Application oXL;
            Workbook oWB;
            Worksheet oSheet;
            oXL = new Application();
            oXL.Visible = true;
            oWB = (Workbook)(oXL.Workbooks.Add(""));
            oSheet = oWB.ActiveSheet;

            oSheet.Cells[1, 1] = "Domain";
            oSheet.Cells[1, 2] = "Cookies";

            int counter = 2;
            while ((line = infile.ReadLine()) != null)
            {
                int c2 = 3;
                
                driver.Navigate().GoToUrl(line);
                try
                {
                    wait.Until(ExpectedConditions.AlertIsPresent());
                    var alert = driver.SwitchTo().Alert();
                    string username = "bank\\" + ConfigurationManager.AppSettings["username"];
                    string password = ConfigurationManager.AppSettings["password"];
                    alert.SetAuthenticationCredentials(username, password);
                }
                catch
                {
                }
                try
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(By.Id("cookGRALLRej")));//publicSite CookiesBar
                    driver.FindElement(By.Id("cookGRALLRej")).Click();
                    Thread.Sleep(5000);
                }
                catch
                {
                    try
                    {
                        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ccc-notify-accept!")));//wizz
                        driver.FindElement(By.Id("ccc-notify-accept!")).Click();
                        Thread.Sleep(5000);
                    }
                    catch
                    {
                        try
                        {
                            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("checkbox_icon")));//developers
                            driver.FindElement(By.ClassName("checkbox_icon")).Click();
                            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("btn")));
                            driver.FindElement(By.CssSelector("btn")).Click();
                            Thread.Sleep(5000);
                        }
                        catch
                        {
                            try
                            {
                                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='cookie-bar']/div/div[2]/div[3]/button")));//act4Greece
                                driver.FindElement(By.XPath("//*[@id='cookie-bar']/div/div[2]/div[3]/button")).Click();
                                Thread.Sleep(5000);
                            }
                            catch
                            {

                            }
                        }
                    }

                }
                int number = driver.Manage().Cookies.AllCookies.Count;
                List<Cookie> cooks = driver.Manage().Cookies.AllCookies.ToList();
                Thread.Sleep(5000);
                oSheet.Cells[counter, 1] = line;
                oSheet.Cells[counter, 2] = number;
                foreach (Cookie cook in cooks)
                {
                    oSheet.Cells[counter, c2] = cook.Name;
                    c2++;
                }
                counter++;
                driver.Manage().Cookies.DeleteAllCookies();
            }

            infile.Close();

            oXL.Visible = false;
            oXL.UserControl = false;
            oXL.DisplayAlerts = false;
            oXL.DisplayAlerts = false;
            oWB.SaveAs(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["IEOutputFileRej"], Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();

            String path = Utilities.UploadToTeamSite(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["IEOutputFileRej"]);
            String result = Utilities.TestFile(ConfigurationManager.AppSettings["teamSiteUrl"]+"IE-CookiesRej.xlsx", AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["IEOutputFileRej"]);
            if (!(result.Equals("OK")))
            {
                Utilities.SendEmail(path, "Explorer Cookies -> Reject",result);
            }
        }

        [Test]
        public void CookiesBarDefault()
        {
            String line;
            StreamReader infile = new StreamReader(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["DomainsNames"]);

            Application oXL;
            Workbook oWB;
            Worksheet oSheet;
            oXL = new Application();
            oXL.Visible = true;
            oWB = oXL.Workbooks.Add("");
            oSheet = oWB.ActiveSheet;

            oSheet.Cells[1, 1] = "Domain";
            oSheet.Cells[1, 2] = "Cookies";

            int counter = 2;
            while ((line = infile.ReadLine()) != null)
            {
                int c2 = 3;
                driver.Navigate().GoToUrl(line);
                try
                {
                    wait.Until(ExpectedConditions.AlertIsPresent());
                    var alert = driver.SwitchTo().Alert();
                    string username = "bank\\" + ConfigurationManager.AppSettings["username"];
                    string password = ConfigurationManager.AppSettings["password"];
                    alert.SetAuthenticationCredentials(username, password);
                }
                catch
                {
                }
                int number = driver.Manage().Cookies.AllCookies.Count;
                List<Cookie> cooks = driver.Manage().Cookies.AllCookies.ToList();
                Thread.Sleep(5000);
                oSheet.Cells[counter, 1] = line;
                oSheet.Cells[counter, 2] = number;
                foreach (Cookie cook in cooks)
                {
                    oSheet.Cells[counter, c2] = cook.Name;
                    c2++;
                }
                counter++;
                driver.Manage().Cookies.DeleteAllCookies();                
            }

            infile.Close();

            oXL.Visible = false;
            oXL.UserControl = false;
            oXL.DisplayAlerts = false;
            oXL.DisplayAlerts = false;
            oWB.SaveAs(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["IEOutputFileDef"], Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();

            String path = Utilities.UploadToTeamSite(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["IEOutputFileDef"]);
            String result = Utilities.TestFile(ConfigurationManager.AppSettings["teamSiteUrl"]+"IE-CookiesDef.xlsx", AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["IEOutputFileDef"]);
            if (!(result.Equals("OK")))
            {
                Utilities.SendEmail(path, "Explorer Cookies -> Default",result);
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
