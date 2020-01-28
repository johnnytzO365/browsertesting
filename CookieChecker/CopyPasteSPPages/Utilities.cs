using Microsoft.Office.Interop.Excel;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;

namespace CopyPasteSPPages
{
    class Utilities
    {
        static public String UploadToTeamSite(String localPath)
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
                clientContext.Load(list);
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

        static public bool TestFile(String path1, String path2)
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

        static public void SendEmail(String path, String test)
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
