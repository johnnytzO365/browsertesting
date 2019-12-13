using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using System.Configuration;
using System.IO;
using OpenQA.Selenium.Chrome;
using Microsoft.Office;

namespace CopyPasteSPPages
{
    class Test
    {
        IWebDriver driver = new InternetExplorerDriver("C:\\Users\\e82331\\Desktop\\IEDriverServer");
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
            System.IO.StreamReader infile = new System.IO.StreamReader(@"C:\Users\e82331\Desktop\pageUrls.txt");
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
                Console.WriteLine(html);

                String[] UrlSplits = line.Split('/');
                String Title = UrlSplits[UrlSplits.Length-1];
                Title.Substring(Title.Length - 5); //trim .aspx

                oSheet.Cells[counter, 1] = Title;
                oSheet.Cells[counter, 2] = html;

                counter++;
            }

            infile.Close();

            oXL.Visible = false;
            oXL.UserControl = false;
            oWB.SaveAs(@"C:\Users\e82331\Desktop\htmls.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            oWB.Close();
            oXL.Quit();
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}