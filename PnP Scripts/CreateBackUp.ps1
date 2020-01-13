#initializations
$localFolder = "C:\Users\e82331\Desktop\TeamSiteTemplate2\"
$Url = "https://bousiou.sharepoint.com/sites/TeamSiteBackup"

#connect
$UserName = "sindy@bousiou.onmicrosoft.com"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials

$folders = Get-ChildItem -Path $localFolder -Force
foreach($folder in $folders)
{
    New-PnPList -Title $folder.Name -Template DocumentLibrary -OnQuickLaunch
    $path = $Url + $folder.Name
    ProcessSubFolders($folder.FullName,$path)
}

function ProcessSubFolders($FullName,$path)
{
    $items = Get-ChildItem -Path $localFolder -Force
    if($items -ne $null)
    {
        foreach($item in $items)
        {
            if($item -is [System.IO.DirectoryInfo])
            {
               Add-PnPFolder -Name $item.Name -Folder $path
               $path += $item.Name
               ProcessSubFolders($item.FullName,$path)
            }
            else
            {

            }
        }
    }
}