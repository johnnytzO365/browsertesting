using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Microsoft.Office.Interop.Excel;

namespace CopyPasteSPPages
{
    class Test
    {
        IWebDriver driver = new InternetExplorerDriver("C:\\Users\\e82331\\Desktop\\Git\\browsertesting\\.nuget");
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
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\e82331\Desktop\pageUrls1.xlsx", ReadOnly: false);
            Worksheet oSheet = xlWorkbook.Sheets[1];
            Range xlRange = oSheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            for(int i=2;i<=rowCount;i++)
            {
                String Url = xlRange.Cells[i, 2].Value2;
                driver.Navigate().GoToUrl(Url);
                try
                {
                    wait.Until(ExpectedConditions.AlertIsPresent());
                    var alert = driver.SwitchTo().Alert();
                    alert.SetAuthenticationCredentials("bank\\e82331", "Y?Ugjxgar");
                    alert.Accept();
                }
                catch
                {
                }

                try
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("article-content")));
                    IWebElement content = driver.FindElement(By.ClassName("article-content"));
                    string html = content.GetAttribute("outerHTML");
                    Console.WriteLine(html);

                    oSheet.Cells[i, 5] = html;
                }
                catch
                {
                }
            }

            xlApp.Visible = false;
            xlApp.UserControl = false;
            xlWorkbook.SaveAs(@"C:\Users\e82331\Desktop\pageUrls.xlsx", XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,false, false, XlSaveAsAccessMode.xlNoChange,Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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