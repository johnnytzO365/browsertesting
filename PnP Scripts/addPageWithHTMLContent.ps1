Import-Module SharePointPnPPowerShellOnline

$UserName = "e82331@nbg.gr"
$pwd = "123sindy^"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://groupnbg.sharepoint.com/sites/TranformationQA"
Connect-PnPOnline -Url $Url -Credentials $Credentials

$inputUrl = "C:\Users\KyriakiBousiou\Desktop\html.csv"
$file = Import-Csv $inputUrl

foreach($record in $file) {  
    $page = Add-PnPClientSidePage -Name $record.Title -Publish
    Add-PnPClientSideText -Page $record.Title -Text $record.html
    #$pageUrl = "/sites/TranformationQA/SitePages/"+$record.Title+".aspx"
    #Set-PnPFileCheckedIn -Url $pageUrl -CheckinType MajorCheckIn
    Set-PnPClientSidePage -Identity $record.Title -Publish
}