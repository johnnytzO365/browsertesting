Import-Module SharePointPnPPowerShell2013

$UserName = "bank\e82331"
$pwd = "p@ssw0rd"

[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)

$Url = "http://mynbgportal:86/InternalCom"
Connect-PnPOnline -Url $Url -Credentials $Credentials

$outputPath = "C:\Users\e82337\Desktop\pageUrls.txt"
$excel = New-Object -ComObject excel.application
$excel.visible = $True
$workbook = $excel.Workbooks.Add()
$worksheet= $workbook.Worksheets.Item(1)

$worksheet.Cells.Item(1,1)= 'Title'
$worksheet.Cells.Item(1,2)= 'Url'
$worksheet.Cells.Item(1,3)= 'Category'
$worksheet.Cells.Item(1,4)= 'HTML'

$ListItems = Get-PnPListItem -List "Pages"
$i=2
ForEach($Item in $ListItems)
{
    $title = Get-PnPProperty -ClientObject $Item.File -Property Title
    $category = $Item.FieldValues["CategoryInternalCom"]
    $name = Get-PnPProperty -ClientObject $Item.File -Property Name
    if($name.EndsWith(".aspx"))
    {
        $pageUrl = "http://mynbgportal:86/InternalCom/Pages/" + $name
        $worksheet.Cells.Item($i,1)= $title
        $worksheet.Cells.Item($i,2)= $pageUrl
        $worksheet.Cells.Item($i,3)= $category
        $i++
    }
}

$workbook.SaveAs($outputPath)
$excel.Quit()