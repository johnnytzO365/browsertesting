﻿using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.Security;
using Microsoft.SharePoint.Client;

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
            System.IO.StreamReader infile = new System.IO.StreamReader(ConfigurationManager.AppSettings["DomainsNames"]);

            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;
            oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

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
                int number=driver.Manage().Cookies.AllCookies.Count;
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
            uploadToTeamSite(ConfigurationManager.AppSettings["ChromeOutputFileAc"]);
        }

        [Test]
        public void CookiesBarRejectAll()
        {
            String line;
            System.IO.StreamReader infile = new System.IO.StreamReader(ConfigurationManager.AppSettings["DomainsNames"]);

            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;
            oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

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

            uploadToTeamSite(ConfigurationManager.AppSettings["ChromeOutputFileRej"]);
        }

        [Test]
        public void CookiesBarDefault()
        {
            String line;
            System.IO.StreamReader infile = new System.IO.StreamReader(ConfigurationManager.AppSettings["DomainsNames"]);

            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;
            oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

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
            uploadToTeamSite(ConfigurationManager.AppSettings["ChromeOutputFileDef"]);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        public void TestForNewCookies(Cookie cook ) {
            if (cook.Name != "NBGPUBLICConsent" && cook.Name != "NBGpublicSite" && cook.Name != "WSS_FullScreenMode" && cook.Name != "_ga" && cook.Name != "_gat"
                            && cook.Name != "_gid" && cook.Name != "Consent" && cook.Name != "NID")
            {
                Console.Write(cook.Name);
            }
        }

        public void uploadToTeamSite(String localPath)
        {
            var siteUrl = "http://v000080043:9993/sites/sp_team_nbg/";
            var userName = ConfigurationManager.AppSettings["username"];
            var password = ConfigurationManager.AppSettings["password"];
            using (ClientContext clientContext = new ClientContext(siteUrl))
            {
                SecureString securePassword = new SecureString();
                foreach (char c in password.ToCharArray())
                {
                    securePassword.AppendChar(c);
                }
                clientContext.AuthenticationMode = ClientAuthenticationMode.Default;
                clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);

                clientContext.ExecuteQuery();

                var ServerVersion = clientContext.ServerLibraryVersion.Major;

                var site = clientContext.Site;
                var web = clientContext.Site.RootWeb;

                clientContext.Load(web, w => w.ServerRelativeUrl);
                clientContext.ExecuteQuery();

                var serverRelativeUrl = clientContext.Site.RootWeb.ServerRelativeUrl;

                var name = DateTime.Now.ToString("yyyy/MM/dd");
                var list = clientContext.Web.GetList("CookieCheckerResults");
                list.EnableFolderCreation = true;
                list.Update();
                clientContext.ExecuteQuery();

                //To create the folder
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();

                itemCreateInfo.UnderlyingObjectType = FileSystemObjectType.Folder;
                itemCreateInfo.LeafName = name;

                ListItem newItem = list.AddItem(itemCreateInfo);
                newItem["Title"] = name;
                newItem.Update();
                clientContext.ExecuteQuery();
            }
        }
    }
}