Import-Module SharePointPnPPowerShell2013

#initializations
$Url = "https://authqa.nbg.gr/"

#connect
Connect-PnPOnline -Url $Url -UseWebLogin
$authWebUrl = (Get-PnPWeb).Url 

#open and initialize excel file
$outputPath = "C:\Users\e82331\Desktop\PublicSiteADGroups-BrokenPermissions.xlsx"
$excel = New-Object -ComObject excel.application
$excel.visible = $True
$workbook = $excel.Workbooks.Add()
$worksheet= $workbook.Worksheets.Item(1)

$worksheet.Cells.Item(1,1)= 'Url'
$worksheet.Cells.Item(1,2)= 'Name of AD Group'
$worksheet.Cells.Item(1,3)= 'Permission level'
$worksheet.Cells.Item(1,4) = 'Users'
$i=2

$webs = Get-PnPSubWebs -Recurse
foreach($web in $webs)
{
    $url = $web.Url
    Connect-PnPOnline -Url $url -UseWebLogin
    $thisweb = Get-PnPWeb -Includes RoleAssignments
    $groups = @()
    $permissionLevels = @()
    foreach($ra in $thisweb.RoleAssignments) {
        $group = $ra.Member
        $loginName = get-pnpproperty -ClientObject $group -Property LoginName
        $rolebindings = get-pnpproperty -ClientObject $ra -Property RoleDefinitionBindings
        $worksheet.Cells.Item($i,1)= $url
        $worksheet.Cells.Item($i,2)= $($loginName)
        $worksheet.Cells.Item($i,3)= "$($rolebindings.Name)"

        $members = Get-PnPGroupMembers -Identity $loginName
        $j=4
        foreach($member in $members)
        {
            Write-Host $member.LoginName
            $worksheet.Cells.Item($i,$j) = $member.LoginName
            $j +=1
        }
        $i += 1

        $groups.Add($($loginName))
        $permissionLevels.Add("$($rolebindings.Name)")
    }
    
    $listColl=Get-PnPList -Web $thisweb -Includes RoleAssignments,HasUniqueRoleAssignments     
    foreach($list in $listColl)  
    {    
        if($list.HasUniqueRoleAssignments)  
        {  
            foreach($ralist in $list.RoleAssignments) {
                $group = $ralist.Member
                $loginName = get-pnpproperty -ClientObject $group -Property LoginName
                $rolebindings = get-pnpproperty -ClientObject $ralist -Property RoleDefinitionBindings
                $Title = Get-PnpProperty -ClientObject $list -Property Title
                write-host "$($url+"/"+$Title) $($loginName) - $($rolebindings.Name)" 
                if($groups.Contains($($loginName)))
                {
                    $index = $groups.Contains($($loginName))
                    if(!($permissionLevels[$index].Equals($($rolebindings.Name))))
                    {
                        $worksheet.Cells.Item($i,1)= $url+"/"+$Title
                        $worksheet.Cells.Item($i,2)= $($loginName)
                        $worksheet.Cells.Item($i,3)= "$($rolebindings.Name)"

                        $members = Get-PnPGroupMembers -Identity $loginName
                        $j=4
                        foreach($member in $members)
                        {
                            Write-Host $member.LoginName
                            $worksheet.Cells.Item($i,$j) = $member.LoginName
                            $j +=1
                        }
                        $i += 1
                    }
                }
                else
                {
                    $worksheet.Cells.Item($i,1)= $url+"/"+$Title
                    $worksheet.Cells.Item($i,2)= $($loginName)
                    $worksheet.Cells.Item($i,3)= "$($rolebindings.Name)"

                    $members = Get-PnPGroupMembers -Identity $loginName
                    $j=4
                    foreach($member in $members)
                    {
                        Write-Host $member.LoginName
                        $worksheet.Cells.Item($i,$j) = $member.LoginName
                        $j +=1
                    }
                    $i += 1
                }
            }
        }

    }
}

$workbook.SaveAs($outputPath)
$excel.Quit()