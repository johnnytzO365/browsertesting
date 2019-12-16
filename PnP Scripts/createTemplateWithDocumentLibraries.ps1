﻿Import-Module SharePointPnPPowerShellOnline

#connect
$UserName = "e82331@nbg.gr"
$PassWord = "Y?Ugjxgar"
[SecureString]$SecurePassWord = ConvertTo-SecureString $PassWord -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePassWord)
$Url = "https://groupnbg.sharepoint.com/sites/Transformation"
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials

#create the template
$templateUrl = "C:\Users\KyriakiBousiou\Desktop\TransformationTemplate\TransformationTemplate.xml"
Get-PnPProvisioningTemplate -Out $templateUrl -Force -PersistBrandingFiles -PersistPublishingFiles -IncludeNativePublishingFiles -Handlers Navigation, Lists,PageContents, Pages, Files

$targetPath = "C:\Users\KyriakiBousiou\Desktop\TransformationTemplate\"

#get all document libraries
$docLibs = Get-PNPList | Where-Object{$_.BaseTemplate -eq 101}

#process each document library
foreach($doc in $docLibs)
{
    #if($doc.Title -ne "Site Assets") #don't know why yet
    #{
        $docSplits = $null
        $docSplits = ($doc.DocumentTemplateUrl).Split("/")  #build the relative url to the document library
        $docUrl = "/sites/Transformation/" + $docSplits[3]  #couldn't find a better way
        ProcessFolder $docUrl ($targetPath+$docSplits[3])

        $tempfolders = Get-PnPProperty -ClientObject $doc.RootFolder -Property Folders
        ProcessSubFolders $tempfolders ($targetPath+$docSplits[3])
    #}
}

function ProcessSubFolders($folders, $targetPath) {
    foreach ($folder in $folders) {
        $tempurls = Get-PnPProperty -ClientObject $folder -Property ServerRelativeUrl 
        #Avoid Forms folders
        if ($folder.Name -ne "Forms") #don't know why yet
        {   
            $targetFolder = $targetPath +"\"+ $folder.Name;
            ProcessFolder $folder.ServerRelativeUrl.Substring($web.ServerRelativeUrl.Length) $targetFolder #check again first argument 
            $tempfolders = Get-PnPProperty -ClientObject $folder -Property Folders
            ProcessSubFolders $tempfolders $targetFolder
        }
    }
}

function ProcessFolder($docUrl,$targetPath)
{
    Write-Output "This is what you need to know"
    Write-Output $docUrl $targetPath
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
         
        Get-PnPFile -ServerRelativeUrl $file.ServerRelativeUrl -Path $targetPath -FileName $file.Name -AsFile -Force
 
        Add-PnPFileToProvisioningTemplate -Path $templateUrl -Source ($targetPath + "\" + $file.Name) -Folder $doc.Title -FileLevel Published
    }
}