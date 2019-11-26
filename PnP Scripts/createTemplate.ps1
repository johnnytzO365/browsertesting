Import-Module SharePointPnPPowerShellOnline

$UserName = "e82331@nbg.gr"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$Url = "https://groupnbg.sharepoint.com/sites/TranformationQA"
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials

Write-Host "Creating Template" 

Get-PnPProvisioningTemplate -Out "C:\Users\KyriakiBousiou\Desktop\TransformationQATemplate.xml" -IncludeAllTermGroups -IncludeSiteCollectionTermGroup -PersistBrandingFiles -Handlers Lists -ExcludeContentTypesFromSyndication -ListsToExtract "Site Assets","Site Pages","Style Library","testList","Documents"