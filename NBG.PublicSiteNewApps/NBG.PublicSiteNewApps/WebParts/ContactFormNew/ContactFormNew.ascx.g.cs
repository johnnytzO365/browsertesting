﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NBG.PublicSiteNewApps.WebParts.ContactFormNew {
    using System.Web.UI.WebControls.Expressions;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using System.Text;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.SharePoint.WebPartPages;
    using System.Web.SessionState;
    using System.Configuration;
    using Microsoft.SharePoint;
    using System.Web;
    using System.Web.DynamicData;
    using System.Web.Caching;
    using System.Web.Profile;
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI.WebControls;
    using System.Web.Security;
    using System;
    using Microsoft.SharePoint.Utilities;
    using System.Text.RegularExpressions;
    using System.Collections.Specialized;
    using System.Web.UI.WebControls.WebParts;
    using Microsoft.SharePoint.WebControls;
    
    
    public partial class ContactFormNew {
        
        protected global::System.Web.UI.WebControls.Label StatusLabel;
        
        protected global::System.Web.UI.WebControls.Panel SubmitResultPanel;
        
        protected global::System.Web.UI.WebControls.TextBox txtFullName;
        
        protected global::System.Web.UI.WebControls.RequiredFieldValidator FullNameRequiredFieldValidator;
        
        protected global::System.Web.UI.WebControls.TextBox txtStreet;
        
        protected global::System.Web.UI.WebControls.TextBox txtStreetNo;
        
        protected global::System.Web.UI.WebControls.TextBox txtCity;
        
        protected global::System.Web.UI.WebControls.TextBox txtZipCode;
        
        protected global::System.Web.UI.WebControls.TextBox txtPhone;
        
        protected global::System.Web.UI.WebControls.RegularExpressionValidator telRegularExpressionValidator;
        
        protected global::System.Web.UI.WebControls.TextBox txtEMail;
        
        protected global::System.Web.UI.WebControls.RegularExpressionValidator mailRegularExpressionValidator;
        
        protected global::System.Web.UI.WebControls.RequiredFieldValidator EmailFieldValidator;
        
        protected global::System.Web.UI.WebControls.RadioButtonList rbPartener;
        
        protected global::System.Web.UI.WebControls.RequiredFieldValidator PartenerRequiredFieldValidator;
        
        protected global::System.Web.UI.WebControls.DropDownList ddlInterestedIn;
        
        protected global::System.Web.UI.WebControls.RequiredFieldValidator interestedInRequiredFieldValidator;
        
        protected global::System.Web.UI.WebControls.TextBox txtBody;
        
        protected global::System.Web.UI.WebControls.Literal LegalPDFLiteral;
        
        protected global::System.Web.UI.WebControls.Button btnReset;
        
        protected global::System.Web.UI.WebControls.Button btnSubmit;
        
        protected global::System.Web.UI.WebControls.Panel FormPanel;
        
        public static implicit operator global::System.Web.UI.TemplateControl(ContactFormNew target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Label @__BuildControlStatusLabel() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            this.StatusLabel = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "StatusLabel";
            @__ctrl.Text = "";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Panel @__BuildControlSubmitResultPanel() {
            global::System.Web.UI.WebControls.Panel @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Panel();
            this.SubmitResultPanel = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "SubmitResultPanel";
            @__ctrl.Visible = false;
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n    "));
            global::System.Web.UI.WebControls.Label @__ctrl1;
            @__ctrl1 = this.@__BuildControlStatusLabel();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n"));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtFullName() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtFullName = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtFullName";
            @__ctrl.CssClass = "form-text";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.RequiredFieldValidator @__BuildControlFullNameRequiredFieldValidator() {
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.RequiredFieldValidator();
            this.FullNameRequiredFieldValidator = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "FullNameRequiredFieldValidator";
            @__ctrl.ControlToValidate = "txtFullName";
            @__ctrl.ErrorMessage = "";
            @__ctrl.CssClass = "errormsg";
            @__ctrl.Display = global::System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtStreet() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtStreet = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtStreet";
            @__ctrl.CssClass = "form-text";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtStreetNo() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtStreetNo = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtStreetNo";
            @__ctrl.CssClass = "form-text";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtCity() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtCity = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtCity";
            @__ctrl.CssClass = "form-text";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtZipCode() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtZipCode = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtZipCode";
            @__ctrl.CssClass = "form-text";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtPhone() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtPhone = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtPhone";
            @__ctrl.CssClass = "form-text";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.RegularExpressionValidator @__BuildControltelRegularExpressionValidator() {
            global::System.Web.UI.WebControls.RegularExpressionValidator @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.RegularExpressionValidator();
            this.telRegularExpressionValidator = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "telRegularExpressionValidator";
            @__ctrl.ErrorMessage = "";
            @__ctrl.ValidationExpression = "^\\d+$";
            @__ctrl.ControlToValidate = "txtPhone";
            @__ctrl.CssClass = "errormsg";
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("ValidateEmptyText", "true");
            @__ctrl.Display = global::System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtEMail() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtEMail = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtEMail";
            @__ctrl.CssClass = "form-text";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.RegularExpressionValidator @__BuildControlmailRegularExpressionValidator() {
            global::System.Web.UI.WebControls.RegularExpressionValidator @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.RegularExpressionValidator();
            this.mailRegularExpressionValidator = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "mailRegularExpressionValidator";
            @__ctrl.ErrorMessage = "emailvalidation";
            @__ctrl.ControlToValidate = "txtEMail";
            @__ctrl.ValidationExpression = "\\w+([-+.\']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            @__ctrl.CssClass = "errormsg";
            @__ctrl.SetFocusOnError = true;
            @__ctrl.Display = global::System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.RequiredFieldValidator @__BuildControlEmailFieldValidator() {
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.RequiredFieldValidator();
            this.EmailFieldValidator = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "EmailFieldValidator";
            @__ctrl.ControlToValidate = "txtEMail";
            @__ctrl.ErrorMessage = "";
            @__ctrl.CssClass = "errormsg";
            @__ctrl.Display = global::System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.RadioButtonList @__BuildControlrbPartener() {
            global::System.Web.UI.WebControls.RadioButtonList @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.RadioButtonList();
            this.rbPartener = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "rbPartener";
            @__ctrl.CssClass = "form-radio-dynamic";
            @__ctrl.RepeatDirection = global::System.Web.UI.WebControls.RepeatDirection.Horizontal;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.RequiredFieldValidator @__BuildControlPartenerRequiredFieldValidator() {
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.RequiredFieldValidator();
            this.PartenerRequiredFieldValidator = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "PartenerRequiredFieldValidator";
            @__ctrl.ControlToValidate = "rbPartener";
            @__ctrl.ErrorMessage = "mandatoryfield";
            @__ctrl.CssClass = "errormsg";
            @__ctrl.Display = global::System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.DropDownList @__BuildControlddlInterestedIn() {
            global::System.Web.UI.WebControls.DropDownList @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.DropDownList();
            this.ddlInterestedIn = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "ddlInterestedIn";
            @__ctrl.CssClass = "form-select";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.RequiredFieldValidator @__BuildControlinterestedInRequiredFieldValidator() {
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.RequiredFieldValidator();
            this.interestedInRequiredFieldValidator = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "interestedInRequiredFieldValidator";
            @__ctrl.ControlToValidate = "ddlInterestedIn";
            @__ctrl.ErrorMessage = "mandatoryfield";
            @__ctrl.CssClass = "errormsg";
            @__ctrl.Display = global::System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
            @__ctrl.InitialValue = "-1";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtBody() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtBody = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtBody";
            @__ctrl.CssClass = "form-textarea";
            @__ctrl.TextMode = global::System.Web.UI.WebControls.TextBoxMode.MultiLine;
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("onKeyUp", "javascript:Count(this);");
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("onChange", "javascript:Count(this);");
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Literal @__BuildControlLegalPDFLiteral() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.LegalPDFLiteral = @__ctrl;
            @__ctrl.ID = "LegalPDFLiteral";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnReset() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnReset = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnReset";
            @__ctrl.Text = "Καθαρισμός";
            @__ctrl.CssClass = "form-reset";
            @__ctrl.CausesValidation = false;
            @__ctrl.Click -= new System.EventHandler(this.btnReset_Click);
            @__ctrl.Click += new System.EventHandler(this.btnReset_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnSubmit() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnSubmit = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnSubmit";
            @__ctrl.Text = "Αποστολή";
            @__ctrl.CssClass = "form-submit";
            @__ctrl.Click -= new System.EventHandler(this.btnSubmit_Click);
            @__ctrl.Click += new System.EventHandler(this.btnSubmit_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Panel @__BuildControlFormPanel() {
            global::System.Web.UI.WebControls.Panel @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Panel();
            this.FormPanel = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "FormPanel";
            @__ctrl.DefaultButton = "btnSubmit";
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
    <div class=""node contact"">
        <div class=""field body""><p>Για οποιαδήποτε πληροφορία ή ερώτημα σχετικά με τα προϊόντα και τις υπηρεσίες μας, συμπληρώστε με ελληνικούς ή λατινικούς χαρακτήρες τα πεδία της παρακάτω φόρμας και θα επικοινωνήσουμε μαζί σας το συντομότερο δυνατόν.</p></div>
        <div class=""field form"">
            <div class=""row clearfix"">
                <div class=""col"">
                    <div class=""form-item clearfix"">
                        <label>Ονοματεπώνυμο - Επωνυμία*:</label>
                        "));
            global::System.Web.UI.WebControls.TextBox @__ctrl1;
            @__ctrl1 = this.@__BuildControltxtFullName();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                        <div>"));
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl2;
            @__ctrl2 = this.@__BuildControlFullNameRequiredFieldValidator();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"</div>
                    </div>
                </div>
            </div>
            <div class=""row clearfix"">
                <div class=""col"">
                    <div class=""form-item clearfix"">
                        <label>Οδός:</label>
                        "));
            global::System.Web.UI.WebControls.TextBox @__ctrl3;
            @__ctrl3 = this.@__BuildControltxtStreet();
            @__parser.AddParsedSubObject(@__ctrl3);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                    </div>\n                </div>\n                <div class=\"co" +
                        "l\">\n                    <div class=\"form-item clearfix\">\n                       " +
                        " <label>Αριθμός:</label>\n                        "));
            global::System.Web.UI.WebControls.TextBox @__ctrl4;
            @__ctrl4 = this.@__BuildControltxtStreetNo();
            @__parser.AddParsedSubObject(@__ctrl4);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
                    </div>
                </div>
            </div>
            <div class=""row clearfix"">
                <div class=""col"">
                    <div class=""form-item clearfix"">
                        <label>Πόλη - Περιοχή:</label>
                        "));
            global::System.Web.UI.WebControls.TextBox @__ctrl5;
            @__ctrl5 = this.@__BuildControltxtCity();
            @__parser.AddParsedSubObject(@__ctrl5);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                    </div>\n                </div>\n                <div class=\"co" +
                        "l\">\n                    <div class=\"form-item clearfix\">\n                       " +
                        " <label>ΤΚ:</label>\n                        "));
            global::System.Web.UI.WebControls.TextBox @__ctrl6;
            @__ctrl6 = this.@__BuildControltxtZipCode();
            @__parser.AddParsedSubObject(@__ctrl6);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
                    </div>
                </div>
            </div>
            <div class=""row clearfix"">
                <div class=""col"">
                    <div class=""form-item clearfix"">
                        <label>Τηλ. επικοινωνίας:</label>
                        <div>
                            "));
            global::System.Web.UI.WebControls.TextBox @__ctrl7;
            @__ctrl7 = this.@__BuildControltxtPhone();
            @__parser.AddParsedSubObject(@__ctrl7);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                            <div>"));
            global::System.Web.UI.WebControls.RegularExpressionValidator @__ctrl8;
            @__ctrl8 = this.@__BuildControltelRegularExpressionValidator();
            @__parser.AddParsedSubObject(@__ctrl8);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</div>\n                        </div>\n                    </div>\n                " +
                        "</div>\n                <div class=\"col\">\n                    <div class=\"form-it" +
                        "em clearfix\">\n                        <label>Email*:</label>\n                   " +
                        "     "));
            global::System.Web.UI.WebControls.TextBox @__ctrl9;
            @__ctrl9 = this.@__BuildControltxtEMail();
            @__parser.AddParsedSubObject(@__ctrl9);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                        <div>\n                            "));
            global::System.Web.UI.WebControls.RegularExpressionValidator @__ctrl10;
            @__ctrl10 = this.@__BuildControlmailRegularExpressionValidator();
            @__parser.AddParsedSubObject(@__ctrl10);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                            <div>"));
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl11;
            @__ctrl11 = this.@__BuildControlEmailFieldValidator();
            @__parser.AddParsedSubObject(@__ctrl11);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""row clearfix"">
                <div class=""col"">
                    <div class=""form-item clearfix"">
                        <label>Συνεργάζομαι με την Εθνική Τράπεζα*:</label>
                        "));
            global::System.Web.UI.WebControls.RadioButtonList @__ctrl12;
            @__ctrl12 = this.@__BuildControlrbPartener();
            @__parser.AddParsedSubObject(@__ctrl12);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                        "));
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl13;
            @__ctrl13 = this.@__BuildControlPartenerRequiredFieldValidator();
            @__parser.AddParsedSubObject(@__ctrl13);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
                    </div>
                </div>
            </div>
            <div class=""row clearfix"">
                <div class=""col"">
                    <div class=""form-item clearfix"">
                        <label>Ενδιαφέρομαι για*:</label>
                        "));
            global::System.Web.UI.WebControls.DropDownList @__ctrl14;
            @__ctrl14 = this.@__BuildControlddlInterestedIn();
            @__parser.AddParsedSubObject(@__ctrl14);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                        <div>"));
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl15;
            @__ctrl15 = this.@__BuildControlinterestedInRequiredFieldValidator();
            @__parser.AddParsedSubObject(@__ctrl15);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"</div>
                    </div>
                </div>
            </div> 
            <div class=""row clearfix"">
                <div class=""col full"">
                    <div class=""form-item clearfix"">
                        <label>Περιγραφή θέματος*:</label>
                        "));
            global::System.Web.UI.WebControls.TextBox @__ctrl16;
            @__ctrl16 = this.@__BuildControltxtBody();
            @__parser.AddParsedSubObject(@__ctrl16);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
                        <span class=""info""><span class=""remaining"">Υπολοιπόμενοι χαρακτήρες: <em class=""lettercount""></em></span>
                    </div>
                </div>
            </div>
                        
           

            
            <div class=""row clearfix"">
                <div class=""col full"">
                    <div class=""form-item smaller"">
                        
                        <div id=""captchaPlaceholder""></div>

                    </div>
                </div>
            </div>
               
            <div class=""row clearfix"">
                <div class=""col full"">
                    <div class=""form-item"">
                        <label>*Υποχρεωτικά πεδία</label>
                        <p class=""light"">
                            "));
            global::System.Web.UI.WebControls.Literal @__ctrl17;
            @__ctrl17 = this.@__BuildControlLegalPDFLiteral();
            @__parser.AddParsedSubObject(@__ctrl17);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
                        </p>
                    </div>
                </div>
            </div>
                        
            <div class=""row clearfix"">
                <div class=""col full"">
                    <div class=""form-actions"">
                        "));
            global::System.Web.UI.WebControls.Button @__ctrl18;
            @__ctrl18 = this.@__BuildControlbtnReset();
            @__parser.AddParsedSubObject(@__ctrl18);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                        "));
            global::System.Web.UI.WebControls.Button @__ctrl19;
            @__ctrl19 = this.@__BuildControlbtnSubmit();
            @__parser.AddParsedSubObject(@__ctrl19);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
                    </div>
                </div>
            </div>    
        </div>
    </div>
    <table width=""135"" border=""0"" cellpadding=""2"" cellspacing=""0"" style=""margin-top:20px;"" title=""Click to Verify - This site chose Symantec SSL for secure e-commerce and confidential communications."">
		<tr>
			<td width=""135"" align=""center"" valign=""top"">
				<script type=""text/javascript"" src=""https://seal.verisign.com/getseal?host_name=www.nbg.gr&amp;size=L&amp;use_flash=NO&amp;use_transparent=NO&amp;lang=en""></script><br />
				<a href=""http://www.symantec.com/ssl-certificates"" target=""_blank""  style=""color:#000000; text-decoration:none; font:bold 7px verdana,sans-serif; letter-spacing:.5px; text-align:center; margin:0px; padding:0px;"">ABOUT SSL CERTIFICATES</a>
			</td>
		</tr>
	</table>


"));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::NBG.PublicSiteNewApps.WebParts.ContactFormNew.ContactFormNew @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\n\n<div id=\"h1_full_width\">\n    <h1 class=\"with_padding\">Φόρμα Επικοινωνίας</h1>" +
                        "\n</div>\n\n"));
            global::System.Web.UI.WebControls.Panel @__ctrl1;
            @__ctrl1 = this.@__BuildControlSubmitResultPanel();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n"));
            global::System.Web.UI.WebControls.Panel @__ctrl2;
            @__ctrl2 = this.@__BuildControlFormPanel();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n\r\n"));
        }
        
        private void InitializeControl() {
            this.@__BuildControlTree(this);
            this.Load += new global::System.EventHandler(this.Page_Load);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual object Eval(string expression) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual string Eval(string expression, string format) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression, format);
        }
    }
}
