Import-Module SharePointPnPPowerShellOnline

#Connect
$UserName = "sindy@bousiou.onmicrosoft.com"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://bousiou.sharepoint.com/sites/communicationTest"

Connect-PnPOnline -Url $Url -Credentials $Credentials

$inputPath = "C:\Users\e82331\Desktop\dates.xlsx"

$Excel = New-Object -ComObject Excel.Application
$workbook = $Excel.Workbooks.Open($inputPath)

$workSheet = $workbook.Sheets.Item(1)
$currentRowCount = $workSheet.Rows.CurrentRegion.Rows.Count

for($i=2187;$i -le $currentRowCount;$i++) {  
    $i
    $title = $workSheet.Cells($i,1).Text
    $pageName = $title.Substring(0,4)
    $date = $workSheet.Cells($i,2).Text
    $date
    $finalDate = $date.Substring(0,$date.Length-6)
    $finalDate

    $query = "<View><Query><Where><Eq><FieldRef Name='FileLeafRef' /><Value Type='File'>"+$pageName+".aspx</Value></Eq></Where></Query></View>"
    $page = Get-PnPListItem -List SitePages -Query $query
    if($page -ne $null)
    {
        $page
        Set-PnPListItem -List "SitePages" -Identity $page.Id -Values @{"ArticleDate"=[DateTime]::ParseExact($finalDate, 'M/d/yyyy',[CultureInfo]::InvariantCulture)}
    }
   
}