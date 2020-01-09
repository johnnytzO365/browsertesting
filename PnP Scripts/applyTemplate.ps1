Import-Module SharePointPnPPowerShellOnline

#initializations
$templateUrl = "C:\Users\e82331\Desktop\TeamSiteTemplate\TeamSiteTemplate.xml"
$Url = "https://bousiou.sharepoint.com/sites/TeamSiteBackup/"

#connect
$UserName = "sindy@bousiou.onmicrosoft.com"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials

Write-Host "Applying Template with Document Libraries"

Apply-PnPProvisioningTemplate -Path $templateUrl -Handlers Navigation, Lists, Pages, Files -ClearNavigation