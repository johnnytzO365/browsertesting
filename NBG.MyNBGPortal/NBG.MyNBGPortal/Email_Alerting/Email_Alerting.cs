using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Collections.Generic;
using System.Collections.Specialized;

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
                    /*
                    string filePath = "http://vm-sp2013/greek/test/Shared%20Documents/log.txt";
                    using (Stream file = System.IO.File.OpenWrite(filePath))
                    {
                        string message = "Email sent to " + name;
                        byte[] buffer = Encoding.ASCII.GetBytes(message);
                        //Encoding.GetBytes(message, 0, message.Length, buffer, 0); 
                        file.Write(buffer, 0, message.Length);
                    }*/
                }
            
            }

            
        }


    }
}