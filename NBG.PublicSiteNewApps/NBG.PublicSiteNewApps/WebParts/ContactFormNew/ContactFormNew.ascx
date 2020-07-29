<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactFormNew.ascx.cs" Inherits="NBG.PublicSiteNewApps.WebParts.ContactFormNew.ContactFormNew" %>

<script type="text/javascript">
    if (!String.prototype.trim) {
        String.prototype.trim = function () {
            return this.replace(/^\s+|\s+$/g, '');
        };
    }
    function Contact_by_Validate(source, arguments) {
        var cb1 = document.getElementById('<%= cbContactByPhone.ClientID %>');
        var cb2 = document.getElementById('<%= cbContactByEmail.ClientID %>');
        if ((cb1.checked == true) || (cb2.checked == true)) arguments.IsValid = true;
        else {
            arguments.IsValid = false;
        }
    }
    function Phone_Validate(source, arguments) {
        console.log('phone validate call');
        var cb1 = document.getElementById('<%= cbContactByPhone.ClientID %>');
        var phone = document.getElementById('<%= txtPhone.ClientID %>');
        var otherval = document.getElementById('<%= telRegularExpressionValidator.ClientID %>');

        if ((cb1.checked == true) && (phone.value.trim().length == 0)) arguments.IsValid = false;
        else {
            arguments.IsValid = true;
        }
    }
    function Email_Validate(source, arguments) {
        var cb2 = document.getElementById('<%= cbContactByEmail.ClientID %>');
        var mail = document.getElementById('<%= txtContactEmail.ClientID %>');
        if ((cb2.checked == true) && (mail.value.length == 0)) arguments.IsValid = false;
        else {
            arguments.IsValid = true;
        }
    }

    function Count(text) {
        //asp.net textarea maxlength doesnt work; do it by hand
        var maxlength = 1000; //set your value here (or add a parm and pass it in)
        var object = document.getElementById(text.id)  //get your object
        if (object.value.length > maxlength) {
            object.focus(); //set focus to prevent jumping
            object.value = text.value.substring(0, maxlength); //truncate the value
            object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
            return false;
        }
        return true;
    }

</script>

<div id="h1_full_width">
    <h1 class="with_padding"><%= NBG.PublicSite.Core.Utils.GetLocString("ContactFormTitle") %></h1>
</div>

<asp:Panel ID="SubmitResultPanel" runat="server" Visible="False">
    <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label>
</asp:Panel>
<asp:Panel ID="FormPanel" runat="server" DefaultButton="btnSubmit">
    <div class="node contact">
        <div class="field body"><p><%=NBG.PublicSite.Core.Utils.GetLocString("contactinfo")%></p></div>
        <div class="field form">
            <div class="row clearfix">
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormFullNameLabel")%></label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-text" />
                        <div><asp:RequiredFieldValidator ID="FullNameRequiredFieldValidator" ControlToValidate="txtFullName" ErrorMessage="" CssClass="errormsg" runat="server" Display="Dynamic" /></div>
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormStreetLabel")%></label>
                        <asp:TextBox ID="txtStreet" runat="server" CssClass="form-text" />
                    </div>
                </div>
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormStreetNoLabel")%></label>
                        <asp:TextBox ID="txtStreetNo" runat="server" CssClass="form-text" />
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormCityLabel")%></label>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-text" />
                    </div>
                </div>
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormZipCodeLabel")%></label>
                        <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-text" />
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormPhone")%></label>
                        <div>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-text" />
                            <div><asp:RegularExpressionValidator ID="telRegularExpressionValidator" runat="server" ErrorMessage="" ValidationExpression="^\d+$" ControlToValidate="txtPhone" CssClass="errormsg" ValidateEmptyText="true" Display="Dynamic"></asp:RegularExpressionValidator></div>
                            <div><asp:CustomValidator ID="TelCustomValidator" runat="server" ErrorMessage="" ClientValidationFunction="Phone_Validate" CssClass="errormsg" ForeColor="" OnServerValidate="Phone_ServerValidate" Display="Dynamic" ValidateEmptyText="true"></asp:CustomValidator></div>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormEMailLabel")%></label>
                        <asp:TextBox ID="txtEMail" runat="server" CssClass="form-text" />
                        <div>
                            <asp:RegularExpressionValidator ID="mailRegularExpressionValidator" runat="server" ErrorMessage="emailvalidation" ControlToValidate="txtEMail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="errormsg" SetFocusOnError="true" Display="Dynamic"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormPartenerLabel")%></label>
                        <asp:RadioButtonList ID="rbPartener" runat="server" CssClass="form-radio-dynamic" RepeatDirection="Horizontal" />
                        <asp:RequiredFieldValidator ID="PartenerRequiredFieldValidator" runat="server" ControlToValidate="rbPartener" ErrorMessage="mandatoryfield" CssClass="errormsg" Display="Dynamic" />
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormInterestedInLabel")%></label>
                        <asp:DropDownList ID="ddlInterestedIn" runat="server" CssClass="form-select" ></asp:DropDownList>
                        <div><asp:RequiredFieldValidator ID="interestedInRequiredFieldValidator" runat="server" ControlToValidate="ddlInterestedIn" ErrorMessage="mandatoryfield" CssClass="errormsg" Display="Dynamic" InitialValue="-1" /></div>
                    </div>
                </div>
            </div> 
            <div class="row clearfix">
                <div class="col full">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormBodyLabel")%></label>
                        <asp:TextBox ID="txtBody" runat="server" CssClass="form-textarea" TextMode="MultiLine" onKeyUp="javascript:Count(this);" onChange="javascript:Count(this);" />
                        <span class="info"><span class="remaining"><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormBodyConstraintLabel")%>: <em class="lettercount"></em></span>
                    </div>
                </div>
            </div>
                        
            <div class="row clearfix">
                <div class="col full">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormContactByPromptLabel")%></label>
                        <div class="ckeckitem">
                            <asp:CheckBox ID="cbContactByPhone" runat="server" CssClass="form-checkbox-custom" />
                            <asp:Label ID="Label1" CssClass="form-radiocheck" AssociatedControlID="cbContactByPhone" runat="server" ><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormContactByPhoneLabel")%><span class="hours"><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormContactByPhoneHoursLabel")%></span></asp:Label>
                        </div>
                        <div class="ckeckitem">
                            <asp:CheckBox ID="cbContactByEmail" runat="server" CssClass="form-checkbox-custom" />
                            <asp:Label ID="Label2" CssClass="form-radiocheck form-inline" AssociatedControlID="cbContactByEmail" runat="server" ><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormContactByEmailLabel")%></asp:Label>
                            <asp:TextBox ID="txtContactEmail" CssClass="form-text form-inline" runat="server"/>
                            <div class="mailvldtr">
                                <asp:CustomValidator ID="EmailCustomValidator" runat="server" ErrorMessage="" ClientValidationFunction="Email_Validate" 
            CssClass="errormsg" ForeColor="" OnServerValidate="Email_ServerValidate" Display="Dynamic"></asp:CustomValidator>
                            </div>
                        </div>
                    </div>
                    <asp:CustomValidator ID="ContactCustomValidator" runat="server" ErrorMessage="" ClientValidationFunction="Contact_by_Validate" 
        CssClass="errormsg choosecontact" ForeColor="" ValidateEmptyText="true" ControlToValidate="" 
            onservervalidate="Contact_by_ServerValidate" Display="Dynamic"></asp:CustomValidator>
                </div>
            </div>

            
            <div class="row clearfix">
                <div class="col full">
                    <div class="form-item smaller">
                        
                        <div id="captchaPlaceholder"></div>

                    </div>
                </div>
            </div>
               
            <div class="row clearfix">
                <div class="col full">
                    <div class="form-item">
                        <label><%=NBG.PublicSite.Core.Utils.GetLocString("WPContactFormRequiredFieldsLabel")%></label>
                        <p class="light">
                            <asp:Literal ID="LegalPDFLiteral" runat="server"></asp:Literal>
                        </p>
                    </div>
                </div>
            </div>
                        
            <div class="row clearfix">
                <div class="col full">
                    <div class="form-actions">
                        <asp:Button ID="btnReset" runat="server" Text='<%# ResetBtnLabel %>' CssClass="form-reset" OnClick="btnReset_Click" CausesValidation="false" />
                        <asp:Button ID="btnSubmit" runat="server" Text='<%# SubmitBtnLabel %>' CssClass="form-submit" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>    
        </div>
    </div>
    <table width="135" border="0" cellpadding="2" cellspacing="0" style="margin-top:20px;" title="Click to Verify - This site chose Symantec SSL for secure e-commerce and confidential communications.">
		<tr>
			<td width="135" align="center" valign="top">
				<script type="text/javascript" src="https://seal.verisign.com/getseal?host_name=www.nbg.gr&amp;size=L&amp;use_flash=NO&amp;use_transparent=NO&amp;lang=en"></script><br />
				<a href="http://www.symantec.com/ssl-certificates" target="_blank"  style="color:#000000; text-decoration:none; font:bold 7px verdana,sans-serif; letter-spacing:.5px; text-align:center; margin:0px; padding:0px;">ABOUT SSL CERTIFICATES</a>
			</td>
		</tr>
	</table>


</asp:Panel>

