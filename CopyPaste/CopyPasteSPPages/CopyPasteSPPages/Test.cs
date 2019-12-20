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
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\e82331\Downloads\CommunicationPages.xlsx", ReadOnly: false);
            Microsoft.Office.Interop.Excel._Worksheet oSheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = oSheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            for(int i=2;i<=rowCount;i++)
            {
                String Url = xlRange.Cells[i, 2].Value2;
                driver.Navigate().GoToUrl(Url);
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

                wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("article-content")));
                IWebElement content = driver.FindElement(By.ClassName("article-content"));
                String html = content.GetAttribute("outerHTML");
                Console.WriteLine(html);

                oSheet.Cells[i, 4] = html;
            }

            xlApp.Visible = false;
            xlApp.UserControl = false;
            xlWorkbook.SaveAs(@"C:\Users\e82331\Desktop\CommunicationPages.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            xlWorkbook.Close();
            xlApp.Quit();
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}