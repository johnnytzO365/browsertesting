Import-Module SharePointPnPPowerShell2013

$UserName = "bank\e82331"
$pwd = "Y?Ugjxgar"

[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)

$Url = "http://mynbgportal:86/InternalCom"
Connect-PnPOnline -Url $Url -Credentials $Credentials

$outputPath = "C:\Users\e82331\Desktop\pageUrls.xlsx"
$excel = New-Object -ComObject excel.application
$excel.visible = $True
$workbook = $excel.Workbooks.Add()
$worksheet= $workbook.Worksheets.Item(1)

$worksheet.Cells.Item(1,1)= 'Title'
$worksheet.Cells.Item(1,2)= 'Url'
$worksheet.Cells.Item(1,3)= 'Category'
$worksheet.Cells.Item(1,4) = 'Date'
$worksheet.Cells.Item(1,5)= 'HTML'

$ListItems = Get-PnPListItem -List "Pages"
$i=2
ForEach($Item in $ListItems)
{
    $name = Get-PnPProperty -ClientObject $Item.File -Property Name
    if($name.EndsWith(".aspx"))
    {
        if($Item.FieldValues.URL.Url -eq $null)
        {
            $title = Get-PnPProperty -ClientObject $Item.File -Property Title
            $category = $Item.FieldValues["CategoryInternalCom"]
            $date = $Item.FieldValues["ArticleStartDate"]

            $pageUrl = "http://mynbgportal:86/InternalCom/Pages/" + $name
            $worksheet.Cells.Item($i,1)= $title
            $worksheet.Cells.Item($i,2)= $pageUrl
            $worksheet.Cells.Item($i,3)= $category
            $worksheet.Cells.Item($i,4)= $date
            $i++
        }
    }
}

$workbook.SaveAs($outputPath)
$excel.Quit()