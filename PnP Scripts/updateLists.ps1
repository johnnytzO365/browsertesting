Import-Module SharePointPnPPowerShell2013

$UserName = "spsetup"
$pwd = "p@ssw0rd"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)

$Url = "http://vm-sp2013"
getSubSitesWithParentIDs

function getSubSitesWithParentIDs()
{
    Connect-PnPOnline -Url $Url -Credentials $Credentials

    $Ctx = New-Object Microsoft.SharePoint.Client.ClientContext($Url)
    $Ctx.Credentials = $Credentials

    $parent = Get-PnPWeb
    $webs = Get-PnPSubWebs
    foreach($web in $webs)
    {
        $lists = Get-PnpList
        foreach ($lib in $lists) 
        { 
            Try
            {
                $lib.EnableVersioning = $true 
                $lib.MajorVersionLimit = 1000
                $lib.Update()
                $Ctx.ExecuteQuery()
                Write-Host "Success for imported " $lib.title
            }
            Catch
            {
                Writh-Host "Error"
            }
        } 
        $parent = $web
        $Url = $web.Url
        getSubSitesWithParentIDs
    }
}