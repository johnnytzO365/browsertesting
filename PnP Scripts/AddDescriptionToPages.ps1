#Connect
$UserName = "sindy@bousiou.onmicrosoft.com"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://bousiou.sharepoint.com/sites/communicationTest"
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials

$pageName = "1718.aspx"
$page = Get-PnPClientSidePage -Identity $pageName