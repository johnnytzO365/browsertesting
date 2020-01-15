#initializations
$Url = "http://v000080043:9993/sites/sp_team_nbg/TestEvretirio/"
$banksCSV = "C:\Users\e82331\Downloads\BANKS.csv"
$branchesCSV = "C:\Users\e82331\Downloads\BRANCHES.csv"
$tempBank = "C:\Temp\banks_utf8.csv"
$tempBranch = "C:\Temp\branches_utf8.csv"

$ErrorActionPreference = "SilentlyContinue"

#connect
$UserName = "e82331"
$pwd = "p@ssw0rd"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
$connection = Connect-PnPOnline -Url $Url -Credentials $Credentials

#create the list
$listName = "BANKS"
try
{
    New-PnPList -Title $listName -Template GenericList
    Add-PnPField -List $listName -DisplayName "Code" -InternalName "bankCode" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Name" -InternalName "bankName" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Region" -InternalName "bankRegion" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Bic" -InternalName "bankBic" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Tel" -InternalName "bankTel" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Fax" -InternalName "bankFax" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "WebSite" -InternalName "bankWebSite" -Type Text -AddToDefaultView
}
catch
{}

Get-Content  $banksCSV | Out-File $tempBank -Encoding utf8
$Banks = import-csv -Delimiter ";" -Path $tempBank -Encoding Unicode

$items =Get-PnPListItem -List “BANKS”
foreach ($item in $items)
{
    $id = Get-PnPProperty -ClientObject $item -Property Id
    Remove-PnPListItem -List "BANKS” -Identity $id -Force
}
foreach ($Bank in $Banks){
    Add-PnPListItem -List "BANKS" -Values @{
        "bankCode"=$Bank.'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ';
        "bankBic"=$Bank.'SWIFT BIC';                                                   
        "bankName"= $Bank.'ΕΠΙΣΗΜΗ ΟΝΟΜΑΣΙΑ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)';                    
        "bankTel"= $Bank.'ΤΗΛΕΦΩΝΟ ΤΟΥ ΤΗΛΕΦΩΝΙΚΟΥ ΚΕΝΤΡΟΥ';
        "bankFax"= $Bank.'ΚΕΝΤΡΙΚΟFAX';
        "bankRegion"= $Bank.'ΔΙΕΥΘΥΝΣΗ ΕΔΡΑΣ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)';
        "bankWebSite"= $Bank.'ΗΛΕΚΤΡΟΝΙΚΗ ΔΙΕΥΘΥΝΣΗ ΤΟΥ WEBSITE';
    }
}

#create the list
$listName = "BRANCHES"
try
{
    New-PnPList -Title $listName -Template GenericList
    Add-PnPField -List $listName -DisplayName "Hebic" -InternalName "branchHebic" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Name" -InternalName "branchName" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Region" -InternalName "branchRegion" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Address" -InternalName "branchAddress" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Tel" -InternalName "branchTel" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Community" -InternalName "branchCommunity" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Municipality" -InternalName "branchMunicipality" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "ZipCode" -InternalName "branchZipCode" -Type Text -AddToDefaultView
}
catch
{}

Get-Content  $branchesCSV | Out-File $tempBranch -Encoding utf8
$Branches = import-csv -Delimiter ";" -Path $tempBranch -Encoding Unicode

$items =Get-PnPListItem -List “BRANCHES”
foreach ($item in $items)
{
    Remove-PnPListItem -List "BRANCHES” -Identity $item.Id -Force
}
foreach ($Branch in $Branches){
    Add-PnPListItem -List "BRANCHES" -Values @{
        "branchHebic"=$Branch.'ΚΩΔΙΚΟΣ HEBIC';                                                   
        "branchName"= $Branch.'ΟΝΟΜΑΣΙΑ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)';                    
        "branchRegion"=$Branch.'ΟΝΟΜΑΣΙΑ ΤΟΠΟΘΕΣΙΑΣ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)';
        "branchAddress"= $Branch.'Διεύθυνση (οδός, αριθμός) ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)';
        "branchTel"= $Branch.'ΑΡΙΘΜΟΣ ΤΗΛΕΦΩΝΟΥ';
        "branchCommunity"= $Branch.'ΤΑΧΥΔΡΟΜΙΚΗ ΠΕΡΙΟΧΗ (ΕΛΛΗΝΙΚΑ)';
        "branchMunicipality"= $Branch.'ΔΗΜΟΣ/ΚΟΙΝΟΤΗΤΑ';
        "branchZipCode"= $Branch.'ΓΡΑΦΕΙΟ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ'+$Branch.'ΔΙΑΔΡΟΜΗ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
    }
}

Remove-Item -Path $tempBank -Force
Remove-Item -Path $tempBranch -Force