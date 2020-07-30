using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

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

        public string PartenerYesLabel = "Ναι";
        public string PartenerNoLabel = "Όχι";
        /*public string SubmitBtnLabel { get { return Core.Utils.GetLocString("WPGlobalSend"); } }
        public string ResetBtnLabel { get { return Core.Utils.GetLocString("WPContactFormResetBtnLabel"); } }
        public string ContactByPhoneLabel { get { return Core.Utils.GetLocString("WPContactFormContactByPhoneLabel"); } }
        public string ContactByEmailLabel { get { return Core.Utils.GetLocString("WPContactFormContactByEmailLabel"); } }*/

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

            /*if (SPContext.Current.FormContext.FormMode != Microsoft.SharePoint.WebControls.SPControlMode.Display)
            {
                FullNameRequiredFieldValidator.Visible = false;
                //PartenerRequiredFieldValidator.Visible = false;
                interestedInRequiredFieldValidator.Visible = false;
                //recaptchaid.Visible = false;
                //ContactCustomValidator.Visible = false;
                //EmailCustomValidator.Visible = false;
                //TelCustomValidator.Visible = false;

            }*/

            if (!Page.IsPostBack)
            {
                InitControls();
                DataBind();

                FullNameRequiredFieldValidator.ErrorMessage = "mandatory field";
                mailRegularExpressionValidator.ErrorMessage = "emailValidation";
                PartenerRequiredFieldValidator.ErrorMessage = "mandatory field";
                interestedInRequiredFieldValidator.ErrorMessage = "mandatory field";
                telRegularExpressionValidator.ErrorMessage = "wrong number";
                //TelCustomValidator.ErrorMessage = Core.Utils.GetLocString("mandatorytelno");
                EmailFieldValidator.ErrorMessage ="mandatory field";
                //ContactCustomValidator.ErrorMessage = Core.Utils.GetLocString("WPmandatorycontact");
            }
            else
            {
            }


        }


        private void AddNewContact()
        {
                 using (SPSite elevatedSite = new SPSite(SPContext.Current.Site.ID))
                {
                    //SPWeb web = SPContext.Current.Site.RootWeb;
                    SPWeb web = elevatedSite.RootWeb;
                    web.AllowUnsafeUpdates = true;
                    try
                    {
                        SPList lst = web.GetList("/Lists/ContactForms");
                        SPListItemCollection items = GetEmptyItemsCollection(lst);

                        SPContentTypeId ctid = lst.ContentTypes.BestMatch(new SPContentTypeId("0x0100548398963F53464698001F990988DBA7"));

                        SPListItem item = items.Add();
                        item[SPBuiltInFieldId.ContentTypeId] = ctid;
                        item[SPBuiltInFieldId.Title] = this.Context.Server.HtmlEncode(txtFullName.Text);
                        //item[SPBuiltInFieldId.WorkPhone] = txtPhone.Text;
                        item[SPBuiltInFieldId.WorkAddress] = string.Format("{0} {1}", this.Context.Server.HtmlEncode(txtStreet.Text), this.Context.Server.HtmlEncode(txtStreetNo.Text));
                        item[SPBuiltInFieldId.WorkCity] = this.Context.Server.HtmlEncode(txtCity.Text);
                        item[SPBuiltInFieldId.WorkZip] = this.Context.Server.HtmlEncode(txtZipCode.Text);
                        item[SPBuiltInFieldId.EMail] = this.Context.Server.HtmlEncode(txtEMail.Text);
                        //item[SPBuiltInFieldId.Email2] = this.Context.Server.HtmlEncode(txtContactEmail.Text);
                        item[SPBuiltInFieldId.HomePhone] = this.Context.Server.HtmlEncode(txtPhone.Text);

                        //SPFieldMultiChoiceValue val = new SPFieldMultiChoiceValue();
                        //if (cbContactByEmail.Checked) val.Add(Core.Utils.GetLocString("FLDContactByEmail"));
                        //if (cbContactByPhone.Checked) val.Add(Core.Utils.GetLocString("FLDContactByPhone"));
                        //item[NBG.PublicSite.Core.Fields.ContactBy_Id] = val.ToString();

                        item[ContactBankCooperation_Id] = rbPartener.SelectedValue; //rbPartenerYes.Checked;
                        item[ContactInterestedFor_Id] = ddlInterestedIn.SelectedItem.Value;
                        item[ContactSubject_Id] = this.Context.Server.HtmlEncode(txtBody.Text);

                        item.Update();//storing
                        SubmitResultPanel.Visible = true;
                        StatusLabel.Text = "<div class=\"sub_success\">Αγαπητέ κύριε / κυρία<br><br>Θα θέλαμε να επιβεβαιώσουμε την παραλαβή του μηνύματος σας και να σας ενημερώσουμε οτι θα επικοινωνήσουμε μαζί σας το συντομότερο δυνατόν.<br><br>Σας ευχαριστούμε,<br><br>Εθνική Τράπεζα<br>Κέντρο Εξυπηρέτησης Πελατείας<br></div>";
                        FormPanel.Visible = false;

                        //send email
                        /*string emailAddresses = Core.Configuration.GetValue1(SPContext.Current.Site.RootWeb, Core.Configuration.ConfigurationKeys.FormContact, Core.Configuration.ConfigurationCategories.Data);
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
                            Core.EMails.NotifyNewContactRegistration(web, item, emailSubject, lm, emailAddressesList, null, emailAddressFrom);
                        }*/
                    }
                    catch (Exception ex)//uls
                    {
                        //Logger.LogEvent(string.Format(Core.Configuration.ERROR_STRING_FORMAT, null, "ContactForm.AddNewContact", null, ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                        SubmitResultPanel.Visible = true;
                        StatusLabel.Text = "σφαλμα αποστολής";
                        FormPanel.Visible = false;
                    }
                    finally
                    {
                        web.AllowUnsafeUpdates = false;
                    }
                }
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


            //rbPartenerNo.Checked = false;
            //rbPartenerYes.Checked = false;
            ddlInterestedIn.Items.Clear();
            //ddlInterestedIn.Items.AddRange(GetInterestedItems());
            ddlInterestedIn.Items.Add(new ListItem("Επιλέξτε", "-1"));
            ddlInterestedIn.Items.Add(new ListItem("Καταθέσεις", "Καταθέσεις"));
            ddlInterestedIn.Items.Add(new ListItem("Κάρτες", "Κάρτες"));
            ddlInterestedIn.Items.Add(new ListItem("Διαγωνισμός Καινοτομίας & Τεχνολογίας", "Διαγωνισμός Καινοτομίας & Τεχνολογίας"));
            /*ddlInterestedIn.Items.Add(new ListItem(Core.Utils.GetLocString("DropHouseLoans"), Core.Utils.GetLocString("DropHouseLoans")));
            ddlInterestedIn.Items.Add(new ListItem(Core.Utils.GetLocString("DropConsumerLoans"), Core.Utils.GetLocString("DropConsumerLoans")));
            ddlInterestedIn.Items.Add(new ListItem(Core.Utils.GetLocString("DropCaringPrograms"), Core.Utils.GetLocString("DropCaringPrograms")));
            ddlInterestedIn.Items.Add(new ListItem(Core.Utils.GetLocString("DropSignibank"), Core.Utils.GetLocString("DropSignibank")));
            ddlInterestedIn.Items.Add(new ListItem(Core.Utils.GetLocString("DropShareTransactions"), Core.Utils.GetLocString("DropShareTransactions")));
            ddlInterestedIn.Items.Add(new ListItem(Core.Utils.GetLocString("DropSmallBusiness"), Core.Utils.GetLocString("DropSmallBusiness")));*/
            ddlInterestedIn.ClearSelection();
            ddlInterestedIn.SelectedIndex = 0;
            txtBody.Text = null;
            //txtCaptcha.Text = null;
            //recaptcha.
            txtCity.Text = null;
            //txtContactEmail.Text = null;
            txtEMail.Text = null;
            txtFullName.Text = null;
            txtPhone.Text = null;
            txtStreet.Text = null;
            txtStreetNo.Text = null;
            txtZipCode.Text = null;

        }
    }
}
