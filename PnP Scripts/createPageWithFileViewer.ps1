Import-Module SharePointPnPPowerShellOnline

$inputPath = "C:\Users\e82331\Desktop\filePages.xlsx"

$Excel = New-Object -ComObject Excel.Application
$workbook = $Excel.Workbooks.Open($inputPath)
$workSheet = $workbook.Sheets.Item(1)
$currentRowCount = $workSheet.Rows.CurrentRegion.Rows.Count

$leitourgia='/sites/communicationTest/images1/leitourgia.png'
$kliroseis='/sites/communicationTest/images1/kliroseis.png'
$alla='/sites/communicationTest/images1/alla.png'
$anthropino='/sites/communicationTest/images1/anthropino.png'
$proionta='/sites/communicationTest/images1/proionta.png'
$EKE='/sites/communicationTest/images1/EKE.png'
$protoi='/sites/communicationTest/images1/protoi.png'

#Connect
$UserName = "sindy@bousiou.onmicrosoft.com"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://bousiou.sharepoint.com/sites/communicationTest"

$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials

$documents = Get-PnPListItem -List "FilePages" -Query "<View><Query><OrderBy><FieldRef Name='FileLeafRef' Ascending='True' /></OrderBy></Query></View>" #gia na ta pairnei alfavitika
$i=2
foreach($document in $documents)
{
    $uniqueId = $document.FieldValues["GUID"]
    $url = Get-PnPProperty -ClientObject $document.File -Property ServerRelativeUrl
    $name = Get-PnPProperty -ClientObject $document.File -Property Name

    $json = '{"controlType":3,"id":"9d788a64-f678-4a51-8db5-7e3a7fd7f2d3","position":{"zoneIndex":1,"sectionIndex":1,"controlIndex":1,"layoutIndex":1},"webPartId":"b7dd04e1-19ce-4b24-9132-b60a1c2b910d","webPartData":{"id":"b7dd04e1-19ce-4b24-9132-b60a1c2b910d","instanceId":"9d788a64-f678-4a51-8db5-7e3a7fd7f2d3","title":"File viewer","description":"Display a document or other file on your page. You can show a file from most applications including Word, Excel, PowerPoint, Visio, PDF, 3D, and others","serverProcessedContent":{"htmlStrings":{},"searchablePlainTexts":{"title":""},"imageSources":{},"links":{"serverRelativeUrl":"'+$url+'","wopiurl":"https://bousiou.sharepoint.com/sites/communicationTest/_layouts/15/Doc.aspx?sourcedoc=%7B'+$uniqueId+'%7D&file='+$name+'&action=embedview&mobileredirect=true"}},"dataVersion":"1.4","properties":{"annotation":"","authorName":"","chartitem":"","endrange":"","excelSettingsType":"","file":"","listId":"","modifiedAt":"","photoUrl":"","rangeitem":"","siteId":"","startPage":1,"startrange":"","tableitem":"","uniqueId":"","wdallowinteractivity":true,"wdhidegridlines":true,"wdhideheaders":true,"webId":"","webAbsoluteUrl":""}},"emphasis":{},"reservedHeight":445,"reservedWidth":744}'
    
    $title = $workSheet.Cells($i,1).Text
    $pageName = $title.Substring(0,4)
    $pageTitle = $title.Substring(7,$title.Length-7)
    $category = $workSheet.Cells($i,2).Text
    $date = $workSheet.Cells($i,3).Text
    $finalDate = $date.Substring(0,$date.Length-6)

    $page = Add-PnPClientSidePage -Name $pageName -Publish -PromoteAs NewsArticle

    if($category -eq "Λειτουργία Ομίλου")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $leitourgia
        $page.SetCustomPageHeader($leitourgia)
    }
    elseif($category -eq "Κληρώσεις/Διαγωνισμοί")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $kliroseis
        $page.SetCustomPageHeader($kliroseis)
    }
    elseif($category -eq "Άλλα θέματα")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $alla
        $page.SetCustomPageHeader($alla)
    }
    elseif($category -eq "Πρώτοι Εμείς")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $protoi
        $page.SetCustomPageHeader($protoi)
    }
    elseif($category -eq "Ανθρώπινο Δυναμικό")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $anthropino
        $page.SetCustomPageHeader($anthropino)
    }
    elseif($category -eq "ΕΚΕ")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $EKE
        $page.SetCustomPageHeader($EKE)
    }
    elseif($category -eq "Προϊόντα και Υπηρεσίες")
    {
        Set-PnPClientSidePage -Identity $page -ThumbnailUrl $proionta
        $page.SetCustomPageHeader($proionta)
    }
    $page.Save()

    $fileViewer = Add-PnPClientSideWebPart -Page $pageName -DefaultWebPartType DocumentEmbed
    Set-PnPClientSideWebPart -Page $pageName -Identity $fileViewer.InstanceId -PropertiesJson $json
    Set-PnPClientSidePage -Identity $pageName -Title $pageTitle

    $id=Get-PnPProperty -ClientObject $page.PageListItem -Property Id

    $retval = Set-PnPListItem -List "SitePages" -Identity $id -Values @{"ArticleDate"=$finalDate; "Category"=$category}
    
    Set-PnPClientSidePage -Identity $pageName -Publish
    $i++
}