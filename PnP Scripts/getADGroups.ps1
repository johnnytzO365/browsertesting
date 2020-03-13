Import-Module SharePointPnPPowerShell2013

#initializations
$Url = "https://authqa.nbg.gr/"

#connect
Connect-PnPOnline -Url $Url -UseWebLogin
$authWebUrl = (Get-PnPWeb).Url 

$webs = Get-PnPSubWebs -Recurse
foreach($web in $webs)
{
    $url = $web.Url
    Connect-PnPOnline -Url $url -UseWebLogin
    $thisweb = Get-PnPWeb -Includes RoleAssignments
    foreach($ra in $thisweb.RoleAssignments) {
        $group = $ra.Member
        $loginName = get-pnpproperty -ClientObject $group -Property LoginName
        $rolebindings = get-pnpproperty -ClientObject $ra -Property RoleDefinitionBindings
        write-host "$url $($loginName) - $($rolebindings.Name)" 
        $members = Get-PnPGroupMembers -Identity $loginName
        foreach($member in $members)
        {
            Write-Host $member.LoginName
        }
    }

    <#
    $listColl=Get-PnPList -Web $thisweb -Includes HasUniqueRoleAssignments     
    foreach($list in $listColl)  
    {    
        if($list.HasUniqueRoleAssignments)  
        {  
            foreach($ralist in $list.RoleAssignments) {
                $member = $ralist.Member
                $loginName = get-pnpproperty -ClientObject $member -Property LoginName
                $rolebindings = get-pnpproperty -ClientObject $ralist -Property RoleDefinitionBindings
                write-host "$($url+$list) $($loginName) - $($rolebindings.Name)" 
            }
        }

    }#>
}