#Get NBG Links and Write All Non Sharepoint Pages inside NBG Domain

Import-Module SharePointPnPPowerShell2013
# Iterate all webs
Connect-PnPOnline -Url $args[0] -UseWebLogin #you need to open IE, login to https://authqa.nbg.gr/ and leave the window open
$authWebUrl = (Get-PnPWeb).Url                            #you need to refresh the window every few minutes
$webUrl = $authWebUrl.Replace("https://authqa", "https://qa")

$outputPath = "C:\Users\e82331\Desktop\links.txt" #change
$rootSiteURL = $args[0]

#root
Write-Host "Processing web: " $webUrl
$pages = Get-PnPListItem -List "Pages"
foreach($page in $pages)
{
    $pageUrl = $rootSiteURL + $page.FieldValues["FileRef"]
    $pageHTML = Invoke-WebRequest -Uri $pageUrl
    $links = $pageHTML.Links
    foreach($link in $links)
    {
        $href = $link.href
        #we don't need relative urls or links we are sure are outside of nbg
        if(!(($href[0] -match "/")-or($href[0] -match "#")-or($href -eq "")-or($href.Contains(".pdf"))-or($href.Contains("youtube.com"))-or($href.Contains("twitter.com"))-or($href.Contains("facebook.com"))-or($href.Contains("espa.gr"))-or($href.Contains("ec.europa.eu"))-or($href.Contains("linkedin.com"))-or($href.Contains("javascript:__"))))
        {
            try
            {
                $webRequest = Invoke-WebRequest -Uri $link.href
                $content = $webRequest.Content
                if(!($content.Contains("WebPart")-and $content.Contains("sp.") -and $content.Contains("/_layouts/") -and $content.Contains("/Style%20Library/"))) #if the page's html has these values, we are almost sure it's a sharepoint page
                {
                    $href | Out-File -FilePath $outputPath -Append
                }
            }
            catch
            {
                if($_.Exception.Message.Contains("Invalid URI"))
                {
                    Write-Host "INVALID:" $href
                }
                elseif($_.Exception.Message.Contains("File Not Found"))
                {
                    Write-Host "NOT FOUND:" $href
                }
            }
        }
    }
}

#repeat the process for all subsites
$webs = Get-PnPSubWebs -Recurse

foreach($web in $webs)
{
    $url = $web.url
    Connect-PnPOnline -Url $url -UseWebLogin
   
    $webUrl = $url.Replace("https://authqa", "https://qa")
   
    Write-Host "Processing web: " $webUrl
    $pages = Get-PnPListItem -List "Pages"
    foreach($page in $pages)
    {
        $pageUrl = $rootSiteURL + $page.FieldValues["FileRef"]
        $pageHTML = Invoke-WebRequest -Uri $pageUrl
        $links = $pageHTML.Links
        foreach($link in $links)
        {
            $href = $link.href
            if(!(($href[0] -match "/")-or($href[0] -match "#")-or($href -eq "")-or($href.Contains(".pdf"))-or($href.Contains("youtube.com"))-or($href.Contains("twitter.com"))-or($href.Contains("facebook.com"))-or($href.Contains("espa.gr"))-or($href.Contains("ec.europa.eu"))-or($href.Contains("linkedin.com"))-or($href.Contains("javascript:__"))))
            {
                try
                {
                    $webRequest = Invoke-WebRequest -Uri $link.href
                    $content = $webRequest.Content
                    if(!($content.Contains("WebPart")-and $content.Contains("sp.") -and $content.Contains("/_layouts/") -and $content.Contains("/Style%20Library/")))
                    {
                        $href | Out-File -FilePath $outputPath -Append
                    }
                }
                catch
                {
                    if($_.Exception.Message.Contains("Invalid URI"))
                    {
                        Write-Host "INVALID:" $href
                    }
                    elseif($_.Exception.Message.Contains("File Not Found"))
                    {
                        Write-Host "NOT FOUND:" $href
                    }
                }
            }
        }
    }
}