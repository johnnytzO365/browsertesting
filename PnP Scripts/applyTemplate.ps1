Import-Module SharePointPnPPowerShellOnline

$UserName = "sindy@bousiou.onmicrosoft.com"
$pwd = "Gld9q_31"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://bousiou.sharepoint.com/sites/Transformation"
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials

Write-Host "Applying Template with Document Libraries"

$templateUrl = "C:\Users\KyriakiBousiou\Desktop\TransformationTemplate\TransformationTemplate.xml"

Apply-PnPProvisioningTemplate -Path $templateUrl -Handlers Navigation, Lists, Pages, Files -ClearNavigation
