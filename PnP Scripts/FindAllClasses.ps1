Import-Module SharePointPnPPowerShellOnline

$inputPath = "C:\Users\KyriakiBousiou\Desktop\htmls.xlsx"
$Excel = New-Object -ComObject Excel.Application
$workbook = $Excel.Workbooks.Open($inputPath)
$workSheet = $workbook.Sheets.Item(1)
$currentRowCount = $workSheet.Rows.CurrentRegion.Rows.Count

$outputPath = "C:\Users\KyriakiBousiou\Desktop\classes.txt"

$classes =@()
for($i=2;$i -le $currentRowCount;$i++) {  
    $html = $workSheet.Cells($i,2).Text
    $j=0
    while(1)
    {
        $temp = $html.IndexOf("class=",$j)
        if($temp -eq -1){
            break
        }
        $j=$temp+1
        $start=$j+6
        $end=$html.IndexOf('"',$j+6) - $start
        $html.Substring($start,$end) | Out-File -FilePath $outputPath -Append
    }
}