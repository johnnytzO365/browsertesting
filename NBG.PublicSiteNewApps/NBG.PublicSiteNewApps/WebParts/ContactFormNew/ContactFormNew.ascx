<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactFormNew.ascx.cs" Inherits="NBG.PublicSiteNewApps.WebParts.ContactFormNew.ContactFormNew" %>


<div id="h1_full_width">
    <h1 class="with_padding"><%= NBG.PublicSiteNewApps.Core.Utils.GetLocString("ContactFormTitle") %></h1>
</div>

<asp:Panel ID="SubmitResultPanel" runat="server" Visible="False">
    <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label>
</asp:Panel>
<asp:Panel ID="FormPanel" runat="server" DefaultButton="btnSubmit">
    <div class="node contact">
        <div class="field body"><p></p></div>
        <div class="field form">
            <div class="row clearfix">
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSite.Core.Utils.GetLocString("contactinfo")%></label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-text" />
                        <div><asp:RequiredFieldValidator ID="FullNameRequiredFieldValidator" ControlToValidate="txtFullName" ErrorMessage="" CssClass="errormsg" runat="server" Display="Dynamic" /></div>
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormStreetLabel")%></label>
                        <asp:TextBox ID="txtStreet" runat="server" CssClass="form-text" />
                    </div>
                </div>
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormStreetNoLabel")%></label>
                        <asp:TextBox ID="txtStreetNo" runat="server" CssClass="form-text" />
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormCityLabel")%></label>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-text" />
                    </div>
                </div>
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormZipCodeLabel")%></label>
                        <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-text" />
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormPhone")%></label>
                        <div>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-text" />
                            <div><asp:RegularExpressionValidator ID="telRegularExpressionValidator" runat="server" ErrorMessage="" ValidationExpression="^\d+$" ControlToValidate="txtPhone" CssClass="errormsg" ValidateEmptyText="true" Display="Dynamic"></asp:RegularExpressionValidator></div>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormEMailLabel")%></label>
                        <asp:TextBox ID="txtEMail" runat="server" CssClass="form-text" />
                        <div>
                            <asp:RegularExpressionValidator ID="mailRegularExpressionValidator" runat="server" ErrorMessage="emailvalidation" ControlToValidate="txtEMail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="errormsg" SetFocusOnError="true" Display="Dynamic"></asp:RegularExpressionValidator>
                            <div><asp:RequiredFieldValidator ID="EmailFieldValidator" ControlToValidate="txtEMail" ErrorMessage="" CssClass="errormsg" runat="server" Display="Dynamic" /></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormPartenerLabel")%></label>
                        <asp:RadioButtonList ID="rbPartener" runat="server" CssClass="form-radio-dynamic" RepeatDirection="Horizontal" />
                        <asp:RequiredFieldValidator ID="PartenerRequiredFieldValidator" runat="server" ControlToValidate="rbPartener" ErrorMessage="mandatoryfield" CssClass="errormsg" Display="Dynamic" />
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormInterestedInLabel")%></label>
                        <asp:DropDownList ID="ddlInterestedIn" runat="server" CssClass="form-select" ></asp:DropDownList>
                        <div><asp:RequiredFieldValidator ID="interestedInRequiredFieldValidator" runat="server" ControlToValidate="ddlInterestedIn" ErrorMessage="mandatoryfield" CssClass="errormsg" Display="Dynamic" InitialValue="-1" /></div>
                    </div>
                </div>
            </div> 
            <div class="row clearfix">
                <div class="col full">
                    <div class="form-item clearfix">
                        <label><%=NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormBodyLabel")%></label>
                        <asp:TextBox ID="txtBody" runat="server" CssClass="form-textarea" TextMode="MultiLine" onKeyUp="javascript:Count(this);" onChange="javascript:Count(this);" />
                        <span class="info"><span class="remaining"><%=NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormBodyConstraintLabel")%>:<em class="lettercount"></em></span>
                    </div>
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
                        <label><%=NBG.PublicSiteNewApps.Core.Utils.GetLocString("WPContactFormRequiredFieldsLabel")%></label>
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

