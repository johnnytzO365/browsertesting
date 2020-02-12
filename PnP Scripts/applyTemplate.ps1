Import-Module SharePointPnPPowerShellOnline

#initializations
$Url = "https://bousiou.sharepoint.com/sites/TransformationQA5"

#connect
$UserName = "sindy@bousiou.onmicrosoft.com"
$PassWord = "Y?Ugjxgar"
[SecureString]$SecurePassWord = ConvertTo-SecureString $PassWord -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePassWord)
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials
$templateUrl = "C:\Users\e82331\Desktop\TransformationTemplate3\Template.xml"
Set-PnPTraceLog -On -Level Debug 
Apply-PnPProvisioningTemplate -Path $templateUrl -ClearNavigation