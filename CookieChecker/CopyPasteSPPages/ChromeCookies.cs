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
using System.Collections.Specialized;

namespace CookieChecker
{
    class ChromeCookies
    {
        IWebDriver driver = new ChromeDriver(ConfigurationManager.AppSettings["DriverPath"]);
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
        }

        [Test]
        public void CookiesBarAcceptAll()
        {
            String line;
            StreamReader infile = new StreamReader(ConfigurationManager.AppSettings["DomainsNames"]);

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
                    TestForNewCookies(cook);
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
            oWB.SaveAs(ConfigurationManager.AppSettings["ChromeOutputFileAc"], Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();
            String path = Utilities.UploadToTeamSite(ConfigurationManager.AppSettings["ChromeOutputFileAc"]);
            if (!(Utilities.TestFile(@"X:\Sample\Chrome-CookiesAccept.xlsx", ConfigurationManager.AppSettings["ChromeOutputFileAc"])))
            {
                Utilities.SendEmail(path, "Chrome Cookies -> Accept");
            }
        }

        [Test]
        public void CookiesBarRejectAll()
        {
            String line;
            StreamReader infile = new StreamReader(ConfigurationManager.AppSettings["DomainsNames"]);

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
                    TestForNewCookies(cook);
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
            oWB.SaveAs(ConfigurationManager.AppSettings["ChromeOutputFileRej"], Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();

            String path = Utilities.UploadToTeamSite(ConfigurationManager.AppSettings["ChromeOutputFileRej"]);
            if (!(Utilities.TestFile(@"X:\Sample\Chrome-CookiesRej.xlsx", ConfigurationManager.AppSettings["ChromeOutputFileRej"])))
            {
                Utilities.SendEmail(path, "Chrome Cookies -> Reject");
            }
        }

        [Test]
        public void CookiesBarDefault()
        {
            String line;
            StreamReader infile = new StreamReader(ConfigurationManager.AppSettings["DomainsNames"]);

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
                driver.Navigate().GoToUrl(line);

                int number = driver.Manage().Cookies.AllCookies.Count;
                List<Cookie> cooks = driver.Manage().Cookies.AllCookies.ToList();
                oSheet.Cells[counter, 1] = line;
                oSheet.Cells[counter, 2] = number;
                foreach (Cookie cook in cooks)
                {
                    TestForNewCookies(cook);
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
            oWB.SaveAs(ConfigurationManager.AppSettings["ChromeOutputFileDef"], Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();
            String path = Utilities.UploadToTeamSite(ConfigurationManager.AppSettings["ChromeOutputFileDef"]);
            if (!(Utilities.TestFile(@"X:\Sample\Chrome-CookiesDef.xlsx", ConfigurationManager.AppSettings["ChromeOutputFileDef"])))
            {
                Utilities.SendEmail(path,"Chrome Cookies -> Default");
            }
        }

        [Test]
        public void TestEncrypt()
        {
            var secretSection = ConfigurationManager.GetSection("localSecrets") as NameValueCollection;
            string secret;
            if (secretSection != null)
            {
                secret = secretSection["Password"]?.ToString();
                Console.WriteLine("{0}", secret);
            }
        }


        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        public void TestForNewCookies(OpenQA.Selenium.Cookie cook)
        {
            if (cook.Name != "NBGPUBLICConsent" && cook.Name != "NBGpublicSite" && cook.Name != "WSS_FullScreenMode" && cook.Name != "_ga" && cook.Name != "_gat"
                            && cook.Name != "_gid" && cook.Name != "Consent" && cook.Name != "NID")
            {
                Console.Write(cook.Name);
            }
        }
    }
}
