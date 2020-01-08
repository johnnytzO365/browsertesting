Import-Module SharePointPnPPowerShell2013

#connect
$UserName = "e82331"
$pwd = "p@ssw0rd"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "http://v000080043:9993/sites/sp_team_nbg"
Connect-PnPOnline -Url $Url -Credentials $Credentials

#upload
$localPath = "C:\Users\e82331\Downloads\CommunicationPages.xlsx"
$name = Get-Date -Format "yyyy.MM.dd"
$splib = "/CookieCheckerResults/"+$name
Add-PnPFolder -Name $name -Folder "CookieCheckerResults"
Add-PnPFile -Path $localPath -Folder $splib
$message = "See CookieChecker Results in " + $Url+$splib

#delete old files
$folder = Get-PnPFolderItem -FolderSiteRelativeUrl "/CookieCheckerResults"
if($folder.Count -ge 15)
{
    if($folder[0].Name -ne "Forms")
    {
        Remove-PnPFolder -Name $folder[0].Name -Folder "/CookieCheckerResults" -Force
        $message += "\n"+ $folder[0].Name+" removed!"
    }
    else
    {
        Remove-PnPFolder -Name $folder[1].Name -Folder "/CookieCheckerResults" -Force
        $message += "\n"+ $folder[1].Name+" removed!"
    }
}

#send Email
$UserName = "e82331@nbg.gr"
$pwd = "p@ssw0rd"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://groupnbg.sharepoint.com/"
Connect-PnPOnline -Url $Url -Credentials $Credentials

Send-PnPMail -To e82331@nbg.gr,e82337@nbg.gr -Subject "CookieChecker Results" -Body $message