Import-Module SharePointPnPPowerShell2013

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
    }
}

#initializations
$Url = "http://v000080043:9993/sites/sp_team_nbg/"
$targetPath = "C:\Users\e82331\Desktop\TeamSiteTemplate4\"

Remove-Item $targetPath -Recurse -ErrorAction Ignore

#connect
$UserName = "e82331"
$PassWord = "p@ssw0rd"
[SecureString]$SecurePassWord = ConvertTo-SecureString $PassWord -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePassWord)
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials

#get all document libraries
$docLibs = Get-PNPList | Where-Object{$_.BaseTemplate -eq 101}

#process each document library
foreach($doc in $docLibs)
{
    $title = Get-PnPProperty -ClientObject $doc -Property Title
    Write-Host "Processing $title"
    $docSplits = $null
    $docSplits = ($doc.DefaultViewUrl).Split("/")  #build the relative url to the document library
    $docUrl = "/sites/sp_team_nbg/" + $docSplits[3]
    ProcessFolder $docUrl ($targetPath+$docSplits[3])

    $tempfolders = Get-PnPProperty -ClientObject $doc.RootFolder -Property Folders
    ProcessSubFolders $tempfolders ($targetPath+$docSplits[3])
}