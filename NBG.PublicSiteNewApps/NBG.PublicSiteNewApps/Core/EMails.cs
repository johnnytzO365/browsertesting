using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSP = NBG.PublicSiteNewApps;

namespace NBG.PublicSiteNewApps.Core
{
    class EMails
    {
        public static string CSS_CLASS_TD_FOOTER = "csstdfooter";
        public static string CSS_CLASS_A = "cssa";
        public static string CSS_CLASS_TD_BODY = "cssbody";
        public static string CSS_CLASS_TD_HEADER = "csstdheader";
        public static string CSS_CLASS_TABLE = "csstable";

        public static void NotifyNewContactRegistration(SPWeb spWeb, SPListItem spItem, string subject, List<string> recipients, List<string> recipientsBcc, string additionalRows, string emailAddressFrom)
        {
            try
            {
                if (Configuration.GetSendEMails(spWeb) == "1")
                {
                    if (recipients == null)
                        recipients = new List<string>();

                    //string email = Configuration.GetEmailForNewContact(spWeb);
                    //if (!string.IsNullOrEmpty(email))
                    //    recipients.Add(email);

                    //                    if (recipients != null && recipients.Count > 0)
                    if ((recipients == null ? false : recipients.Count > 0) || (recipientsBcc == null ? false : recipientsBcc.Count > 0))
                    {
                        //string subject = string.Format("{0}: New contact registration: '{1}'", Configuration.APPLICATION_NAME_EMAILSUBJECT, spItem[SPBuiltInFieldId.EMail]);
                        //string subject = string.Format("{0}: New contact registration: \"{1}\"", Configuration.APPLICATION_NAME_EMAILSUBJECT, GetPersonFullName(spItem));
                        if (subject.Length > 100)
                            subject = subject.Substring(0, 100) + "...";

                        List<Guid> fieldIds = new List<Guid>();
                        //fieldIds.Add(SPBuiltInFieldId.FirstName);
                        fieldIds.Add(SPBuiltInFieldId.Title);
                        fieldIds.Add(SPBuiltInFieldId.WorkAddress);
                        fieldIds.Add(SPBuiltInFieldId.WorkCity);
                        fieldIds.Add(SPBuiltInFieldId.WorkZip);
                        //fieldIds.Add(SPBuiltInFieldId.WorkPhone);
                        //fieldIds.Add(SPBuiltInFieldId.WorkFax);
                        //fieldIds.Add(SPBuiltInFieldId.WorkState);
                        //fieldIds.Add(SPBuiltInFieldId.WorkCountry);
                        fieldIds.Add(SPBuiltInFieldId.EMail);
                        fieldIds.Add(SPBuiltInFieldId.Email2);
                        fieldIds.Add(SPBuiltInFieldId.HomePhone);
                        //fieldIds.Add(SPBuiltInFieldId.Comments);
                        fieldIds.Add(Fields.ContactBy_Id);
                        fieldIds.Add(Fields.ContactBankCooperation_Id);
                        fieldIds.Add(Fields.ContactInterestedFor_Id);
                        fieldIds.Add(Fields.ContactSubject_Id);

                        string body = BuildEMailBody(spWeb, spItem, null, fieldIds.ToArray(), null, Core.Utils.GetLocString(spWeb, "ContactFormTitle"), null, additionalRows, false, false, false);
#if DEBUG
                        //Logger.LogEvent(LogEventSource, string.Format(Configuration.ERROR_STRING_FORMAT, "New contact registration", spItem.ID, spItem[SPBuiltInFieldId.EMail], body), System.Diagnostics.EventLogEntryType.Information);
#endif
                        System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress(emailAddressFrom);
                        SendEmail(spWeb, recipients, null, recipientsBcc, subject, body, fromAddress);
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.LogEvent(LogEventSource, string.Format(Configuration.ERROR_STRING_FORMAT, spItem.ID, "NotifyNewContactRegistration", "", ex.ToString()), System.Diagnostics.EventLogEntryType.Warning);
            }
        }

        public static string BuildEMailBody(SPWeb spWeb, SPListItem spItem, string itemUrl, Guid[] fieldIds, SPListItem spTaskItem, string header, string footer, string additionalRows, bool allowTemplatedStrings, bool allowTaskTemplatedStrings, bool showIdField)
        {
            StringBuilder sb = new StringBuilder();
            string taskItemUrl = null;
            SPList spList = null;

            if (spItem != null)
            {
                spList = spItem.ParentList;
                if (string.IsNullOrEmpty(itemUrl))
                {
                    BuildItemUrl(spWeb, spItem);
                }
            }
            if (spTaskItem != null)
            {
                SPList spListTasks = spTaskItem.ParentList;
                taskItemUrl = BuildTaskItemUrl(spWeb, spTaskItem);
            }

            //if (!allowTemplatedStrings)
            {
                sb.AppendFormat("<html>{0}", Environment.NewLine);
                sb.AppendFormat("<head>{0}", Environment.NewLine);
                sb.AppendFormat("<style type='text/css'>{0}", Environment.NewLine);

                sb.AppendFormat(".{1} {{width:100%;border:none; width:100%;font: 9pt Tahoma;color: #1f497d;border:none}}{0}", Environment.NewLine, CSS_CLASS_TABLE);
                sb.AppendFormat(".{1} {{width:100%; background:#eaf1dd; border:none;padding: 12pt 10px 20px 10px; font: 16pt Verdana; color:#1f497d }}{0}", Environment.NewLine, CSS_CLASS_TD_HEADER);
                sb.AppendFormat(".{1} {{width:100%;padding: 12pt 10px 24pt 10px; }}{0};border:none", Environment.NewLine, CSS_CLASS_TD_BODY);
                sb.AppendFormat(".{1} {{width:100%; border-top:1px solid #E8EAEC; border-bottom:1px solid #9CA3AD; padding: 4pt 10px 4pt 10px; color:Black; }}{0}", Environment.NewLine, CSS_CLASS_TD_FOOTER);
                sb.AppendFormat(".{1} {{color:#c00000; text-decoration:none underline;}}{0}", Environment.NewLine, CSS_CLASS_A);
                sb.AppendFormat("</style>{0}", Environment.NewLine);
                sb.AppendFormat("</head>{0}", Environment.NewLine);
                sb.AppendFormat("<body>{0}", Environment.NewLine);
            }

            sb.AppendFormat("<table class='{1}'>{0}", Environment.NewLine, CSS_CLASS_TABLE);

            sb.AppendFormat("<tr>{0}", Environment.NewLine);

            if (spItem != null)
                sb.AppendFormat("<td colspan='2' class='{1}'>{2} - {3}</td>{0}", Environment.NewLine, CSS_CLASS_TD_HEADER, CommonSP.Core.Configuration.APPLICATION_NAME_EMAILSUBJECT, (!string.IsNullOrEmpty(header) ? header : spItem[SPBuiltInFieldId.Title]));
            else if (allowTemplatedStrings)
                sb.AppendFormat("<td colspan='2' class='{1}'>{2} - {3}</td>{0}", Environment.NewLine, CSS_CLASS_TD_HEADER, CommonSP.Core.Configuration.APPLICATION_NAME_EMAILSUBJECT, (!string.IsNullOrEmpty(header) ? header : "[TITLE]"));
            sb.AppendFormat("</tr>{0}", Environment.NewLine);
            sb.AppendFormat("<tr>{0}", Environment.NewLine);
            sb.AppendFormat("<td colspan='2' class='{0}' style:border-style:none>", CSS_CLASS_TD_BODY);

            sb.AppendFormat("<table class='{0}',style='border-style:none'>", CSS_CLASS_TABLE);

            if (showIdField)
            {
                sb.AppendFormat("<tr>{0}", Environment.NewLine);
                sb.Append("<td style='width:15%'><b>Id:<b/></td>");
                if (spItem != null)
                    sb.AppendFormat("<td><b>{0}<b/></td>", spItem.ID);
                else if (allowTemplatedStrings)
                    sb.AppendFormat("<td><b>{0}<b/></td>", "[ID]");
                sb.AppendFormat("</tr>{0}", Environment.NewLine);
            }

            foreach (Guid fld in fieldIds)
            {
                sb.AppendFormat("<tr>{0}", Environment.NewLine);
                if (spItem != null)
                {
                    SPField spField = spList.Fields[fld];
                    sb.AppendFormat("<td style='width:15%'><b>{0}:<b/></td>", spField.Title);
                    string value = "";
                    if (spItem[fld] != null && !string.IsNullOrEmpty(spItem[fld].ToString()))
                    {
                        value = spItem[fld].ToString();
                        if (value.StartsWith(";#"))
                        {
                            value = value.Substring(2).Replace(";#", ", ");
                            if (value.EndsWith(", "))
                                value = value.Substring(0, value.Length - 2);
                        }
                    }
                    sb.AppendFormat("<td>{0}</td>", value);
                }
                else if (allowTemplatedStrings)
                {
                    sb.AppendFormat("<td style='width:15%'><b>[{0}_TITLE]:<b/></td>", fld);
                    sb.AppendFormat("<td>[{0}_VALUE]</td>", spItem[fld]);
                }
                sb.AppendFormat("</tr>{0}", Environment.NewLine);
            }

            sb.AppendFormat("<tr>{0}", Environment.NewLine);
            sb.Append("<td>&nbsp;</td>");
            sb.Append("<td>&nbsp;</td>");
            sb.AppendFormat("</tr>{0}", Environment.NewLine);

            if (additionalRows != null)
            {
                sb.AppendFormat("<tr>{0}", Environment.NewLine);
                sb.Append("<td>&nbsp;</td>");
                sb.Append("<td>&nbsp;</td>");
                sb.AppendFormat("</tr>{0}", Environment.NewLine);
                sb.Append(additionalRows);
            }
            sb.AppendFormat("</table>{0}", Environment.NewLine);

            sb.AppendFormat("</td>{0}", Environment.NewLine);
            sb.AppendFormat("</tr>{0}", Environment.NewLine);

            if (!string.IsNullOrEmpty(footer))
            {
                if (spItem != null)
                {
                    sb.AppendFormat("<tr{0}", Environment.NewLine);
                    //string linkText = (header != null ? header.Replace("<a href='{ITEM_URL}'>here</a>", string.Format("<a href='{0}' class='{1}'>here</a>", itemUrl, CSS_CLASS_A)) : string.Format("This request requires your action. Please click <a href='{0}' class='{1}'>here</a> to access your task.", itemUrl, CSS_CLASS_A));
                    string linkText = (footer != null ? footer.Replace("<a href='[ITEM_URL]'>here</a>", string.Format("<a href='{0}' class='{1}'>here</a>", itemUrl, CSS_CLASS_A)) : string.Format("This item requires your action. Please click <a href='{0}' class='{1}'>here</a> to access the item.", itemUrl, CSS_CLASS_A));
                    sb.AppendFormat("<td class='{0}' colspan='2'><i>{1}</i></td>", CSS_CLASS_TD_FOOTER, linkText);
                    sb.AppendFormat("</tr>{0}", Environment.NewLine);
                }
                else if (allowTemplatedStrings)
                {
                    sb.AppendFormat("<tr{0}", Environment.NewLine);
                    string linkText = (footer != null ? footer : string.Format("This item requires your action. Please click <a href='#{0}' class='{1}'>here</a> to access the item.", "[ITEM_URL]", CSS_CLASS_A));
                    sb.AppendFormat("<td class='{0}' colspan='2'><i>{1}</i></td>", CSS_CLASS_TD_FOOTER, linkText);
                    sb.AppendFormat("</tr>{0}", Environment.NewLine);
                }

                if (spTaskItem != null)
                {
                    sb.AppendFormat("<tr>{0}", Environment.NewLine);
                    string linkText = (footer != null ? footer.Replace("<a href='[ITEM_URL]'>here</a>", string.Format("<a href='{0}' class='{1}'>here</a>", taskItemUrl, CSS_CLASS_A)) : string.Format("This item requires your action. Please click <a href='{0}' class='{1}'>here</a> to access the item.", taskItemUrl, CSS_CLASS_A));
                    sb.AppendFormat("<td class='{0}' colspan='2'><i>{1}</i></td>", CSS_CLASS_TD_FOOTER, linkText);
                    sb.AppendFormat("</tr>{0}", Environment.NewLine);
                }
                else if (allowTemplatedStrings && allowTaskTemplatedStrings)
                {
                    sb.AppendFormat("<tr>{0}", Environment.NewLine);
                    string linkText = (footer != null ? footer : string.Format("This item requires your action. Please click <a href='#{0}' class='{1}'>here</a> to access the task.", "[TASK_ITEM_URL]", CSS_CLASS_A));
                    sb.AppendFormat("<td class='{0}' colspan='2'><i>{1}</i></td>", CSS_CLASS_TD_FOOTER, linkText);
                    sb.AppendFormat("</tr>{0}", Environment.NewLine);
                }
            }
            sb.AppendFormat("</table>{0}", Environment.NewLine);

            if (!allowTemplatedStrings)
            {
                sb.AppendFormat("</body>{0}", Environment.NewLine);
                sb.AppendFormat("</html>{0}", Environment.NewLine);
            }
            return sb.ToString();
        }

        public static void SendEmail(SPWeb spWeb, List<string> emailAddresses, List<string> ccEmailAddresses, List<string> bccEmailAddresses, string subject, string body, System.Net.Mail.MailAddress from)
        {
            SendEmail(spWeb, emailAddresses, ccEmailAddresses, bccEmailAddresses, subject, body, from, null);
        }

        public static void SendEmail(SPWeb spWeb, List<string> emailAddresses, List<string> ccEmailAddresses, List<string> bccEmailAddresses, string subject, string body, System.Net.Mail.MailAddress from, SPFile[] spFiles)
        {
            try
            {
                if (!string.IsNullOrEmpty(spWeb.Site.WebApplication.OutboundMailSenderAddress))
                {
                    using (System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage())
                    {
                        if (from != null)
                            msg.From = from;
                        else
                            msg.From = new System.Net.Mail.MailAddress(spWeb.Site.WebApplication.OutboundMailSenderAddress, spWeb.Title);
                        if (emailAddresses != null)
                        {
                            foreach (string email in emailAddresses)
                            {
                                if (!string.IsNullOrWhiteSpace(email))
                                    msg.To.Add(new System.Net.Mail.MailAddress(email));

                            }
                        }
                        if (ccEmailAddresses != null)
                        {
                            foreach (string email in ccEmailAddresses)
                            {
                                if (!string.IsNullOrWhiteSpace(email))
                                    msg.CC.Add(new System.Net.Mail.MailAddress(email));

                            }
                        }
                        if (bccEmailAddresses != null)
                        {
                            foreach (string email in bccEmailAddresses)
                            {
                                if (!string.IsNullOrWhiteSpace(email))
                                    msg.Bcc.Add(new System.Net.Mail.MailAddress(email));

                            }
                        }
                        msg.BodyEncoding = System.Text.Encoding.UTF8;
                        msg.IsBodyHtml = true;
                        msg.Body = body;
                        msg.Subject = subject;
                        if (!string.IsNullOrEmpty(subject) && subject.Length > 100)
                            msg.Subject = subject.Substring(0, 100) + "...";
                        msg.SubjectEncoding = System.Text.Encoding.UTF8;

                        if (spFiles != null)
                        {
                            for (int i = 0; i < spFiles.Length; i++)
                            {
                                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(spFiles[i].OpenBinaryStream(), spFiles[i].Name);
                                msg.Attachments.Add(attachment);
                            }
                        }
                        if (spWeb.Site.WebApplication.OutboundMailServiceInstance != null &&
                            spWeb.Site.WebApplication.OutboundMailServiceInstance.Server != null &&
                            !string.IsNullOrEmpty(spWeb.Site.WebApplication.OutboundMailServiceInstance.Server.Address))
                        {
                            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(spWeb.Site.WebApplication.OutboundMailServiceInstance.Server.Address);
                            client.Send(msg);
                        }
                    }
                }
#if DEBUG
                //if (emailAddresses != null)
                    //Logger.LogEvent(CommonSP.Core.Configuration.APPLICATION_NAME, string.Format(CommonSP.Core.Configuration.ERROR_STRING_FORMAT, null, "EMails", "SendEmail addresses", string.Join(",", emailAddresses.ToArray())), System.Diagnostics.EventLogEntryType.Information);
                //Logger.LogEvent(CommonSP.Core.Configuration.APPLICATION_NAME, string.Format(CommonSP.Core.Configuration.ERROR_STRING_FORMAT, null, "EMails", "SendEmail body", body), System.Diagnostics.EventLogEntryType.Information);
#endif
            }
            catch (Exception ex)
            {
                //Logger.LogEvent(CommonSP.Core.Configuration.APPLICATION_NAME, string.Format(CommonSP.Core.Configuration.ERROR_STRING_FORMAT, null, "EMails", "SendEmail", ex.ToString()), System.Diagnostics.EventLogEntryType.Warning);
            }
        }

        public static string BuildItemUrl(SPWeb spWeb, SPListItem spItem)
        {
            string itemUrl = null;
            if (spItem.ParentList.BaseType == SPBaseType.DocumentLibrary)
                itemUrl = string.Format("{0}", NBG.PublicSiteNewApps.Core.Utils.ConcatUrlPaths(spWeb.Url, spItem.File.Url));
            else
                itemUrl = string.Format("{0}?List={1}&ID={2}", NBG.PublicSiteNewApps.Core.Utils.ConcatUrlPaths(spWeb.Url, spItem.ParentList.RootFolder.Url), spItem.ParentList.ID.ToString(), spItem.ID.ToString());
            return itemUrl;
        }

        public static string BuildTaskItemUrl(SPWeb spWeb, SPListItem spTaskItem)
        {
            string itemUrl = string.Format("{0}/EditForm.aspx?List={1}&ID={2}", NBG.PublicSiteNewApps.Core.Utils.ConcatUrlPaths(spWeb.Url, spTaskItem.ParentList.RootFolder.Url), spTaskItem.ParentList.ID.ToString(), spTaskItem.ID.ToString());

            return itemUrl;
        }
    }
}
