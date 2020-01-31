using Microsoft.Office.Interop.Excel;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            var siteUrl = ConfigurationManager.AppSettings["teamSiteUrl"];
            using (ClientContext clientContext = new ClientContext(siteUrl))
            {
                string username = ConfigurationManager.AppSettings["username"];

                //decrypt password
                string base64Encoded = ConfigurationManager.AppSettings["password"];
                string password;
                byte[] data = System.Convert.FromBase64String(base64Encoded);
                password = System.Text.ASCIIEncoding.ASCII.GetString(data);

                NetworkCredential _myCredentials = new NetworkCredential(username, password);
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

                return ConfigurationManager.AppSettings["teamSiteUrl"]+"/CookieCheckerResults/" + name;
            }
        }

        static public String TestFile(String path1, String path2)
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
            if ((row1 != row2))
            {
                return "Number of sites";
            }

            String ret = "Lines ";
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
                        ret += i.ToString()+" ";
                    }
                }
            }
            return "OK";
        }

        static public void SendEmail(String path, String test, String result)
        {
            string ServiceSiteUrl = ConfigurationManager.AppSettings["sharepointOnline"];
            string ServiceUserName = ConfigurationManager.AppSettings["SPOUsername"];

            //decrypt password
            string base64Encoded = ConfigurationManager.AppSettings["SPOPassword"];
            string ServicePassword;
            byte[] data = System.Convert.FromBase64String(base64Encoded);
            ServicePassword = System.Text.ASCIIEncoding.ASCII.GetString(data);

            var securePassword = new SecureString();
            foreach (char c in ServicePassword)
            {
                securePassword.AppendChar(c);
            }

            var onlineCredentials = new SharePointOnlineCredentials(ServiceUserName, securePassword);

            var context = new ClientContext(ServiceSiteUrl);
            context.Credentials = onlineCredentials;
            context.ExecuteQuery();

            List<string> mailRecipients = ConfigurationManager.AppSettings["mailRecipients"].Split(';').ToList();
            var emailp = new EmailProperties();
            emailp.To = mailRecipients ;
            emailp.From = ConfigurationManager.AppSettings["SPOUsername"];
            emailp.Body = "Something went wrong with Cookie Checker Results in test " + test + ". Click <a href=\"" + path + "\">here</a> to check. Problem: "+result;
            emailp.Subject = "Cookie Checker Results problem!";
            Utility.SendEmail(context, emailp);
            context.ExecuteQuery();
        }
    }
}
