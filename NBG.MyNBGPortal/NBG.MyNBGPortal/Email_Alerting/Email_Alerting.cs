using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace NBG.MyNBGPortal.Email_Alerting
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class Email_Alerting : SPItemEventReceiver
    {
        /// <summary>
        /// An item was updated.
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);
            using (SPWeb web = properties.OpenWeb())
            {
                try
                {
                    SPListItem currentItem = properties.ListItem;
                    string EmailBody = currentItem["Anakoinoseis"].ToString();
                    var body1 = EmailBody.Split(new[] {";"}, StringSplitOptions.None);
                    var email = "";
                    currentItem["E_x002d_mail_x0020_Body"] = "";
                    for (int i = body1.Length; i > 1; i-=2 )
                    {
                        email = currentItem["E_x002d_mail_x0020_Body"].ToString();
                        var body = body1[i - 1].Split(new[] { "#" }, StringSplitOptions.None);
                        currentItem["E_x002d_mail_x0020_Body"] = email + body[1] + "<br>";
                    }
                    base.EventFiringEnabled = false;
                    currentItem.Update();
                    base.EventFiringEnabled = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                WriteLogFile(web, "Start sending emails");
                SPList groups = web.GetList("/greek/test/Lists/Expand Groups");
                SPListItemCollection items = groups.GetItems();
                foreach (SPListItem item in items)
                {
                    string name = item["User"].ToString();
                    name = name.Substring(3, name.Length - 3);
                    string email = web.EnsureUser(name).Email;

                    StringDictionary headers = new StringDictionary();
                    headers.Add("from", "sender@domain.com");
                    headers.Add("to", email);
                    headers.Add("subject", properties.ListItem["E-mail Subject"].ToString());
                    headers.Add("fAppendHtmlTag", "True"); //To enable HTML format

                    System.Text.StringBuilder strMessage = new System.Text.StringBuilder();
                    strMessage.Append(properties.ListItem["E-mail Body"].ToString());
                    SPUtility.SendEmail(web, headers, strMessage.ToString());

                    string message = "Email sent to " + name;
                    WriteLogFile(web, message);
                }
                WriteLogFile(web, "End sending emails");
            }
        }

        public static void WriteLogFile(SPWeb web, string message)
        {
            string sLibrary = "Έγγραφα";
            string sFilename = "log.txt";
            web.AllowUnsafeUpdates = true;
            SPDocumentLibrary documentLibrary = (SPDocumentLibrary)web.Lists[sLibrary];
            string log = documentLibrary.RootFolder.Url + @"/" + sFilename;
            SPFile spf = web.GetFile(log);
            ASCIIEncoding enc = new ASCIIEncoding();
            string dt = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
            message = dt + " " + message;
            if (!spf.Exists)
            {
                SPFolder myLibrary = web.Folders["Shared Documents"];
                Boolean replaceExistingFiles = true;
                String fileName = System.IO.Path.GetFileName("log.txt");
                byte[] buffer = enc.GetBytes(message);
                SPFile spfile = myLibrary.Files.Add(fileName, buffer, replaceExistingFiles);
                myLibrary.Update();
            }
            else
            {
                byte[] contents = spf.OpenBinary();
                string newContents = enc.GetString(contents) + Environment.NewLine + message;
                spf.SaveBinary(enc.GetBytes(newContents));
            }
        }
    }
}