$UserName = "spsetup"
$pwd = "p@ssw0rd"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$outputPath = "C:\Users\KyriakiBousiou\Desktop\subsites.csv"
$Url = "http://vm-sp2013"
Connect-PnPOnline -Url $Url -Credentials $Credentials
$webs = Get-PnPSubWebs -Recurse

foreach($web in $webs)
{
    $web.url | Out-File -FilePath $outputPath -Append
}