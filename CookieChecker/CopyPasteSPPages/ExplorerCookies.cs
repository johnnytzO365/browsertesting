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
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;
using System.Security;
using System.IO;
using System.Net;

namespace CookieChecker
{
    
    class ExplorerCookies
    {
        IWebDriver driver = new InternetExplorerDriver(ConfigurationManager.AppSettings["DriverPath"]);
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
            System.IO.StreamReader infile = new System.IO.StreamReader(ConfigurationManager.AppSettings["DomainsNames"]);

            Application oXL;
            Workbook oWB;
            Worksheet oSheet;
            oXL = new Microsoft.Office.Interop.Excel.Application();
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
                    alert.SetAuthenticationCredentials("bank\\e82331", "p@ssw0rd");
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
                List<OpenQA.Selenium.Cookie> cooks = driver.Manage().Cookies.AllCookies.ToList();
                Thread.Sleep(5000);
                oSheet.Cells[counter, 1] = line;
                oSheet.Cells[counter, 2] = number;
                foreach (OpenQA.Selenium.Cookie cook in cooks)
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
            oWB.SaveAs(ConfigurationManager.AppSettings["IEOutputFileAc"], Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();

            String path = uploadToTeamSite(ConfigurationManager.AppSettings["IEOutputFileAc"]);
            if (!(TestFile(@"X:\Sample\Explorer-CookiesAccept.xlsx", ConfigurationManager.AppSettings["IEOutputFileAc"])))
            {
                SendEmail(path, "IE Cookies -> Accept");
            }
        }

        [Test]
        public void CookiesBarRejectAll()
        {
            String line;
            System.IO.StreamReader infile = new System.IO.StreamReader(ConfigurationManager.AppSettings["DomainsNames"]);

            IWebDriver driver = new InternetExplorerDriver(ConfigurationManager.AppSettings["DriverPath"]);

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
                    alert.SetAuthenticationCredentials("bank\\e82337", "Bah7mut13!");
                    alert.Accept();
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
                List<OpenQA.Selenium.Cookie> cooks = driver.Manage().Cookies.AllCookies.ToList();
                Thread.Sleep(5000);
                oSheet.Cells[counter, 1] = line;
                oSheet.Cells[counter, 2] = number;
                foreach (OpenQA.Selenium.Cookie cook in cooks)
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
            oWB.SaveAs(ConfigurationManager.AppSettings["IEOutputFileRej"], Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();

            String path = uploadToTeamSite(ConfigurationManager.AppSettings["IEOutputFileRej"]);
            if (!(TestFile(@"X:\Sample\Explorer-CookiesRej.xlsx", ConfigurationManager.AppSettings["IEOutputFileRej"])))
            {
                SendEmail(path, "IE Cookies -> Reject");
            }
        }

        [Test]
        public void CookiesBarDefault()
        {
            String line;
            System.IO.StreamReader infile = new System.IO.StreamReader(ConfigurationManager.AppSettings["DomainsNames"]);

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
                    alert.SetAuthenticationCredentials("bank\\e82337", "Bah7mut13!");
                    alert.Accept();
                }
                catch
                {
                }
                int number = driver.Manage().Cookies.AllCookies.Count;
                List<OpenQA.Selenium.Cookie> cooks = driver.Manage().Cookies.AllCookies.ToList();
                Thread.Sleep(5000);
                oSheet.Cells[counter, 1] = line;
                oSheet.Cells[counter, 2] = number;
                foreach (OpenQA.Selenium.Cookie cook in cooks)
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
            oWB.SaveAs(ConfigurationManager.AppSettings["IEOutputFileDef"], Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();

            String path = uploadToTeamSite(ConfigurationManager.AppSettings["IEOutputFileDef"]);
            if (!(TestFile(@"X:\Sample\Explorer-CookiesDef.xlsx", ConfigurationManager.AppSettings["IEOutputFileDef"])))
            {
                SendEmail(path, "IE Cookies -> Default");
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        public String uploadToTeamSite(String localPath)
        {
            var siteUrl = "http://v000080043:9993/sites/sp_team_nbg/";
            using (ClientContext clientContext = new ClientContext(siteUrl))
            {
                NetworkCredential _myCredentials = new NetworkCredential("e82331", "p@ssw0rd");
                clientContext.Credentials = _myCredentials;
                clientContext.ExecuteQuery();

                var ServerVersion = clientContext.ServerLibraryVersion.Major;

                var site = clientContext.Site;
                var web = clientContext.Site.RootWeb;

                clientContext.Load(web, w => w.ServerRelativeUrl);
                clientContext.ExecuteQuery();

                var serverRelativeUrl = clientContext.Site.RootWeb.ServerRelativeUrl;

                //Check and create folder
                String name = DateTime.Now.ToString("yyyy.MM.dd");
                Microsoft.SharePoint.Client.List list = clientContext.Web.Lists.GetByTitle("CookieCheckerResults");
                FolderCollection folders = list.RootFolder.Folders;
                clientContext.Load(folders);
                clientContext.ExecuteQuery();

                var folderExists = folders.Any(X => X.Name == name);
                if (!folderExists)
                {
                    Folder newFolder = folders.Add(name);
                    clientContext.Load(newFolder);
                    clientContext.ExecuteQuery();
                }

                //Add the file
                String[] Splits = localPath.Split('\\');
                using (FileStream fs = new FileStream(localPath, FileMode.Open))
                {
                    Microsoft.SharePoint.Client.File.SaveBinaryDirect(clientContext, "/sites/sp_team_nbg/CookieCheckerResults/" + name + "/" + Splits[Splits.Length - 1], fs, true);
                }

                return "http://v000080043:9993/sites/sp_team_nbg/CookieCheckerResults/" + name;
            }
        }

        public bool TestFile(String path1, String path2)
        {
            Application excel1 = new Application();
            Workbook wb1 = excel1.Workbooks.Open(path1);
            Worksheet sheet1 = wb1.ActiveSheet;

            Application excel2 = new Application();
            Workbook wb2 = excel2.Workbooks.Open(path2);
            Worksheet sheet2 = wb2.ActiveSheet;

            int row1 = sheet1.Cells.Find("*", System.Reflection.Missing.Value,
                                           System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                           XlSearchOrder.xlByRows, XlSearchDirection.xlPrevious,
                                           false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
            int column1 = sheet1.Cells.Find("*", System.Reflection.Missing.Value,
                                           System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                           XlSearchOrder.xlByColumns, XlSearchDirection.xlPrevious,
                                           false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Column;
            int row2 = sheet2.Cells.Find("*", System.Reflection.Missing.Value,
                                           System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                           XlSearchOrder.xlByRows, XlSearchDirection.xlPrevious,
                                           false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
            int column2 = sheet2.Cells.Find("*", System.Reflection.Missing.Value,
                                           System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                           XlSearchOrder.xlByColumns, XlSearchDirection.xlPrevious,
                                           false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Column;
            if ((row1 != row2) || (column1 != column2))
            {
                return false;
            }
            for (int i = 1; i <= row1; i++)
            {
                for (int j = 1; j <= column1; j++)
                {
                    String str1 = "";
                    String str2 = "";
                    if (sheet1.Cells[i, j].Value2 != null)
                    {
                        str1 = sheet1.Cells[i, j].Value2.ToString();
                    }
                    if (sheet2.Cells[i, j].Value2 != null)
                    {
                        str2 = sheet2.Cells[i, j].Value2.ToString();
                    }
                    if (!(str1.Equals(str2)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void SendEmail(String path, String test)
        {
            string ServiceSiteUrl = "https://groupnbg.sharepoint.com/";
            string ServiceUserName = "e82331@nbg.gr";
            string ServicePassword = "p@ssw0rd";

            var securePassword = new SecureString();
            foreach (char c in ServicePassword)
            {
                securePassword.AppendChar(c);
            }

            var onlineCredentials = new SharePointOnlineCredentials(ServiceUserName, securePassword);

            var context = new ClientContext(ServiceSiteUrl);
            context.Credentials = onlineCredentials;
            context.ExecuteQuery();
            var emailp = new EmailProperties();
            emailp.To = new List<string> { "e82331@nbg.gr" };
            emailp.From = "e82331@nbg.gr";
            emailp.Body = "Something went wrong with Cookie Checker Results in test " + test + ". Click <a href=\"" + path + "\">here</a> to check.";
            emailp.Subject = "Cookie Checker Results problem!";
            Utility.SendEmail(context, emailp);
            context.ExecuteQuery();
        }
    }
}
