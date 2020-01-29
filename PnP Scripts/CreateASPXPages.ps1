<#
.SYNOPSIS
    Add a page in a Sharepoint online site and add HTML content to it.

.DESCRIPTION
    Connect to the SPO site and read an xlsx file, then create a page, add the html content from the input file and publish it.

.INPUTS
    An xlsx file with two columns: one for the title of the page and one for the html content of each page. The first row is for the names of the columns (it is ignored).
    It can also be added as an argument a number indicating the line of the xlsx file from which the program will start (in case the script crashes). If a number is not given, the default is 2.

.OUTPUT
    Published SPO pages.
    A log file stored in C:\Windows\Temp\PnPScriptLogFile.log

.NOTES
    Author: Ioannis Tzanos - Kyriaki Bousiou
    Creation Date: 12/2019
    Purpose: Transfer SP2013 pages to SPO with format
#>

#-----------------------------------[Initialisations]----------------------------------------------------------
Import-Module SharePointPnPPowerShellOnline
Import-Module 'C:\Users\e82331\Desktop\Git\browsertesting\PnP Scripts\WriteLogModule.psm1'
$ErrorActionPreference = "SilentlyContinue"

#----------------------------------[Declarations]--------------------------------------------------------------
$inputPath = "C:\Users\KyriakiBousiou\Desktop\CommunicationPages1.xlsx"
$LogPath = "C:\Users\KyriakiBousiou\Desktop\PnP Powershell Scripts\Log.log"

$leitourgia='https://bousiou.sharepoint.com/sites/communicationTest/images1/leitourgia.png'
$kliroseis='/sites/communicationTest/images1/kliroseis.png'
$alla='/sites/communicationTest/images1/alla.png'
$anthropino='/sites/communicationTest/images1/anthropino.png'
$proionta='/sites/communicationTest/images1/proionta.png'
$EKE='/sites/communicationTest/images1/EKE.png'
$protoi='/sites/communicationTest/images1/protoi.png'

#-------------------------------------[Functions]-------------------------------------------------------------

#--------------------------------------[Execution]------------------------------------------------------------
#Connect
$UserName = "sindy@bousiou.onmicrosoft.com"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://bousiou.sharepoint.com/sites/communicationTest"

try{
    Connect-PnPOnline -Url $Url -Credentials $Credentials
    Write-Host "Connected"
}
catch{
    Write-Host "Connection Problem. Check the Url and your credentials and try again"
    Write-Log -Message "Couldn't connect: $_" -Path $LogPath -Level Error
    Exit(1)
}

#Open input file
try{
    $Excel = New-Object -ComObject Excel.Application
    $workbook = $Excel.Workbooks.Open($inputPath)
}
catch{
    Write-Host "Problem with xlsx file. Check the path and the type of file and try again."
    Write-Log -Message "Couldn't open input xlsx file: $_" -Path $LogPath -Level Error
    Exit(2)
}
$workSheet = $workbook.Sheets.Item(1)
$currentRowCount = $workSheet.Rows.CurrentRegion.Rows.Count

#loop to each line
if($args[0] -eq $null){
    $startline =2
}
else{
    $startline=$args[0]
}
for($i=$startline;$i -le $currentRowCount;$i++) {  
    $title = $workSheet.Cells($i,1).Text
    $pageName = $title.Substring(0,4)
    $pageTitle = $title.Substring(7,$title.Length-7)
    $category = $workSheet.Cells($i,3).Text
    $date = $workSheet.Cells($i,4).Text
    $finalDate = $date.Substring(0,$date.Length-6)
    $html = $workSheet.Cells($i,5).Value2
    $newhtml = $html.Replace("/InternalCom/","/sites/communicationtopic/")

    try{
        $page = Add-PnPClientSidePage -Name $pageName -Publish -PromoteAs NewsArticle
        Add-PnPClientSideText -Page $pageName -Text $newhtml
        Set-PnPClientSidePage -Identity $pageName -Title $pageTitle
        Set-PnPListItem -List "SitePages" -Identity $page.PageListItem.Id -Values @{"Category"=$category}
        Set-PnPListItem -List "SitePages" -Identity $page.PageListItem.Id -Values @{"ArticleDate"=[DateTime]::ParseExact($finalDate, 'M/d/yyyy',[CultureInfo]::InvariantCulture)}
        
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
        Set-PnPClientSidePage -Identity $pageName -Publish
        Write-Log -Message "$title : Completed successfully" -Path $LogPath -Level Info
    }
    catch{
        Write-Log -Message "$title : $_" -Path $LogPath -Level Warn
        continue
    }
}