Import-Module SharePointPnPPowerShellOnline

$UserName = ""
$pwd = ""
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://devnbg.sharepoint.com"
Connect-PnPOnline -Url $Url -Credentials $Credentials

$inputPath = "C:\Users\IoannisTzanos\Desktop\NewsMigration\html.xlsx"
$Excel = New-Object -ComObject Excel.Application
$workbook = $Excel.Workbooks.Open($inputPath)
$workSheet = $workbook.Sheets.Item(1)
$currentRowCount = $workSheet.Rows.CurrentRegion.Rows.Count

for($i=2;$i -le $currentRowCount;$i++) {  
    $pageTitle = $workSheet.Cells($i,1).Text
    $page = Add-PnPClientSidePage -Name $pageTitle -Publish
    Add-PnPClientSideText -Page $pageTitle -Text $workSheet.Cells($i,2).Text
    Set-PnPClientSidePage -Identity $pageTitle -Publish
}