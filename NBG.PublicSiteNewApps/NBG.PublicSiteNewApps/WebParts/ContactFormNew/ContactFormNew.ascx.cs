using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NBG.PublicSiteNewApps.CommonLibrary.Log;

namespace NBG.PublicSiteNewApps.WebParts.ContactFormNew
{
    [ToolboxItemAttribute(false)]
    public partial class ContactFormNew : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public ContactFormNew()
        {
        }

        public static Guid ContactBankCooperation_Id = new Guid("{C4080389-C093-4117-8F5D-F58729BFFE1A}");
        public static Guid ContactInterestedFor_Id = new Guid("{0EDCB11A-D7FA-4EC9-A6D9-0F7D6C466FEE}");
        public static Guid ContactSubject_Id = new Guid("{86C5A8CA-1F28-4EAA-9BA5-8C5DE247E3C7}");
        public static Guid ContactBy_Id = new Guid("{285AF751-8A5B-4430-AB32-CB48E579C067}");
        public string PartenerYesLabel { get { return Core.Utils.GetLocString("WPContactFormPartenerYesLabel"); } }
        public string PartenerNoLabel { get { return Core.Utils.GetLocString("WPContactFormPartenerNoLabel"); } }
        public string SubmitBtnLabel { get { return Core.Utils.GetLocString("WPGlobalSend"); } }
        public string ResetBtnLabel { get { return Core.Utils.GetLocString("WPContactFormResetBtnLabel"); } }
        //public string ContactByPhoneLabel { get { return Core.Utils.GetLocString("WPContactFormContactByPhoneLabel"); } }
        //public string ContactByEmailLabel { get { return Core.Utils.GetLocString("WPContactFormContactByEmailLabel"); } }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            InitControls();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                AddNewContact();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (SPContext.Current.Web.Language == 1033)
                LegalPDFLiteral.Text = string.Format(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormPersonalDataLegalLabel"), "/english/contact/Documents/Personal+Data+Protection.pdf");
            else LegalPDFLiteral.Text = string.Format(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormPersonalDataLegalLabel"), "/greek/contact/Documents/Προστασία+Δεδομένων+Προσωπικού+Χαρακτήρα.pdf");


            if (SPContext.Current.FormContext.FormMode != Microsoft.SharePoint.WebControls.SPControlMode.Display)
            {
                FullNameRequiredFieldValidator.Visible = false;
                FullNameRequiredFieldValidator.Enabled = false;
                PartenerRequiredFieldValidator.Visible = false;
                PartenerRequiredFieldValidator.Enabled = false;
                interestedInRequiredFieldValidator.Visible = false;
                interestedInRequiredFieldValidator.Enabled = false;
                EmailFieldValidator.Visible = false;
                EmailFieldValidator.Enabled = false;
                formBodyRequiredFieldValidator.Visible = false;
                formBodyRequiredFieldValidator.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                InitControls();
                DataBind();

                FullNameRequiredFieldValidator.ErrorMessage = Core.Utils.GetLocString("mandatoryfield");
                formBodyRequiredFieldValidator.ErrorMessage = Core.Utils.GetLocString("mandatoryfield");
                EmailFieldValidator.ErrorMessage = Core.Utils.GetLocString("mandatoryfield");
                mailRegularExpressionValidator.ErrorMessage = Core.Utils.GetLocString("emailvalidation");
                PartenerRequiredFieldValidator.ErrorMessage = Core.Utils.GetLocString("mandatoryfield");
                interestedInRequiredFieldValidator.ErrorMessage = Core.Utils.GetLocString("mandatoryfield");
                telRegularExpressionValidator.ErrorMessage = Core.Utils.GetLocString("wrongnumber");
            }
            else
            {
            }


        }


        private void AddNewContact()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
            using (SPSite elevatedSite = new SPSite(SPContext.Current.Site.ID))
            {
                //SPWeb web = SPContext.Current.Site.RootWeb;
                SPWeb web = elevatedSite.RootWeb;
                web.AllowUnsafeUpdates = true;
                try
                {
                    SPList lst = web.Lists["Contact Forms"];
                    SPListItemCollection items = GetEmptyItemsCollection(lst);

                    SPContentTypeId ctid = lst.ContentTypes.BestMatch(new SPContentTypeId("0x0100548398963F53464698001F990988DBA7"));

                    SPListItem item = items.Add();
                    item[SPBuiltInFieldId.ContentTypeId] = ctid;
                    item[SPBuiltInFieldId.Title] = this.Context.Server.HtmlEncode(txtFullName.Text);
                    item[SPBuiltInFieldId.WorkAddress] = string.Format("{0} {1}", this.Context.Server.HtmlEncode(txtStreet.Text), this.Context.Server.HtmlEncode(txtStreetNo.Text));
                    item[SPBuiltInFieldId.WorkCity] = this.Context.Server.HtmlEncode(txtCity.Text);
                    item[SPBuiltInFieldId.WorkZip] = this.Context.Server.HtmlEncode(txtZipCode.Text);
                    item[SPBuiltInFieldId.EMail] = this.Context.Server.HtmlEncode(txtEMail.Text);
                    item[SPBuiltInFieldId.HomePhone] = this.Context.Server.HtmlEncode(txtPhone.Text);

                    //SPFieldMultiChoiceValue val = new SPFieldMultiChoiceValue();
                    //if (cbContactByEmail.Checked) val.Add(Core.Utils.GetLocString("FLDContactByEmail"));
                    //if (cbContactByPhone.Checked) val.Add(Core.Utils.GetLocString("FLDContactByPhone"));
             
                    item[ContactBy_Id] = "By e-mail";
                    item[ContactBankCooperation_Id] = rbPartener.SelectedValue; //rbPartenerYes.Checked;
                    item[ContactInterestedFor_Id] = ddlInterestedIn.SelectedItem.Value;
                    item[ContactSubject_Id] = this.Context.Server.HtmlEncode(txtBody.Text);

                    item.Update();//storing
                    SubmitResultPanel.Visible = true;
                    StatusLabel.Text = "<div class=\"sub_success\">" + Core.Utils.GetLocString("loanformsubmissionsalut") + "<br /><br />" + Core.Utils.GetLocString("loanformsubmissionbody");
                    StatusLabel.Text += "<br /><br />" + Core.Utils.GetLocString("FormThankYou") + "<br /><br />";
                    StatusLabel.Text += Core.Utils.GetLocString("Menunbgbank") + "<br />";
                    StatusLabel.Text += Core.Utils.GetLocString("FormSignature") + "<br />";
                    StatusLabel.Text += "</div>";
                    FormPanel.Visible = false;

                    //send email
                    string emailAddresses = Core.Configuration.GetValue1(SPContext.Current.Site.RootWeb, Core.Configuration.ConfigurationKeys.FormContact, Core.Configuration.ConfigurationCategories.Data);
                    string emailUser = item[SPBuiltInFieldId.EMail] as string;
                    if (string.IsNullOrEmpty(emailUser))
                        emailUser = item[SPBuiltInFieldId.Email2] as string;
                    if (!string.IsNullOrEmpty(emailAddresses))
                    {
                        // Expected format for emailAddresses: user1@test.com;user2@test2.com

                        string emailSubject = Core.Configuration.GetValue2(SPContext.Current.Site.RootWeb, Core.Configuration.ConfigurationKeys.FormContact, Core.Configuration.ConfigurationCategories.Data);
                        string emailAddressFrom = Core.Configuration.GetValue1(SPContext.Current.Site.RootWeb, Core.Configuration.ConfigurationKeys.FormEMailFrom, Core.Configuration.ConfigurationCategories.Data);

                        if (string.IsNullOrEmpty(emailAddressFrom))
                            emailAddressFrom = "Nbg.donotreply@nbg.gr";
                        if (string.IsNullOrEmpty(emailSubject))
                            emailSubject = Core.Utils.GetLocString("contactEmailSubject");

                        List<string> emailAddressesList = new List<string>();
                        emailAddressesList.AddRange(emailAddresses.Split(new char[] { ';' }));
                        List<string> lm = new List<string>();
                        if (!string.IsNullOrWhiteSpace(emailUser))
                        {
                            lm.Add(emailUser);
                        }

                        //List<string> lm1 = new List<string>();
                        //lm1.Add("spsetup@spdev.local");
                        Core.EMails.NotifyNewContactRegistration(web, item, emailSubject, lm, emailAddressesList, null, emailAddressFrom);
                    }
                }
                catch (Exception ex)//uls
                {
                    Logger.LogEvent(string.Format(Core.Configuration.ERROR_STRING_FORMAT, null, "ContactForm.AddNewContact", null, ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                    SubmitResultPanel.Visible = true;
                    StatusLabel.Text = "σφαλμα αποστολής";
                    FormPanel.Visible = false;
                }
                finally
                {
                    web.AllowUnsafeUpdates = false;
                }
            }
            });
        }

        public SPListItemCollection GetEmptyItemsCollection(SPList spList)
        {
            SPQuery spQuery = new SPQuery();
            spQuery.Query = "  <Where><Lt><FieldRef Name='ID' /><Value Type='Counter'>0</Value></Lt></Where>";

            SPListItemCollection items = spList.GetItems(spQuery);

            return items;
        }

        private void InitControls()
        {
            rbPartener.Items.Clear();
            rbPartener.Items.Add(new ListItem(PartenerYesLabel, "1"));
            rbPartener.Items.Add(new ListItem(PartenerNoLabel, "0"));
            rbPartener.ClearSelection();

            uint language = SPContext.Current.Web.Language;
            ddlInterestedIn.Items.Clear();
            if (language.Equals(1032))
            {
                ddlInterestedIn.Items.Add(new ListItem("Επιλέξτε", "-1"));
            }
            else
            {
                ddlInterestedIn.Items.Add(new ListItem("Choose", "-1"));
            }
            SPList config = SPContext.Current.Site.RootWeb.Lists[Core.Configuration.ListNames.Configuration]; 
            SPQuery query = new SPQuery();
            if (SPContext.Current.Web.Language == 1033)
            {
                query.Query = "<Where><Eq><FieldRef Name=\"Title\" /><Value Type=\"Text\">ContactFormDropDownChoicesEn</Value></Eq></Where>";
            }
            else
            {
                query.Query = "<Where><Eq><FieldRef Name=\"Title\" /><Value Type=\"Text\">ContactFormDropDownChoicesEl</Value></Eq></Where>";
            }
            SPListItemCollection item = config.GetItems(query);
            string value = item[0]["Config Value1"].ToString();
            string[] choices = value.Split(';');
            foreach (string choice in choices) {
                ddlInterestedIn.Items.Add(choice);
            }
                

            ddlInterestedIn.ClearSelection();
            ddlInterestedIn.SelectedIndex = 0;
            txtBody.Text = null;
            txtCity.Text = null;
            txtEMail.Text = null;
            txtFullName.Text = null;
            txtPhone.Text = null;
            txtStreet.Text = null;
            txtStreetNo.Text = null;
            txtZipCode.Text = null;

        }
    }
}
