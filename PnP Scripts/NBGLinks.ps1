#Get NBG Links and Write All Non Sharepoint Pages inside NBG Domain
<#
.SYNOPSIS
    Iterate through all NBG Public Site subsites, find every link in every page and write it to a file.

.DESCRIPTION
    An IE window logged in to https://authqa.nbg.gr must be open during the execution
    Connect to the SP2013 site, (root or other), find all pages in this subsite and for every page find and write every link. Then, go to the original's site subsites and do the same.
    Divide the links in three categories, internal Sharepoint, internal non-Sharepoint, external. Try to write in the file only internal non-sharepoint links (might have some externals as well)
    Sort and delete duplicates from output file.
    Because of the large number of subsites, the script takes some hours to complete. That's why there are the following parameters for the user to be able to split the subsites.

.PARAMETERS
    -OutputFile: Mandatory, string, the location where the output file will be stored. If this is not the first time the user runs the program, this file will be appended with the new results.
    -RootSite: String, it's the subsite from which the program will start and will take it as root site, the program will run for this site and all its subsites.
    If the root site parameter is not given, the user can determine how many subsites the program will run. A list of all subsites (from the root down) will be taken, so the following parameters define:
    -Start: Int, the index of the subsite list from which the program will start.
    -End: Int, the index of the subsite list with which the program will end.

.EXAMPLE
    ./NBGLinks.ps1 -> NOT ALLOWED, user must give either the RootSite or one of Start-End parameters.
    ./NBGLinks.ps1 -RootSite https://authqa.nbg.gr/el
    ./NBGLinks.ps1 -Start 101 -End 200
    ./NBGLinks.ps1 -Start 101 -> End will be the end of the subsite list by default
    ./NBGLinks.ps1 -End 200 -> Start will be 1 by default
    ./NBGLinks.ps1 -RootSite https://authqa.nbg.gr/el -Start 101 -End 200 -> NOT ALLOWED, user can't give both RootSite and Start-End parameters.

.OUTPUT
    A txt file with the links stored in the path given by the user
    A log file stored in C:\Windows\Temp\PnPScriptLogFile.log

.NOTES
    Author: Kyriaki Bousiou
    Creation Date: 1/2020
    Purpose: Find all links in NBG Public Site to check Cookies compliance.
#>
#-------------------------------------[Functions]-------------------------------------------------------------


#-----------------------------------[Initialisations]----------------------------------------------------------
[CmdletBinding()]
param(
    [Parameter()]
    [string]$RootSite,
        
    [Parameter()]
    [int]$Start,

    [Parameter()]
    [int]$End,

    [Parameter(Mandatory)]
    [string]$OutputFile
)
if(($RootSite -eq "")-and($Start -eq "")-and($End -eq ""))
{
    Write-Warning "User must give either the RootSite or Start-End parameters.`r`n-RootSite: The URL of the site that will be used as root (the program will run from this site down).`r`nStart: The index of the subsite you want the program to start from.`r`nEnd: The index of the subsite you want the program to end with."
    Exit
}
elseif(($RootSite -ne "")-and(($Start -ne "")-or($End -ne "")))
{
    Write-Warning "User must give either the RootSite or Start-End parameters.`r`n-RootSite: The URL of the site that will be used as root (the program will run from this site down).`r`nStart: The index of the subsite you want the program to start from.`r`nEnd: The index of the subsite you want the program to end with."
    Exit
}
else{
    if($RootSite -eq "")
    {
        $RootSite="https://authqa.nbg.gr/"
    }
    if($Start -eq "")
    {
        $Start=0
    }
    if($End -eq "")
    {
        Connect-PnPOnline -Url $RootSite -UseWebLogin #you need to open IE, login to https://authqa.nbg.gr/ and leave the window open
        $webs = Get-PnPSubWebs -Recurse
        $End=$webs.Count
    }
}

if($webs -eq $null)
{
    Connect-PnPOnline -Url $RootSite -UseWebLogin #you need to open IE, login to https://authqa.nbg.gr/ and leave the window open
    $webs = Get-PnPSubWebs -Recurse
}
Import-Module SharePointPnPPowerShell2013
Import-Module 'C:\Users\e82331\Desktop\Git\browsertesting\PnP Scripts\WriteLogModule.psm1'
$ErrorActionPreference = "SilentlyContinue"
#----------------------------------[Declarations]--------------------------------------------------------------
$LogPath = "C:\Users\e82331\Temp\Log.log"

#--------------------------------------[Execution]------------------------------------------------------------
# Iterate all webs
Connect-PnPOnline -Url $RootSite -UseWebLogin #you need to open IE, login to https://authqa.nbg.gr/ and leave the window open
$authWebUrl = (Get-PnPWeb).Url                            #you need to refresh the window every few minutes
$webUrl = $authWebUrl.Replace("https://authqa", "https://qa")

$tempOutputFile=($OutputFile.Split('.'))[0]+"duplicates.txt"
$rootSiteURL = "https://qa.nbg.gr"
$hrefs=""

#root
if($start -eq 0)
{
    $start=1
    Write-Host "Processing web: " $webUrl
    Write-Log -Message "Processing web: $webUrl" -Path $LogPath -Level Info

    $pages = Get-PnPListItem -List "Pages" -Query "<View><Query><Where><Eq><FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>Approved</Value></Eq></Where></Query></View>"
    foreach($page in $pages)
    {
        $pageUrl = $rootSiteURL + $page.FieldValues["FileRef"]
        try
        {
            $pageHTML = Invoke-WebRequest -Uri $pageUrl
        }
        catch
        {
            Write-Log -Message "Error on page $pageURL $_" -Path $LogPath -Level Error
            continue
        }
        $links = $pageHTML.Links
        foreach($link in $links)
        {
            $href = $link.href
            #we don't need relative urls or links we are sure are outside of nbg
            if(!(($href[0] -match "/")-or($href[0] -match "#")-or($href -eq "")-or($href.Contains(".pdf"))-or($href.Contains("youtube.com"))-or($href.Contains("twitter.com"))-or($href.Contains("facebook.com"))-or($href.Contains("espa.gr"))-or($href.Contains("ec.europa.eu"))-or($href.Contains("linkedin.com"))-or($href.Contains("javascript:__"))))
            {
                if(!($hrefs.Contains($href)))
                {
                    $hrefs += $href
                    try
                    {
                        $webRequest = Invoke-WebRequest -Uri $link.href
                        $content = $webRequest.Content
                        if(!($content.Contains("WebPart")-and $content.Contains("sp.") -and $content.Contains("/_layouts/") -and $content.Contains("/Style%20Library/"))) #if the page's html has these values, we are almost sure it's a sharepoint page
                        {
                            $href | Out-File -FilePath $tempOutputFile -Append
                        }
                    }
                    catch
                    {
                        Write-Log -Message "Error on link $href $_" -Path $LogPath -Level Error
                    }
                }
            }
        }
    }
}

#repeat the process for all subsites
for($i=$start-1;$i -lt $end;$i++)
{
    $url = $webs[$i].url
    Connect-PnPOnline -Url $url -UseWebLogin
   
    $webUrl = $url.Replace("https://authqa", "https://qa")
   
    Write-Host "Processing web: " $webUrl
    Write-Log -Message "Processing web: $webUrl" -Path $LogPath -Level Info
    $pages = Get-PnPListItem -List "Pages" -Query "<View><Query><Where><Eq><FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>Approved</Value></Eq></Where></Query></View>"
    foreach($page in $pages)
    {
        $pageUrl = $rootSiteURL + $page.FieldValues["FileRef"]
        try
        {
            $pageHTML = Invoke-WebRequest -Uri $pageUrl
        }
        catch
        {
            Write-Log -Message "Error on page $pageURL $_" -Path $LogPath -Level Error
            continue
        }
        $links = $pageHTML.Links
        foreach($link in $links)
        {
            $href = $link.href
            if(!(($href[0] -match "/")-or($href[0] -match "#")-or($href -eq "")-or($href.Contains(".pdf"))-or($href.Contains("youtube.com"))-or($href.Contains("twitter.com"))-or($href.Contains("facebook.com"))-or($href.Contains("espa.gr"))-or($href.Contains("ec.europa.eu"))-or($href.Contains("linkedin.com"))-or($href.Contains("javascript:__"))))
            {
                if(!($hrefs.Contains($href)))
                {
                    $hrefs += $href
                    try
                    {
                        $webRequest = Invoke-WebRequest -Uri $link.href
                        $content = $webRequest.Content
                        if(!($content.Contains("WebPart")-and $content.Contains("sp.") -and $content.Contains("/_layouts/") -and $content.Contains("/Style%20Library/")))
                        {
                            $href | Out-File -FilePath $tempOutputFile -Append
                        }
                    }
                    catch
                    {
                        Write-Log -Message "Error on link $href $_" -Path $LogPath -Level Error
                    }
                }
            }
        }
    }
}

#sort file and trim duplicates
gc $tempOutputFile | sort -CaseSensitive | get-unique > $OutputFile
Write-Log -Message "Execution Completed" -Path $LogPath -Level Info