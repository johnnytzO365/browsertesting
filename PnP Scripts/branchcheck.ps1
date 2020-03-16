#initializations
$Url = "http://mynbgportal/sites/hebic/"
$branchesCSV = "C:\Users\e82331\Desktop\branches. 31 12 19.csv"
$banksCSV = "C:\Users\e82331\Documents\banks. 31 12 19.csv"
$ErrorActionPreference = "SilentlyContinue"

#connect
$UserName = "bank\e82331"
$pwd = "Y?Ugjxgar"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
Connect-PnPOnline -Url $Url -Credentials $Credentials

$items =Get-PnPListItem -List “BANKS”
foreach($item in $items){
    Set-PnPListItem -List "BANKS" -Identity $item -Values @{
     "bankChange"="NO"
    }
}
$items =Get-PnPListItem -List “BRANCHES”
foreach($item in $items){
    Set-PnPListItem -List "BRANCHES" -Identity $item -Values @{
     "branchChange"="NO"
    }
}
#start of brances===============================================================================
#take branches from csv
$Branches = import-csv -Delimiter ";" -Path $branchesCSV -Encoding UTF8

foreach ($Branch in $Branches){
    #check if this branch exist in list branches 
    #as key we use the hebic
    $changed_item= $Branch.'ΚΩΔΙΚΟΣ HEBIC'
    if([int]$changed_item -ge 1000000){
        $changed_item= $changed_item;
    }
    else
    {
        $changed_item= "0"+$changed_item;
    }

    $checks= Get-PnPListItem -List "BRANCHES" -Query "<View><Query><Where><Eq><FieldRef Name='branchHebic' /><Value Type='Text'>$changed_item</Value></Eq></Where></Query></View>"
    
    #take the values from csv
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
    #if we have a new branch, we will add it in list
    if(!$checks)
    {
        Add-PnPListItem -List "BRANCHES" -Values @{
        "branchHebic"=$changed_item;                                                   
        "branchName"= $name;                    
        "branchRegion"=$region;
        "branchAddress"= $address;
        "branchTel"= $tel;
        "branchCommunity"= $community;
        "branchMunicipality"= $municipality;
        "branchZipCode"= $tk1+$tk2;
        }
    }
    #we check if something has changed
    else{
         Set-PnPListItem -List "BRANCHES" -Identity $checks -Values @{
            "branchChange"="YES"
         }
        if($checks.FieldValues['branchName'] -ne $name){
            Set-PnPListItem -List "BRANCHES" -Identity $checks -Values @{
                "branchName"= $name;
            }
        }
        if($checks.FieldValues['branchRegion'] -ne $region){
            Set-PnPListItem -List "BRANCHES" -Identity $checks -Values @{
                "branchRegion"= $region;
            }
        }
        if($checks.FieldValues['branchAddress'] -ne $address){
            Set-PnPListItem -List "BRANCHES" -Identity $checks -Values @{
                "branchAddress"= $address;
            }
        }
        if($checks.FieldValues['branchTel'] -ne $tel){
            Set-PnPListItem -List "BRANCHES" -Identity $checks -Values @{
                "branchTel"= $tel;
            }
        }
       if($checks.FieldValues['branchCommunity'] -ne $community){
            Set-PnPListItem -List "BRANCHES" -Identity $checks -Values @{
                "branchCommunity"= $community;
            }
        }
       if($checks.FieldValues['branchMunicipality'] -ne $municipality){
            Set-PnPListItem -List "BRANCHES" -Identity $checks -Values @{
                "branchMunicipality"= $municipality;
            }
        }
       if($checks.FieldValues['branchZipCode'] -ne $tk1+$tk2){
            Set-PnPListItem -List "BRANCHES" -Identity $checks -Values @{
                "branchZipCode"= $tk1+$tk2;
            }
        }
    }
}
$items =Get-PnPListItem -List “BRANCHES”
$neverChanges=Get-PnPListItem -List "BRANCHES" -Query "<View><Query><Where><Eq><FieldRef Name='branchChange' /><Value Type='Text'>'NO'</Value></Eq></Where></Query></View>"
foreach($neverChange in $neverChanges)
{
    $id= Get-PnPProperty -ClientObject $neverChange -Property Id
    Remove-PnPListItem -List "BRANCHES" -Identity $id -Force
}
#end of branches=================================================================================

#start of banks==================================================================================
#take banks from csv
$Banks = import-csv -Delimiter ";" -Path $banksCSV -Encoding UTF8
foreach ($Bank in $Banks){
    $changed_item=$Bank.'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ'
    if([int]$changed_item -ge 100)
    {
        $code = $changed_item
    }
    else
    {
        $code = "0"+$changed_item
    }

    $checks= Get-PnPListItem -List "BANKS" -Query "<View><Query><Where><Eq><FieldRef Name='bankCode' /><Value Type='Text'>$code</Value></Eq></Where></Query></View>"
	
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
    if(!$checks){
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
    else{
         Set-PnPListItem -List "BANKS" -Identity $checks -Values @{
            "bankChange"="YES"
            }
         if($checks.FieldValues['bankName'] -ne $name){
            Set-PnPListItem -List "BANKS" -Identity $checks -Values @{
                "bankName"= $name;
            }
        }
        if($checks.FieldValues['bankRegion'] -ne $region){
            Set-PnPListItem -List "BANKS" -Identity $checks -Values @{
                "bankRegion"= $region;
            }
        }
        if($checks.FieldValues['bankBic'] -ne $bic){
            Set-PnPListItem -List "BANKS" -Identity $checks -Values @{
                "branchAddress"= $address;
            }
        }
        if($checks.FieldValues['bankTel'] -ne $tel){
            Set-PnPListItem -List "BANKS" -Identity $checks -Values @{
                "bankTel"= $tel;
            }
        }
       if($checks.FieldValues['bankFax'] -ne $fax){
            Set-PnPListItem -List "BANKS" -Identity $checks -Values @{
                "bankFax"= $fax;
            }
        }
       if($checks.FieldValues['bankWebSite'] -ne $website){
            Set-PnPListItem -List "BANKS" -Identity $checks -Values @{
                "bankWebSite"= $website;
            }
        }

       }
    }

$neverChanges=Get-PnPListItem -List "BANKS" -Query "<View><Query><Where><Eq><FieldRef Name='bankChange' /><Value Type='Text'>'NO'</Value></Eq></Where></Query></View>"
foreach($neverChange in $neverChanges)
{
    $id= Get-PnPProperty -ClientObject $neverChange -Property Id
    Remove-PnPListItem -List "BANKS" -Identity $id -Force
}


