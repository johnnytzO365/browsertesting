Import-Module SharePointPnPPowerShellOnline

$UserName = "sindy@bousiou.onmicrosoft.com"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://bousiou.sharepoint.com/sites/communicationTest"
Connect-PnPOnline -Url $Url -Credentials $Credentials

$pages = Get-PnPListItem -List "SitePages"
foreach($item in $pages)
{
    $name = Get-PnPProperty -ClientObject $item.File -Property Name
    $page = Get-PnPClientSidePage -Identity $item.File.Name
    Set-PnPClientSidePage -Identity $name -Publish
}