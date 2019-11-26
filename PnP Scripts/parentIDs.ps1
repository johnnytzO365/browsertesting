$UserName = "spsetup"
$pwd = "p@ssw0rd"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)

$csvPath = "C:\Users\KyriakiBousiou\Desktop\out.csv"

$Url = "http://vm-sp2013"
getSubSitesWithParentIDs

function getSubSitesWithParentIDs()
{
    Connect-PnPOnline -Url $Url -Credentials $Credentials

    $parent = Get-PnPWeb
    $webs = Get-PnPSubWebs
    $parentID = $parent.Id
    foreach($web in $webs)
    {
        $title = $web.title
        $url = $web.url
        Write-Output "$title  $url  $parentID"
        $title, $url, $parentID | Export-Csv -Append -NoTypeInformation -Encoding UTF8 -Path $csvPath -Delimiter ","
        $parent = $web
        $Url = $web.Url
        getSubSitesWithParentIDs
    }
}