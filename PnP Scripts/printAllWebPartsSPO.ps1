Import-Module SharePointPnPPowerShellOnline

$UserName = "e82331@nbg.gr"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)

$Url = "https://groupnbg.sharepoint.com/sites/TranformationQA"
Connect-PnPOnline -Url $Url -Credentials $Credentials
Write-Host "Get List SitePages"
$pages = Get-PnPListItem -List SitePages
foreach($page in $pages)
{
    $file = $page.File
    Get-PnPProperty -ClientObject $file -Property Name
    $pagePage = $page.File.Name

    Get-PnPClientSideComponent -Page $pagePage
}
