Import-Module SharePointPnPPowerShell2013
Import-Module 'C:\Users\e82331\Desktop\Git\browsertesting\PnP Scripts\WriteLogModule.psm1'

$UserName = "bank\e82331"
$pwd = "Y?Ugjxgar"

[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "http://mynbgportal:86/InternalCom"
Connect-PnPOnline -Url $Url -Credentials $Credentials

$LogPath = "C:\Users\e82331\Desktop\Log1.log"
$outputPath = "C:\Users\e82331\Desktop\filePages.xlsx"
$filesPath = "C:\Users\e82331\Desktop\communication"
$excel = New-Object -ComObject excel.application
$excel.visible = $True
$workbook = $excel.Workbooks.Add()
$worksheet= $workbook.Worksheets.Item(1)

$worksheet.Cells.Item(1,1)= 'Title'
$worksheet.Cells.Item(1,2)= 'Category'
$worksheet.Cells.Item(1,3) = 'Date'
$worksheet.Cells.Item(1,4) = 'Name'

$ListItems = Get-PnPListItem -List "Pages" -Query "<View><Query><Where><Gt><FieldRef Name='ArticleStartDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>2020-01-29T16:07:50Z</Value></Gt></Where></Query></View>"
$i=2
ForEach($Item in $ListItems)
{
    $title = Get-PnPProperty -ClientObject $Item.File -Property Title
    $category = $Item.FieldValues["CategoryInternalCom"]
    $name = Get-PnPProperty -ClientObject $Item.File -Property Name
    $date = $Item.FieldValues["ArticleStartDate"].AddDays(1)
    if(!($name.EndsWith(".aspx")))
    {
        $fileUrl = "/InternalCom/Pages/" + $name
        try
        {
            Get-PnPFile -Url $fileUrl -Path $filesPath -AsFile
        }
        catch
        {
            Write-Log -Message "Error on $title $_" -Path $LogPath -Level Error -ErrorAction Stop
        }
        $worksheet.Cells.Item($i,1)= $title
        $worksheet.Cells.Item($i,2)= $category
        $worksheet.Cells.Item($i,3)= $date
        $worksheet.Cells.Item($i,4)= $name
        $i++
    }
    elseif($Item.FieldValues.URL.Url -ne $null)
    {
        $url = $Item.FieldValues.URL.Url
        $serverRelativeUrl = $url.Substring(21,$url.Length-21)
        try
        {
            Get-PnPFile -Url $serverRelativeUrl -Path $filesPath -AsFile -ErrorAction Stop
        }
        catch
        {
            Write-Log -Message "Error on $title $_" -Path $LogPath -Level Error
        }
        $worksheet.Cells.Item($i,1)= $title
        $worksheet.Cells.Item($i,2)= $category
        $worksheet.Cells.Item($i,3)= $date

        $splits = $url.Split('/')
        $worksheet.Cells.Item($i,4)= $splits[$splits.Length-1]
        $i++
    }
}

$workbook.SaveAs($outputPath)
$excel.Quit()