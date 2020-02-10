Import-Module SharePointPnPPowerShellOnline

$leitourgia='/sites/communicationTest/images1/leitourgia.png'
#Connect
$UserName = "sindy@bousiou.onmicrosoft.com"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://bousiou.sharepoint.com/sites/communicationTest"
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials

$pageName = "Repost5"
$page = Add-PnPClientSidePage -Name $pageName -Publish -PromoteAs NewsArticle -LayoutType RepostPage

$name = "0108_Παρουσίαση Επιχειρησιακού Σχεδίου 2007-09 (ppt) απ.ppt"
$query = "<View><Query><Where><Eq><FieldRef Name='FileLeafRef' /><Value Type='File'>"+$name+"</Value></Eq></Where></Query></View>"
$document = Get-PnPListItem -List "FilePages" -Query $query
$uniqueId = $document.FieldValues["GUID"]

$id=Get-PnPProperty -ClientObject $page.PageListItem -Property Id
Set-PnPClientSidePage -Identity $page -ThumbnailUrl $leitourgia
$retval = Set-PnPListItem -List "SitePages" -Identity $id -Values @{"_OriginalSourceUrl"="https://bousiou.sharepoint.com/sites/communicationTest/FilePages/Forms/AllItems.aspx?id=/sites/communicationTest/FilePages/"+$name+"&parent=/sites/communicationTest/FilePages";"ArticleDate"="5/20/2020"; "Category"="Λειτουργία Ομίλου";"Description"="Description"}
Set-PnPClientSidePage -Identity $pageName -Publish