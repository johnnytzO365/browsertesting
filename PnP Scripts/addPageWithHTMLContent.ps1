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
$ErrorActionPreference="SilentlyContinue"

#----------------------------------[Declarations]--------------------------------------------------------------
$inputPath = "C:\Users\KyriakiBousiou\Desktop\html.xlsx"
$LogPath = "C:\Users\KyriakiBousiou\Desktop"
$LogName = "PnPSciptLogFile.log"
$LogFile = Join-Path -Path $LogPath -ChildPath $LogName

#-------------------------------------[Functions]-------------------------------------------------------------

#--------------------------------------[Execution]------------------------------------------------------------
#Connect
$UserName = "sindy@bousiou.onmicrosoft.com"
$pwd = "Gld9q_31"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://bousiou.sharepoint.com/sites/testSite"

Log-Start -LogPath $LogPath -LogName $LogName

try{
    Connect-PnPOnline -Url $Url -Credentials $Credentials
}
catch{
    Write-Host "Connection Problem. Check the Url and your credentials and try again"
    #Log-Error -LogPath $LogFile -ErrorDesc $_.Exception -ExitGracefully $True
    Exit(1)
}

#Open input file
$Excel = New-Object -ComObject Excel.Application
$workbook = $Excel.Workbooks.Open($inputPath)
$workSheet = $workbook.Sheets.Item(1)
$currentRowCount = $workSheet.Rows.CurrentRegion.Rows.Count

#loop to each line
for($i=2;$i -le $currentRowCount;$i++) {  
    $pageTitle = $workSheet.Cells($i,1).Text

    try{
        $page = Add-PnPClientSidePage -Name $pageTitle -Publish
        Add-PnPClientSideText -Page $pageTitle -Text $workSheet.Cells($i,2).Text
        Set-PnPClientSidePage -Identity $pageTitle -Publish
        $message = "\n"+$pageTitle+": Completed succesfully\n"
        #Log-Write -LogPath $LogFile -LineValue $message
    }
    catch{
        Write-Host "Problem with page:" $pageTitle
        #Log-Write -LogPath $LogFile -LineValue $pageTitle
        #Log-Error -LogPath $LogFile -ErrorDesc $_.Exception
        continue
    }
}

Log-Finish -LogPath $LogFile