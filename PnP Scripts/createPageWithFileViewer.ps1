Import-Module SharePointPnPPowerShellOnline

#Connect
$UserName = "sindy@bousiou.onmicrosoft.com"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://bousiou.sharepoint.com/sites/communicationTest"

Connect-PnPOnline -Url $Url -Credentials $Credentials

$documents = Get-PnPListItem -List "Shared Documents"
foreach($document in $documents)
{
    $uniqueId = Get-PnPProperty -ClientObject $document.File -Property UniqueId
    $url = Get-PnPProperty -ClientObject $document.File -Property ServerRelativeUrl
    $name = Get-PnPProperty -ClientObject $document.File -Property Name

    $json = '{"controlType":3,"id":"9d788a64-f678-4a51-8db5-7e3a7fd7f2d3","position":{"zoneIndex":1,"sectionIndex":1,"controlIndex":1,"layoutIndex":1},"webPartId":"b7dd04e1-19ce-4b24-9132-b60a1c2b910d","webPartData":{"id":"b7dd04e1-19ce-4b24-9132-b60a1c2b910d","instanceId":"9d788a64-f678-4a51-8db5-7e3a7fd7f2d3","title":"File viewer","description":"Display a document or other file on your page. You can show a file from most applications including Word, Excel, PowerPoint, Visio, PDF, 3D, and others","serverProcessedContent":{"htmlStrings":{},"searchablePlainTexts":{"title":""},"imageSources":{},"links":{"serverRelativeUrl":"'+$url+'","wopiurl":"https://bousiou.sharepoint.com/sites/communicationTest/_layouts/15/Doc.aspx?sourcedoc=%7B'+$uniqueId+'%7D&file='+$name+'&action=embedview&mobileredirect=true"}},"dataVersion":"1.4","properties":{"annotation":"","authorName":"","chartitem":"","endrange":"","excelSettingsType":"","file":"","listId":"","modifiedAt":"","photoUrl":"","rangeitem":"","siteId":"","startPage":1,"startrange":"","tableitem":"","uniqueId":"","wdallowinteractivity":true,"wdhidegridlines":true,"wdhideheaders":true,"webId":"","webAbsoluteUrl":""}},"emphasis":{},"reservedHeight":445,"reservedWidth":744}'
    
    $pageName = $name.Split('.')[0]
    $page = Add-PnPClientSidePage -Name $pageName -Publish -PromoteAs NewsArticle
    $fileViewer = Add-PnPClientSideWebPart -Page $pageName -DefaultWebPartType DocumentEmbed
    Set-PnPClientSideWebPart -Page $pageName -Identity $fileViewer.InstanceId -PropertiesJson $json
    Set-PnPClientSidePage -Identity $pageName -Publish
}