[System.Reflection.Assembly]::UnsafeLoadfrom($defaultDLLPath)      
$defaultDLLPath= "C:\Users\e82331\Downloads\microsoft.sharepointonline.csom.16.1.19515.12000\lib\net45\Microsoft.SharePoint.Client.Runtime.dll"       
[System.Reflection.Assembly]::UnsafeLoadfrom($defaultDLLPath)        
$defaultDLLPath= "C:\Users\e82331\Downloads\microsoft.sharepointonline.csom.16.1.19515.12000\lib\net45\Microsoft.Online.SharePoint.Client.Tenant.dll"       
[System.Reflection.Assembly]::UnsafeLoadfrom($defaultDLLPath)        
#$defaultDLLPath= "C:\Users\e82276\Downloads\microsoft.sharepointonline.csom.16.1.19515.12000\lib\net45\Client.Search.dll"       
$defaultDLLPath= "C:\Users\e82331\Downloads\microsoft.sharepointonline.csom.16.1.19515.12000\lib\net45\Microsoft.SharePoint.Client.Search.dll"       
[System.Reflection.Assembly]::UnsafeLoadfrom($defaultDLLPath)      
$defaultDLLPath= "C:\Users\e82331\Downloads\microsoft.sharepointonline.csom.16.1.19515.12000\lib\net45\Microsoft.SharePoint.Client.dll"

$NewsPageFileName = "Repost"
[string]$username = "sindy@bousiou.onmicrosoft.com"

#[string]$PwdTXTPath = "C:\SECUREDPWD\ExportedPWD-$($username).txt"

$secureStringPwd = ConvertTo-SecureString -string "Y?Ugjxgar" -AsPlainText -Force

#$cred = New-Object System.Management.Automation.PSCredential -ArgumentList $username, $secureStringPwd

$Myctx = New-Object Microsoft.SharePoint.Client.ClientContext("https://bousiou.sharepoint.com/sites/communicationTest")

 

$Myctx.Credentials = New-Object Microsoft.SharePoint.Client.SharePointOnlineCredentials($userName,$secureStringPwd)

$Myctx.RequestTimeout = 1000000 # milliseconds

$MyspoRootweb = $Myctx.Web

$Myctx.Load($MyspoRootweb)

$Myctx.ExecuteQuery()

 

$MyPagelist = $Myctx.Web.Lists.GetByTitle("Site Pages")

$Myctx.Load($MyPagelist);

$Myctx.ExecuteQuery()

 

$NewPageitem = $MyPagelist.RootFolder.Files.AddTemplateFile("/sites/communicationTest/SitePages/" + $NewsPageFileName + ".aspx", [Microsoft.SharePoint.Client.TemplateFileType]::ClientSidePage).ListItemAllFields

                     # Make this page a "modern" page

                     $NewPageitem["ContentTypeId"] = "0x0101009D1CB255DA76424F860D91F20E6C4118002A50BFCFB7614729B56886FADA02339B00874A802FBA36B64BAB7A47514EAAB232";

                     $NewPageitem["PageLayoutType"] = "RepostPage"

                     $NewPageitem["PromotedState"] = "2"

                     $NewPageitem["Title"] = "Repost"

                     $NewPageitem["ClientSideApplicationId"] = "b6917cb1-93a0-4b97-a84d-7cf49975d4ec"

 

                     #$NewPageitem["_OriginalSourceSiteId"] = $MyDestinationPageSiteColl.ID

                     #$NewPageitem["_OriginalSourceWebId"] = $MyDestinationPageweb.ID

                     #$NewPageitem["_OriginalSourceListId"] = $MyDestinationPagelist.ID

                     #$NewPageitem["_OriginalSourceItemId"] = $MyDestinationPageFileitem["UniqueId"].ToString()

 

                     $NewPageitem["_OriginalSourceUrl"] =  "https://bousiou.sharepoint.com/:b:/s/communicationTest/EbWuImmDL5pMihRMXy5LegcBdNXxC6enECQ1PBqxwNXwhw?e=AC7KHP"

                     #$NewPageitem["Editor"] = $MyEditoruserAccount.Id

                     #$NewPageitem["Author"] = $MyEditoruserAccount.Id

                     #$NewPageitem["Description"] = $MyCSVNewsDescription

                     #$NewPageitem["BannerImageUrl"] = $MyCSVNewsPictureURL;

                     #$NewPageitem["Modified"] = $MyCSVPublishingDate;

                     #$NewPageitem["Created"] = $MyCSVPublishingDate;

                     #$NewPageitem["Created_x0020_By"] = $MyEditoruserAccount.LoginName

                     #$NewPageitem["Modified_x0020_By"] = $MyEditoruserAccount.LoginName

                     #$NewPageitem["FirstPublishedDate"] = $MyCSVPublishingDate;

                     $NewPageitem.Update()

            $NewPageitem.File.Publish("ss")

                     $Myctx.Load($NewPageitem)

                     $Myctx.ExecuteQuery()