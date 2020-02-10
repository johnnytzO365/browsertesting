Import-Module SharePointPnPPowerShellOnline

$blue='/sites/communicationtopic/icons_news/blue.png'
$red='/sites/communicationtopic/icons_news/red.png'
$green='/sites/communicationtopic/icons_news/green.png'

#Connect
$UserName = "e82331@nbg.gr"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://groupnbg.sharepoint.com/sites/communicationtopic/"
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials

$pages = Get-PnPListItem -List SitePages

foreach($page in $pages)
{
    $name = Get-PnPProperty -ClientObject $page.File -Property Name
    if($name -ne "Home.aspx")
    {
        $clientSidePage = Get-PnPClientSidePage -Identity $name
        Set-PnPClientSidePage -Identity $name -PromoteAs NewsArticle

        $random = Get-Random -Minimum 1 -Maximum 4
        if($random -eq 1)
        {
            Set-PnPClientSidePage -Identity $name -ThumbnailUrl $blue
            $clientSidePage.SetCustomPageHeader($blue)
        }
        elseif($random -eq 2)
        {
            Set-PnPClientSidePage -Identity $name -ThumbnailUrl $red
            $clientSidePage.SetCustomPageHeader($red)
        }
        elseif($random -eq 3)
        {
            Set-PnPClientSidePage -Identity $name -ThumbnailUrl $green
            $clientSidePage.SetCustomPageHeader($green)
        }
        $clientSidePage.Save()
        Set-PnPClientSidePage -Identity $name -Publish
    }
}