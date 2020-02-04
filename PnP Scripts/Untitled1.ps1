Import-Module SharePointPnPPowerShellOnline
#initializations
$Url = "https://bousiou.sharepoint.com/sites/SiteTemplate"
$targetPath = "C:\Users\e82331\Desktop\Template\"

#connect
$UserName = "sindy@bousiou.onmicrosoft.com"
$PassWord = "Y?Ugjxgar"
[SecureString]$SecurePassWord = ConvertTo-SecureString $PassWord -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePassWord)
Connect-PnPOnline -Url $Url -Credentials $Credentials

#create the template
$templateUrl = "C:\Users\e82331\Desktop\Template\TeamSiteTemplate.xml"
Get-PnPProvisioningTemplate -Out $templateUrl -Force -IncludeAllTermGroups -PersistComposedLookFiles -IncludeAllClientSidePages -PersistPublishingFiles -IncludeNativePublishingFiles -Handlers Navigation, Lists, PageContents, Pages, Files

#get all document libraries
$docLibs = Get-PNPList | Where-Object{$_.BaseTemplate -eq 101}

#process each document library
foreach($doc in $docLibs)
{
    $docSplits = $null
    $docSplits = ($doc.DefaultViewUrl).Split("/")  #build the relative url to the document library
    $docUrl = "/sites/SiteTemplate/" + $docSplits[3]
    ProcessFolder $docUrl ($targetPath+$docSplits[3])

    $tempfolders = Get-PnPProperty -ClientObject $doc.RootFolder -Property Folders
    ProcessSubFolders $tempfolders ($targetPath+$docSplits[3])
}

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
       
        Get-PnPFile -ServerRelativeUrl $file.ServerRelativeUrl -Path $targetPath -FileName $file.Name -AsFile -Force
        Add-PnPFileToProvisioningTemplate -Path $templateUrl -Source ($targetPath + "\" + $file.Name) -Folder $doc.Title -FileLevel Published
    }
}