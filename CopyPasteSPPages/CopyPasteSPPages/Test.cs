using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using OpenQA.Selenium.Chrome;
using System.Configuration;


namespace CopyPasteSPPages
{
    class Test
    {
        IWebDriver driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            driver.Manage().Window.Maximize();  //to use the desired width of window
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void CopyPaste()
        {
            String line;
            System.IO.StreamReader infile = new System.IO.StreamReader(@"C:\Users\e82337\Desktop\pageUrls.txt");
            //StreamWriter outfile = new StreamWriter(@"C:\Users\e82331\Desktop\htmls.csv");
            /*var csv = new StringBuilder();
            String firstLine = "Title,html";
            csv.AppendLine(firstLine);*/
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;
            oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

            oSheet.Cells[1, 1] = "Title";
            oSheet.Cells[1, 2] = "html";

            int counter = 2;
            while ((line = infile.ReadLine()) != null)
            {
                driver.Navigate().GoToUrl(line);
                try
                {
                    wait.Until(ExpectedConditions.AlertIsPresent());
                    var alert = driver.SwitchTo().Alert();
                    alert.SetAuthenticationCredentials("bank\\e82331", "123sindy^");
                    alert.Accept();
                }
                catch
                {

                }

                wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("article-content")));
                IWebElement content = driver.FindElement(By.ClassName("article-content"));
                String html = content.GetAttribute("outerHTML");

                String[] UrlSplits = line.Split('/');
                String Title = UrlSplits[UrlSplits.Length - 1];
                Title.Substring(Title.Length - 5); //trim .aspx

                oSheet.Cells[counter, 1] = Title;
                oSheet.Cells[counter, 2] = html;
                counter++;
                Thread.Sleep(10000);
            }

            //File.WriteAllText(@"C:\Users\e82331\Desktop\htmls.csv", csv.ToString());
            infile.Close();
            //outfile.Close();

            oXL.Visible = false;
            oXL.UserControl = false;
            oWB.SaveAs(@"C:\Users\e82337\Desktop\htmls.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();
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
                }
                catch
                {
                    try
                    {
                        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ccc-notify-accept")));//wizz
                        driver.FindElement(By.Id("ccc-notify-accept")).Click();
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
            oWB.SaveAs(ConfigurationManager.AppSettings["OutputFile"], Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();
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
                }
                catch
                {
                    try
                    {
                        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ccc-notify-accept!")));//wizz
                        driver.FindElement(By.Id("ccc-notify-accept!")).Click();
                    }
                    catch
                    {
                        try
                        {
                            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("checkbox_icon")));//developers
                            driver.FindElement(By.ClassName("checkbox_icon")).Click();
                            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("btn")));
                            driver.FindElement(By.CssSelector("btn")).Click();
                        }
                        catch
                        {
                            try
                            {
                                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='cookie - bar']/div/div[2]/div[3]/button")));//act4Greece
                                driver.FindElement(By.XPath("//*[@id='cookie - bar']/div/div[2]/div[3]/button")).Click();
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
            oWB.SaveAs(ConfigurationManager.AppSettings["OutputFile"], Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();
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
            oWB.SaveAs(ConfigurationManager.AppSettings["OutputFile"], Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();
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
    }
}
