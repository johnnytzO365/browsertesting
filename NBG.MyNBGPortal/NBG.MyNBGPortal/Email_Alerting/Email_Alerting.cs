using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Collections.Generic;

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
                    
                    currentItem.Update();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            
        }


    }
}