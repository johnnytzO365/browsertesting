Import-Module SharePointPnPPowerShell2013

$UserName = "spsetup"
$pwd = "p@ssw0rd"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)

$Url = "http://vm-sp2013"
Connect-PnPOnline -Url $Url -Credentials $Credentials
$webs = Get-PnPSubWebs -Recurse

foreach($web in $webs)
{
    $Url = $web.Url
    Connect-PnPOnline -Url $Url -Credentials $Credentials
    Write-Host "Get List SitePages"
    $pages = Get-PnPListItem -List Pages
    foreach($page in $pages)
    {
        $file = $page.File
        Get-PnPProperty -ClientObject $file -Property ServerRelativeUrl
        $pageUrl = $page.File.ServerRelativeUrl

        $webparts = Get-PnPWebPart -ServerRelativePageUrl $pageUrl
        foreach($webpart in $webparts) 
        {
            $webpartWebpart = $webpart.WebPart
            Get-PnPProperty -ClientObject $webpartWebpart -Property Title
        }
    }
}