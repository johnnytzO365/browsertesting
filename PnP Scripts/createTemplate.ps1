Import-Module SharePointPnPPowerShellOnline
Set-PnPTraceLog -On -Level:Debug

#initializations
$Url = "https://bousiou.sharepoint.com/sites/TransformationQA444"
$UserName = "sindy@bousiou.onmicrosoft.com"
$PassWord = "Y?Ugjxgar"
[SecureString]$SecurePassWord = ConvertTo-SecureString $PassWord -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePassWord)
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials

#create the template
$templateUrl = "C:\Users\e82331\Desktop\communicationTemplate\Template.xml"
Get-PnPProvisioningTemplate -Out $templateUrl -Handlers PageContents, Lists, Navigation, WebSettings,SiteSecurity -ListsToExtract "testList" -IncludeAllClientSidePages
Add-PnPDataRowsToProvisioningTemplate -Path $templateUrl -List "testList"