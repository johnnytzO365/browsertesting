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
        
        protected global::System.Web.UI.WebControls.RequiredFieldValidator formBodyRequiredFieldValidator;
        
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
        private global::System.Web.UI.WebControls.RequiredFieldValidator @__BuildControlformBodyRequiredFieldValidator() {
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.RequiredFieldValidator();
            this.formBodyRequiredFieldValidator = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "formBodyRequiredFieldValidator";
            @__ctrl.ControlToValidate = "txtBody";
            @__ctrl.ErrorMessage = "";
            @__ctrl.CssClass = "errormsg";
            @__ctrl.Display = global::System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
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
            @__ctrl.CssClass = "form-reset";
            @__ctrl.CausesValidation = false;
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBindingbtnReset);
            @__ctrl.Click -= new System.EventHandler(this.btnReset_Click);
            @__ctrl.Click += new System.EventHandler(this.btnReset_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public void @__DataBindingbtnReset(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.Button dataBindingExpressionBuilderTarget;
            System.Web.UI.Control Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.Button)(sender));
            Container = ((System.Web.UI.Control)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.Text = global::System.Convert.ToString( ResetBtnLabel , global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnSubmit() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnSubmit = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnSubmit";
            @__ctrl.CssClass = "form-submit";
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBindingbtnSubmit);
            @__ctrl.Click -= new System.EventHandler(this.btnSubmit_Click);
            @__ctrl.Click += new System.EventHandler(this.btnSubmit_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public void @__DataBindingbtnSubmit(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.Button dataBindingExpressionBuilderTarget;
            System.Web.UI.Control Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.Button)(sender));
            Container = ((System.Web.UI.Control)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.Text = global::System.Convert.ToString( SubmitBtnLabel , global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private global::System.Web.UI.WebControls.Panel @__BuildControlFormPanel() {
            global::System.Web.UI.WebControls.Panel @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Panel();
            this.FormPanel = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "FormPanel";
            @__ctrl.DefaultButton = "btnSubmit";
            global::System.Web.UI.WebControls.TextBox @__ctrl1;
            @__ctrl1 = this.@__BuildControltxtFullName();
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(@__ctrl1);
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl2;
            @__ctrl2 = this.@__BuildControlFullNameRequiredFieldValidator();
            @__parser.AddParsedSubObject(@__ctrl2);
            global::System.Web.UI.WebControls.TextBox @__ctrl3;
            @__ctrl3 = this.@__BuildControltxtStreet();
            @__parser.AddParsedSubObject(@__ctrl3);
            global::System.Web.UI.WebControls.TextBox @__ctrl4;
            @__ctrl4 = this.@__BuildControltxtStreetNo();
            @__parser.AddParsedSubObject(@__ctrl4);
            global::System.Web.UI.WebControls.TextBox @__ctrl5;
            @__ctrl5 = this.@__BuildControltxtCity();
            @__parser.AddParsedSubObject(@__ctrl5);
            global::System.Web.UI.WebControls.TextBox @__ctrl6;
            @__ctrl6 = this.@__BuildControltxtZipCode();
            @__parser.AddParsedSubObject(@__ctrl6);
            global::System.Web.UI.WebControls.TextBox @__ctrl7;
            @__ctrl7 = this.@__BuildControltxtPhone();
            @__parser.AddParsedSubObject(@__ctrl7);
            global::System.Web.UI.WebControls.RegularExpressionValidator @__ctrl8;
            @__ctrl8 = this.@__BuildControltelRegularExpressionValidator();
            @__parser.AddParsedSubObject(@__ctrl8);
            global::System.Web.UI.WebControls.TextBox @__ctrl9;
            @__ctrl9 = this.@__BuildControltxtEMail();
            @__parser.AddParsedSubObject(@__ctrl9);
            global::System.Web.UI.WebControls.RegularExpressionValidator @__ctrl10;
            @__ctrl10 = this.@__BuildControlmailRegularExpressionValidator();
            @__parser.AddParsedSubObject(@__ctrl10);
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl11;
            @__ctrl11 = this.@__BuildControlEmailFieldValidator();
            @__parser.AddParsedSubObject(@__ctrl11);
            global::System.Web.UI.WebControls.RadioButtonList @__ctrl12;
            @__ctrl12 = this.@__BuildControlrbPartener();
            @__parser.AddParsedSubObject(@__ctrl12);
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl13;
            @__ctrl13 = this.@__BuildControlPartenerRequiredFieldValidator();
            @__parser.AddParsedSubObject(@__ctrl13);
            global::System.Web.UI.WebControls.DropDownList @__ctrl14;
            @__ctrl14 = this.@__BuildControlddlInterestedIn();
            @__parser.AddParsedSubObject(@__ctrl14);
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl15;
            @__ctrl15 = this.@__BuildControlinterestedInRequiredFieldValidator();
            @__parser.AddParsedSubObject(@__ctrl15);
            global::System.Web.UI.WebControls.TextBox @__ctrl16;
            @__ctrl16 = this.@__BuildControltxtBody();
            @__parser.AddParsedSubObject(@__ctrl16);
            global::System.Web.UI.WebControls.RequiredFieldValidator @__ctrl17;
            @__ctrl17 = this.@__BuildControlformBodyRequiredFieldValidator();
            @__parser.AddParsedSubObject(@__ctrl17);
            global::System.Web.UI.WebControls.Literal @__ctrl18;
            @__ctrl18 = this.@__BuildControlLegalPDFLiteral();
            @__parser.AddParsedSubObject(@__ctrl18);
            global::System.Web.UI.WebControls.Button @__ctrl19;
            @__ctrl19 = this.@__BuildControlbtnReset();
            @__parser.AddParsedSubObject(@__ctrl19);
            global::System.Web.UI.WebControls.Button @__ctrl20;
            @__ctrl20 = this.@__BuildControlbtnSubmit();
            @__parser.AddParsedSubObject(@__ctrl20);
            @__ctrl.SetRenderMethodDelegate(new System.Web.UI.RenderMethod(this.@__RenderFormPanel));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__RenderFormPanel(System.Web.UI.HtmlTextWriter @__w, System.Web.UI.Control parameterContainer) {
            @__w.Write("\n    <div class=\"node contact\">\n        <div class=\"field body\"><p>");
                           @__w.Write(NBG.PublicSiteNewApps.Core.Utils.GetLocString("contactinfo"));

            @__w.Write("</p></div>\n        <div class=\"field form\">\n            <div class=\"row clearfix\"" +
                    ">\n                <div class=\"col\">\n                    <div class=\"form-item cl" +
                    "earfix\">\n                        <label>");
                       @__w.Write(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormFullNameLabel"));

            @__w.Write("</label>\n                        ");
            parameterContainer.Controls[0].RenderControl(@__w);
            @__w.Write("\n                        <div>");
            parameterContainer.Controls[1].RenderControl(@__w);
            @__w.Write("</div>\n                    </div>\n                </div>\n            </div>\n     " +
                    "       <div class=\"row clearfix\">\n                <div class=\"col\">\n            " +
                    "        <div class=\"form-item clearfix\">\n                        <label>");
                       @__w.Write(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormStreetLabel"));

            @__w.Write("</label>\n                        ");
            parameterContainer.Controls[2].RenderControl(@__w);
            @__w.Write("\n                    </div>\n                </div>\n                <div class=\"co" +
                    "l\">\n                    <div class=\"form-item clearfix\">\n                       " +
                    " <label>");
                       @__w.Write(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormStreetNoLabel"));

            @__w.Write("</label>\n                        ");
            parameterContainer.Controls[3].RenderControl(@__w);
            @__w.Write("\n                    </div>\n                </div>\n            </div>\n           " +
                    " <div class=\"row clearfix\">\n                <div class=\"col\">\n                  " +
                    "  <div class=\"form-item clearfix\">\n                        <label>");
                       @__w.Write(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormCityLabel"));

            @__w.Write("</label>\n                        ");
            parameterContainer.Controls[4].RenderControl(@__w);
            @__w.Write("\n                    </div>\n                </div>\n                <div class=\"co" +
                    "l\">\n                    <div class=\"form-item clearfix\">\n                       " +
                    " <label>");
                       @__w.Write(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormZipCodeLabel"));

            @__w.Write("</label>\n                        ");
            parameterContainer.Controls[5].RenderControl(@__w);
            @__w.Write("\n                    </div>\n                </div>\n            </div>\n           " +
                    " <div class=\"row clearfix\">\n                <div class=\"col\">\n                  " +
                    "  <div class=\"form-item clearfix\">\n                        <label>");
                       @__w.Write(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormPhone"));

            @__w.Write("</label>\n                        <div>\n                            ");
            parameterContainer.Controls[6].RenderControl(@__w);
            @__w.Write("\n                            <div>");
            parameterContainer.Controls[7].RenderControl(@__w);
            @__w.Write("</div>\n                        </div>\n                    </div>\n                " +
                    "</div>\n                <div class=\"col\">\n                    <div class=\"form-it" +
                    "em clearfix\">\n                        <label>");
                       @__w.Write(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormEMailLabel"));

            @__w.Write("</label>\n                        ");
            parameterContainer.Controls[8].RenderControl(@__w);
            @__w.Write("\n                        <div>\n                            ");
            parameterContainer.Controls[9].RenderControl(@__w);
            @__w.Write("\n                            <div>");
            parameterContainer.Controls[10].RenderControl(@__w);
            @__w.Write(@"</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""row clearfix"">
                <div class=""col"">
                    <div class=""form-item clearfix"">
                        <label>");
                       @__w.Write(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormPartenerLabel"));

            @__w.Write("</label>\n                        ");
            parameterContainer.Controls[11].RenderControl(@__w);
            @__w.Write("\n                        ");
            parameterContainer.Controls[12].RenderControl(@__w);
            @__w.Write("\n                    </div>\n                </div>\n            </div>\n           " +
                    " <div class=\"row clearfix\">\n                <div class=\"col\">\n                  " +
                    "  <div class=\"form-item clearfix\">\n                        <label>");
                       @__w.Write(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormInterestedInLabel"));

            @__w.Write("</label>\n                        ");
            parameterContainer.Controls[13].RenderControl(@__w);
            @__w.Write("\n                        <div>");
            parameterContainer.Controls[14].RenderControl(@__w);
            @__w.Write("</div>\n                    </div>\n                </div>\n            </div> \n    " +
                    "        <div class=\"row clearfix\">\n                <div class=\"col full\">\n      " +
                    "              <div class=\"form-item clearfix\">\n                        <label>");
                       @__w.Write(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormBodyLabel"));

            @__w.Write("</label>\n                        ");
            parameterContainer.Controls[15].RenderControl(@__w);
            @__w.Write("\n                        <span class=\"info\"><span class=\"remaining\">");
                                                           @__w.Write(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormBodyConstraintLabel"));

            @__w.Write(":<em class=\"lettercount\"></em></span>\n                        <div>");
            parameterContainer.Controls[16].RenderControl(@__w);
            @__w.Write(@"</div>
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
                        <label>");
                       @__w.Write(NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormRequiredFieldsLabel"));

            @__w.Write("</label>\n                        <p class=\"light\">\n                            ");
            parameterContainer.Controls[17].RenderControl(@__w);
            @__w.Write(@"
                        </p>
                    </div>
                </div>
            </div>
                        
            <div class=""row clearfix"">
                <div class=""col full"">
                    <div class=""form-actions"">
                        ");
            parameterContainer.Controls[18].RenderControl(@__w);
            @__w.Write("\n                        ");
            parameterContainer.Controls[19].RenderControl(@__w);
            @__w.Write(@"
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


");
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::NBG.PublicSiteNewApps.WebParts.ContactFormNew.ContactFormNew @__ctrl) {
            global::System.Web.UI.WebControls.Panel @__ctrl1;
            @__ctrl1 = this.@__BuildControlSubmitResultPanel();
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(@__ctrl1);
            global::System.Web.UI.WebControls.Panel @__ctrl2;
            @__ctrl2 = this.@__BuildControlFormPanel();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__ctrl.SetRenderMethodDelegate(new System.Web.UI.RenderMethod(this.@__Render__control1));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__Render__control1(System.Web.UI.HtmlTextWriter @__w, System.Web.UI.Control parameterContainer) {
            @__w.Write("\r\n\n\n<div id=\"h1_full_width\">\n    <h1 class=\"with_padding\">");
                     @__w.Write( NBG.PublicSiteNewApps.Core.Utils.GetLocString("ContactFormTitle") );

            @__w.Write("</h1>\n</div>\n\n");
            parameterContainer.Controls[0].RenderControl(@__w);
            @__w.Write("\n");
            parameterContainer.Controls[1].RenderControl(@__w);
            @__w.Write("\n\r\n");
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
