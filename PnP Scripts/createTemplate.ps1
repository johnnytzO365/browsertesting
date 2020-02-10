function ProcessSubFolders($folders, $targetPath) {
    foreach ($folder in $folders) {
        $tempurls = Get-PnPProperty -ClientObject $folder -Property ServerRelativeUrl 
        #Avoid Forms folders
        if ($folder.Name -ne "Forms")
        {   
            $targetFolder = $targetPath +"\"+ $folder.Name;
            ProcessFolder $folder.ServerRelativeUrl.Substring($web.ServerRelativeUrl.Length) $targetFolder 
            $tempfolders = Get-PnPProperty -ClientObject $folder -Property Folders
            ProcessSubFolders $tempfolders $targetFolder
        }
    }
}

function ProcessFolder($docUrl,$targetPath)
{
    $folder = Get-PnPFolder -RelativeUrl $docUrl
    $tempfiles = Get-PnPProperty -ClientObject $folder -Property Files

    if (!(Test-Path -path $targetPath )) 
    {
        $dest = New-Item $targetPath -type directory 
    }

    $total = $folder.Files.Count
    For ($i = 0; $i -lt $total; $i++) 
    {
        $file = $folder.Files[$i]

        $url = $doc.RootFolder.ServerRelativeUrl
        $splits = $url.Split('/')
        $finalUrl = $splits[$splits.Length-1]
       
        Get-PnPFile -ServerRelativeUrl $file.ServerRelativeUrl -Path $targetPath -FileName $file.Name -AsFile -Force
        Add-PnPFileToProvisioningTemplate -Path $templateUrl -Source ($targetPath + "\" + $file.Name) -Folder $finalUrl -FileLevel Published
    }
}

#---------------------------------------------------------------------------------------------------------------------------------------------#

Import-Module SharePointPnPPowerShellOnline
Set-PnPTraceLog -On -Level:Debug

#initializations
$Url = "https://groupnbg.sharepoint.com/sites/TranformationQA"
#$targetPath = "C:\Users\e82331\Desktop\TransformationTemplate3\"

#connect
$UserName = "e82331@nbg.gr"
$PassWord = "Y?Ugjxgar"
[SecureString]$SecurePassWord = ConvertTo-SecureString $PassWord -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePassWord)
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials

#create the template
$templateUrl = "C:\Users\e82331\Desktop\TransformationTemplate3\Template.xml"
Get-PnPProvisioningTemplate -Out $templateUrl -Handlers Pages, PageContents, Lists, Navigation, WebSettings -ListsToExtract "Site Pages","testList" -IncludeAllClientSidePages -PersistBrandingFiles
Add-PnPDataRowsToProvisioningTemplate -Path $templateUrl -List "testList"
Add-PnPDataRowsToProvisioningTemplate -Path $templateUrl -List "Site Pages" -Fields "Category","IsNews"

<#
#get all document libraries
$docLibs = Get-PNPList | Where-Object{$_.BaseTemplate -eq 101}

#process each document library
foreach($doc in $docLibs)
{

    $docSplits = $null
    $docSplits = ($doc.DefaultViewUrl).Split("/")  #build the relative url to the document library
    $docUrl = "/sites/TranformationQA/" + $docSplits[3]
    ProcessFolder $docUrl ($targetPath+$docSplits[3])

    $tempfolders = Get-PnPProperty -ClientObject $doc.RootFolder -Property Folders
    ProcessSubFolders $tempfolders ($targetPath+$docSplits[3])
}#>