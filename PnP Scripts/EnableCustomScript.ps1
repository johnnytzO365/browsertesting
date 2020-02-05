Import-Module -Name SharePointPnPPowerShellOnline -DisableNameChecking
$Url = "https://bousiou.sharepoint.com/sites/Transformation2"
$targetPath = "C:\Users\e82331\Desktop\TransformationTemplate\"

#connect
$UserName = "sindy@bousiou.onmicrosoft.com"
$PassWord = "Y?Ugjxgar"
[SecureString]$SecurePassWord = ConvertTo-SecureString $PassWord -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePassWord)
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials
$DenyAddAndCustomizePagesStatusEnum = [Microsoft.Online.SharePoint.TenantAdministration.DenyAddAndCustomizePagesStatus]
 
$context = Get-PnPContext
$site = Get-PnPTenantSite -Detailed -Url $Url
 
$site.DenyAddAndCustomizePages = $DenyAddAndCustomizePagesStatusEnum::Disabled
 
$site.Update()
$context.ExecuteQuery()
 
# This row should output list of your sites' URLs and the status of their custom scripting (disabled or not)
Get-PnPTenantSite -Detailed -Url $Url | select url,DenyAddAndCustomizePages
 
Disconnect-PnPOnline