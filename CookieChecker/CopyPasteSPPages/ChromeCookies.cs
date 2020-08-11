using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using Microsoft.Office.Interop.Excel;
using CopyPasteSPPages;
using System.IO;

namespace CookieChecker
{
    class ChromeCookies
    {

        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--headless");
            //options.AddArguments("--disable-gpu");
            //options.AddArguments("--no-sandbox");
            //options.AddArguments("--allow-insecure-localhost");
           // options.AddArguments("--remote-debugging-port=9222");
           // options.AddAdditionalCapability("acceptInsecureCerts", true, true);
            //System.setProperty("webdriver.chrome.silentOutput", "true");
            driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["DriverPath"]);
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
        }

        [Test]
        public void CookiesBarAcceptAll()
        {
            string line;
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
                    wait.Until(ExpectedConditions.ElementIsVisible(By.Id("cookGRALL")));//publicSite CookiesBar
                    driver.FindElement(By.Id("cookGRALL")).Click();
                    driver.Navigate().Refresh();
                }
                catch
                {
                    try
                    {
                        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ccc-notify-accept")));//wizz
                        driver.FindElement(By.Id("ccc-notify-accept")).Click();
                        driver.Navigate().Refresh();
                    }
                    catch
                    {
                        try
                        {
                            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='root']/div[2]/div/div/div[3]/div[1]/div/label/div")));//developers
                            driver.FindElement(By.XPath("//*[@id='root']/div[2]/div/div/div[3]/div[1]/div/label/div")).Click();
                            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='root']/div[2]/div/div/div[3]/div[3]/div[1]/button")));
                            driver.FindElement(By.XPath("//*[@id='root']/div[2]/div/div/div[3]/div[3]/div[1]/button")).Click();
                            driver.Navigate().Refresh();
                        }
                        catch
                        {
                            try
                            {
                                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='cookie-bar']/div/div[2]/div[2]/button")));//act4Greece
                                driver.FindElement(By.XPath("//*[@id='cookie-bar']/div/div[2]/div[2]/button")).Click();
                                driver.Navigate().Refresh();

                            }
                            catch
                            {
                                try
                                {
                                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='cookGRALL']")));
                                    driver.FindElement(By.XPath("//*[@id='cookGRALL']")).Click();
                                    driver.Navigate().Refresh();
                                }
                                catch
                                {
                                }
                            }
                        }
                    }

                }
                int number = driver.Manage().Cookies.AllCookies.Count;
                List<Cookie> cooks = driver.Manage().Cookies.AllCookies.ToList();
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
            oWB.SaveAs(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["ChromeOutputFileAc"], Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();
            String path = Utilities.UploadToTeamSite(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["ChromeOutputFileAc"]);
            String result = Utilities.TestFile(ConfigurationManager.AppSettings["teamSiteUrl"]+"CookieCheckerResults/Sample/Chrome-CookiesAccept.xlsx", AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["ChromeOutputFileAc"]);
            if (!(result.Equals("OK")))
            {
                Utilities.SendEmail(path, "Chrome Cookies -> Accept",result);
            }
        }

        [Test]
        public void CookiesBarRejectAll()
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
                    wait.Until(ExpectedConditions.ElementIsVisible(By.Id("cookGRALLRej")));//publicSite CookiesBar
                    driver.FindElement(By.Id("cookGRALLRej")).Click();
                    driver.Navigate().Refresh();
                }
                catch
                {
                    try
                    {
                        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ccc-notify-accept!")));//wizz
                        driver.FindElement(By.Id("ccc-notify-accept!")).Click();
                        driver.Navigate().Refresh();
                    }
                    catch
                    {
                        try
                        {
                            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("checkbox_icon")));//developers
                            driver.FindElement(By.ClassName("checkbox_icon")).Click();
                            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("btn")));
                            driver.FindElement(By.CssSelector("btn")).Click();
                            driver.Navigate().Refresh();
                        }
                        catch
                        {
                            try
                            {
                                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='cookie - bar']/div/div[2]/div[3]/button")));//act4Greece
                                driver.FindElement(By.XPath("//*[@id='cookie - bar']/div/div[2]/div[3]/button")).Click();
                                driver.Navigate().Refresh();
                            }
                            catch
                            {

                            }
                        }
                    }

                }
                int number = driver.Manage().Cookies.AllCookies.Count;
                List<Cookie> cooks = driver.Manage().Cookies.AllCookies.ToList();
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
            oWB.SaveAs(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["ChromeOutputFileRej"], Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();

            String path = Utilities.UploadToTeamSite(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["ChromeOutputFileRej"]);
            String result = Utilities.TestFile(ConfigurationManager.AppSettings["teamSiteUrl"]+"CookieCheckerResults/Sample/Chrome-CookiesRej.xlsx", AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["ChromeOutputFileRej"]);
            if (!(result.Equals("OK")))
            {
                Utilities.SendEmail(path, "Chrome Cookies -> Reject",result);
            }
        }

        [Test]
        public void CookiesBarDefault()
        {
            string line;
            StreamReader infile = new StreamReader(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["DomainsNames"]);

            Application oXL;
            Workbook oWB;
            Worksheet oSheet;
            oXL = new Application();
            oXL.Visible = true;
            oWB = (Workbook)(oXL.Workbooks.Add(""));
            oSheet = (Worksheet)oWB.ActiveSheet;

            oSheet.Cells[1, 1] = "Domain";
            oSheet.Cells[1, 2] = "Cookies";

            int counter = 2;
            while ((line = infile.ReadLine()) != null)
            {
                int c2 = 3;
                try
                {
                    driver.Navigate().GoToUrl(line);
                }
                catch
                {
                    oSheet.Cells[counter, 1] = line;
                    oSheet.Cells[counter, 2] = "Page Problem";
                    counter++;
                    continue;
                }
                
                int number = driver.Manage().Cookies.AllCookies.Count;
                List<Cookie> cooks = driver.Manage().Cookies.AllCookies.ToList();
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
            oWB.SaveAs(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["ChromeOutputFileDef"], Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();
            string path = Utilities.UploadToTeamSite(AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["ChromeOutputFileDef"]);
            String result = Utilities.TestFile(ConfigurationManager.AppSettings["teamSiteUrl"]+"CookieCheckerResults/Sample/Chrome-CookiesDef.xlsx", AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["ChromeOutputFileDef"]);
            if (!(result.Equals("OK")))
            {
                Utilities.SendEmail(path,"Chrome Cookies -> Default",result);
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
