$UserName = "e82331@nbg.gr"
$pwd = "p@ssw0rd"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://groupnbg.sharepoint.com/"
Connect-PnPOnline -Url $Url -Credentials $Credentials

Send-PnPMail -To e82331@nbg.gr -Subject test -Body test