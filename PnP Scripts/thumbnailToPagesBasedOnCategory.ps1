Import-Module SharePointPnPPowerShellOnline

$UserName = "sindy@bousiou.onmicrosoft.com"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://bousiou.sharepoint.com/sites/communicationTest"
Connect-PnPOnline -Url $Url -Credentials $Credentials

$leitourgia='https://bousiou.sharepoint.com/sites/communicationTest/images1/leitourgia.png'
$kliroseis='/sites/communicationTest/images1/kliroseis.png'
$alla='/sites/communicationTest/images1/alla.png'
$anthropino='/sites/communicationTest/images1/anthropino.png'
$proionta='/sites/communicationTest/images1/proionta.png'
$EKE='/sites/communicationTest/images1/EKE.png'
$protoi='/sites/communicationTest/images1/protoi.png'

$pages = Get-PnPListItem -List "SitePages"
foreach($item in $pages)
{
    Get-PnPProperty -ClientObject $item.File -Property Name
    $page = Get-PnPClientSidePage -Identity $item.File.Name
    $category = $item.FieldValues["Category"]
    if($category -eq "Λειτουργία Ομίλου")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $leitourgia
        $page.SetCustomPageHeader($leitourgia)
        $page.Save()
    }
    elseif($category -eq "Κληρώσεις/Διαγωνισμοί")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $kliroseis
        $page.SetCustomPageHeader($kliroseis)
        $page.Save()
    }
    elseif($category -eq "Άλλα θέματα")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $alla
        $page.SetCustomPageHeader($alla)
        $page.Save()
    }
    elseif($category -eq "Πρώτοι Εμείς")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $protoi
        $page.SetCustomPageHeader($protoi)
        $page.Save()
    }
    elseif($category -eq "Ανθρώπινο Δυναμικό")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $anthropino
        $page.SetCustomPageHeader($anthropino)
        $page.Save()
    }
    elseif($category -eq "ΕΚΕ")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $EKE
        $page.SetCustomPageHeader($EKE)
        $page.Save()
    }
    elseif($category -eq "Προϊόντα και Υπηρεσίες")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $proionta
        $page.SetCustomPageHeader($proionta)
        $page.Save()
    }
}