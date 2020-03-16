Import-Module SharePointPnPPowerShellOnline

#initializations
$Url = "https://groupnbg.sharepoint.com/sites/TransformationQA/"

#connect
$UserName = "e82331@nbg.gr"
$PassWord = "Y?Ugjxgar"
[SecureString]$SecurePassWord = ConvertTo-SecureString $PassWord -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePassWord)
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials
$templateUrl = "C:\Users\e82331\Desktop\Transformation\Template.xml"
Set-PnPTraceLog -On -Level Debug 
Apply-PnPProvisioningTemplate -Path $templateUrl -ClearNavigation