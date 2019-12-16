Import-Module SharePointPnPPowerShell2013

$UserName = "bank\e82331"
$pwd = "123sindy^"

[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)

$Url = "http://mynbgportal:86/InternalCom"
Connect-PnPOnline -Url $Url -Credentials $Credentials

$outputPath = "C:\Users\e82337\Desktop\pageUrls.txt"

$ListItems = Get-PnPListItem -List "Pages"
ForEach($Item in $ListItems)
{
    $name = Get-PnPProperty -ClientObject $Item.File -Property Name
    if($name.EndsWith(".aspx"))
    {
        $pageUrl = "http://mynbgportal:86/InternalCom/Pages/" + $name
        $pageUrl | Out-File -FilePath $outputPath -Append
    }
}