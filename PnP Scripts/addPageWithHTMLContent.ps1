<#
.SYNOPSIS
    Add a page in a Sharepoint online site and add HTML content to it.

.DESCRIPTION
    Connect to the SPO site and read an xlsx file, then create a page, add the html content from the input file and publish it.

.INPUTS
    An xlsx file with two columns: one for the title of the page and one for the html content of each page. The first row is for the names of the columns (it is ignored).

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
Import-Module 'C:\Users\KyriakiBousiou\Desktop\PnP Powershell Scripts\WriteLogModule.psm1'
$ErrorActionPreference = "SilentlyContinue"

#----------------------------------[Declarations]--------------------------------------------------------------
$inputPath = "C:\Users\KyriakiBousiou\Desktop\htmls.xlsx"
$LogPath = "C:\Users\KyriakiBousiou\Desktop\Log.log"

#-------------------------------------[Functions]-------------------------------------------------------------

#--------------------------------------[Execution]------------------------------------------------------------
#Connect
$UserName = "sindy@bousiou.onmicrosoft.com"
$pwd = "Gld9q_31"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://bousiou.sharepoint.com/sites/testSite"

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
    $pageTitle = $workSheet.Cells($i,1).Text
    $html = $workSheet.Cells($i,2).Text
    $newhtml = $html.Replace("InternalCom/","sites/testSite/")

    try{
        $page = Add-PnPClientSidePage -Name $pageTitle -Publish
        Add-PnPClientSideText -Page $pageTitle -Text $newhtml
        Set-PnPClientSidePage -Identity $pageTitle -Publish
        $message = $pageTitle+": Completed succesfully"
        Write-Log -Message "$pageTitle : Completed successfully" -Path $LogPath -Level Info
    }
    catch{
        Write-Host "Problem with page:" $pageTitle
        Write-Log -Message "Problem with $pageTitle : $_" -Path $LogPath -Level Warn
        continue
    }
}