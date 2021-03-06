﻿#initializations
$Url = "http://mynbgportal/sites/hebic/"
$banksCSV = "C:\Users\e82331\Desktop\Git\browsertesting\PnP Scripts\BANKS.csv"
$branchesCSV = "C:\Users\e82331\Desktop\Git\browsertesting\PnP Scripts\BRANCHES.csv"

$ErrorActionPreference = "SilentlyContinue"

#connect
$UserName = "bank\e82331"
$pwd = "p@ssw0rd"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
Connect-PnPOnline -Url $Url -Credentials $Credentials

#create the list
$listName = "BANKS"
$list = Get-PnPList $listName
if ($list -eq $null)
{
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
}

$Banks = import-csv -Delimiter ";" -Path $banksCSV -Encoding UTF8

$items =Get-PnPListItem -List “BANKS”
foreach ($item in $items)
{
    $id = Get-PnPProperty -ClientObject $item -Property Id
    Remove-PnPListItem -List “BANKS” -Identity $id -Force
}

foreach ($Bank in $Banks){
    if([int]$Bank.'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ' -ge 100)
    {
        $code = $Bank.'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ'
    }
    else
    {
        $code = "0"+$Bank.'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ'
    }
	
	if(!$Bank.'SWIFT BIC' )
    {
        $bic = "-"
    }
    else
    {
        $bic = $Bank.'SWIFT BIC'
    }
	
	if(!$Bank.'ΕΠΙΣΗΜΗ ΟΝΟΜΑΣΙΑ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)' )
    {
        $name = "-"
    }
    else
    {
        $name = $Bank.'ΕΠΙΣΗΜΗ ΟΝΟΜΑΣΙΑ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)'
    }
	
	if(!$Bank.'ΤΗΛΕΦΩΝΙΚΟ ΚΕΝΤΡΟ' )
    {
        $tel = "-"
    }
    else
    {
        $tel = $Bank.'ΤΗΛΕΦΩΝΙΚΟ ΚΕΝΤΡΟ'
    }
	
	if(!$Bank.'ΚΕΝΤΡΙΚΟFAX' )
    {
        $fax = "-"
    }
    else
    {
        $fax = $Bank.'ΚΕΝΤΡΙΚΟFAX'
    }
	
	if(!$Bank.'ΔΙΕΥΘΥΝΣΗ ΕΔΡΑΣ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)' )
    {
        $region = "-"
    }
    else
    {
        $region = $Bank.'ΔΙΕΥΘΥΝΣΗ ΕΔΡΑΣ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)'
    }
	
	if(!$Bank.'ΗΛΕΚΤΡΟΝΙΚΗ ΔΙΕΥΘΥΝΣΗ-URL' )
    {
        $website = "-"
    }
    else
    {
        $website = $Bank.'ΗΛΕΚΤΡΟΝΙΚΗ ΔΙΕΥΘΥΝΣΗ-URL'
    }
	
    Add-PnPListItem -List "BANKS" -Values @{
        "bankCode"=$code;
        "bankBic"=$bic;                                                   
        "bankName"= $name;                    
        "bankTel"= $tel;
        "bankFax"= $fax;
        "bankRegion"= $region;
        "bankWebSite"= $website;
    }
}

#create the list
$listName = "BRANCHES"
$list = Get-PnPList $listName
if ($list -eq $null)
{
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
}

$Branches = import-csv -Delimiter ";" -Path $branchesCSV -Encoding UTF8

$items =Get-PnPListItem -List “BRANCHES”
foreach ($item in $items)
{
    Remove-PnPListItem -List "BRANCHES" -Identity $item.Id -Force
}
foreach ($Branch in $Branches){
    if([int]$Bank.'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ' -ge 1000000)
    {
        $hebic = $Branch.'ΚΩΔΙΚΟΣ HEBIC';
    }
    else
    {
        $hebic = "0"+$Branch.'ΚΩΔΙΚΟΣ HEBIC';
    }
    if([int]$Branch.'ΓΡΑΦΕΙΟ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ' -ge 100)
    {
        $tk1 = $Branch.'ΓΡΑΦΕΙΟ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
    }
    else
    {
        $tk1 = "0"+$Branch.'ΓΡΑΦΕΙΟ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
    }
    if([int]$Branch.'ΔΙΑΔΡΟΜΗ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ' -ge 10)
    {
        $tk2 = $Branch.'ΔΙΑΔΡΟΜΗ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
    }
    else
    {
        $tk2 = "0"+$Branch.'ΔΙΑΔΡΟΜΗ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
    }
	
	if(!$Branch.'ΟΝΟΜΑΣΙΑ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)' )
    {
        $name = "-"
    }
    else
    {
        $name = $Branch.'ΟΝΟΜΑΣΙΑ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)'
    }
	
	if(!$Branch.'ΟΝΟΜΑΣΙΑ ΤΟΠΟΘΕΣΙΑΣ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)' )
    {
        $region = "-"
    }
    else
    {
        $region = $Branch.'ΟΝΟΜΑΣΙΑ ΤΟΠΟΘΕΣΙΑΣ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)'
    }
	
	if(!$Branch.'Διεύθυνση ΕΛΛΗΝΙΚΑ' )
    {
        $address = "-"
    }
    else
    {
        $address = $Branch.'Διεύθυνση ΕΛΛΗΝΙΚΑ'
    }
		
	if(!$Branch.'ΑΡΙΘΜΟΣ ΤΗΛΕΦΩΝΟΥ' )
    {
        $tel = "-"
    }
    else
    {
        $tel = $Branch.'ΑΡΙΘΜΟΣ ΤΗΛΕΦΩΝΟΥ'
    }
			
	if(!$Branch.'ΤΑΧΥΔΡΟΜΙΚΗ ΠΕΡΙΟΧΗ (ΕΛΛΗΝΙΚΑ)' )
    {
        $community = "-"
    }
    else
    {
        $community = $Branch.'ΤΑΧΥΔΡΟΜΙΚΗ ΠΕΡΙΟΧΗ (ΕΛΛΗΝΙΚΑ)'
    }	
	
	if(!$Branch.'ΔΗΜΟΣ/ ΚΟΙΝΟΤΗΤΑ' )
    {
        $municipality = "-"
    }
    else
    {
        $municipality = $Branch.'ΔΗΜΟΣ/ ΚΟΙΝΟΤΗΤΑ'
    }
    Add-PnPListItem -List "BRANCHES" -Values @{
        "branchHebic"=$hebic;                                                   
        "branchName"= $name;                    
        "branchRegion"=$region;
        "branchAddress"= $address;
        "branchTel"= $tel;
        "branchCommunity"= $community;
        "branchMunicipality"= $municipality;
        "branchZipCode"= $tk1+$tk2;
    }
}