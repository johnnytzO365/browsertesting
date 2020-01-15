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
    Add-PnPField -List $listName -DisplayName "Code" -InternalName "Code" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Name" -InternalName "Name" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Region" -InternalName "Region" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Bic" -InternalName "Bic" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Tel" -InternalName "Tel" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Fax" -InternalName "Fax" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "WebSite" -InternalName "WebSite" -Type Text -AddToDefaultView
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
        "Code"=$Bank.'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ';
        "Bic"=$Bank.'SWIFT BIC';                                                   
        "Name"= $Bank.'ΕΠΙΣΗΜΗ ΟΝΟΜΑΣΙΑ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)';                    
        "Tel"= $Bank.'ΤΗΛΕΦΩΝΟ ΤΟΥ ΤΗΛΕΦΩΝΙΚΟΥ ΚΕΝΤΡΟΥ';
        "Fax"= $Bank.'ΚΕΝΤΡΙΚΟFAX';
        "Region"= $Bank.'ΔΙΕΥΘΥΝΣΗ ΕΔΡΑΣ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)';
        "WebSite"= $Bank.'ΗΛΕΚΤΡΟΝΙΚΗ ΔΙΕΥΘΥΝΣΗ ΤΟΥ WEBSITE';
    }
}

#create the list
$listName = "BRANCHES"
try
{
    New-PnPList -Title $listName -Template GenericList
    Add-PnPField -List $listName -DisplayName "Hebic" -InternalName "Hebic" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Name" -InternalName "Name" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Region" -InternalName "Region" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Address" -InternalName "Address" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Tel" -InternalName "Tel" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Community" -InternalName "Community" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "Municipality" -InternalName "Municipality" -Type Text -AddToDefaultView
    Add-PnPField -List $listName -DisplayName "ZipCode" -InternalName "ZipCode" -Type Text -AddToDefaultView
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
        "Hebic"=$Branch.'ΚΩΔΙΚΟΣ HEBIC';                                                   
        "Name"= $Branch.'ΟΝΟΜΑΣΙΑ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)';                    
        "Region"=$Branch.'ΟΝΟΜΑΣΙΑ ΤΟΠΟΘΕΣΙΑΣ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)';
        "Address"= $Branch.'Διεύθυνση (οδός, αριθμός) ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)';
        "Tel"= $Branch.'ΑΡΙΘΜΟΣ ΤΗΛΕΦΩΝΟΥ';
        "Community"= $Branch.'ΤΑΧΥΔΡΟΜΙΚΗ ΠΕΡΙΟΧΗ (ΕΛΛΗΝΙΚΑ)';
        "Municipality"= $Branch.'ΔΗΜΟΣ/ΚΟΙΝΟΤΗΤΑ';
        "ZipCode"= $Branch.'ΓΡΑΦΕΙΟ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ'+$Branch.'ΔΙΑΔΡΟΜΗ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
    }
}

Remove-Item -Path $tempBank -Force
Remove-Item -Path $tempBranch -Force