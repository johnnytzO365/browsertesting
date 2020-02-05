#initializations
$Url = "http://swisspost.spdev.local/"
$branchesCSV = "C:\Users\TheocharisGIANNOPOUL\Documents\GitHub\browsertesting\PnP Scripts\BRANCHES.csv"
$banksCSV = "C:\Users\TheocharisGIANNOPOUL\Documents\GitHub\browsertesting\PnP Scripts\BANKS.csv"

#connect
$UserName = "spsetup"
$pwd = "p@ssw0rd"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
Connect-PnPOnline -Url $Url -Credentials $Credentials

#======================================================================================================================================================
$items =Get-PnPListItem -List “BANKS” -Query "<View><Query><OrderBy><FieldRef Name='bankCode' Ascending='True' /></OrderBy></Query></View>"
$Banks = import-csv -Delimiter ";" -Path $banksCSV -Encoding UTF8 | sort 'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ'
#=======================================================================================================================================================
$Branch = Import-Csv -Delimiter ";" -Path $branchesCSV -Encoding UTF8 | sort 'ΚΩΔΙΚΟΣ HEBIC'
$items_Branch = Get-PnPListItem -List “BRANCHES” -Query "<View><Query><OrderBy><FieldRef Name='branchHebic' Ascending='True' /></OrderBy></Query></View>"

#start to import on banks
$l=0
$c=0
$stop_l=$items.Count
$stop_c=$Banks.Count

while($c -lt $stop_c){
    $csvitem = $Banks[$c].'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ'
    $sharepointItem = $items[$l].FieldValues['bankCode']
    if([int]$csvitem -eq [int]$sharepointItem){
        $l= $l + 1
        $c= $c + 1
    }elseif([int]$csvitem -gt [int]$sharepointItem){
        
        $id= Get-PnPProperty -ClientObject $items[$l] -Property Id
        Remove-PnPListItem -List "BANKS" -Identity $id -Force
        $l= $l + 1
    }
    else{
        
            $code = $csvitem
        if(!$Banks[$c].'SWIFT BIC' )
        {
            $bic = "-"
        }
        else
        {
            $bic = $Banks[$c].'SWIFT BIC'
        }
	
	    if(!$Banks[$c].'ΕΠΙΣΗΜΗ ΟΝΟΜΑΣΙΑ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)' )
        {
            $name = "-"
        }
        else
        {
            $name = $Banks[$c].'ΕΠΙΣΗΜΗ ΟΝΟΜΑΣΙΑ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)'
        }
	
	    if(!$Banks[$c].'ΤΗΛΕΦΩΝΙΚΟ ΚΕΝΤΡΟ' )
        {
            $tel = "-"
        }
        else
        {
            $tel = $Banks[$c].'ΤΗΛΕΦΩΝΙΚΟ ΚΕΝΤΡΟ'
        }
	
	    if(!$Banks[$c].'ΚΕΝΤΡΙΚΟFAX' )
        {
            $fax = "-"
        }
        else
        {
            $fax = $Banks[$c].'ΚΕΝΤΡΙΚΟFAX'
        }
	
	    if(!$Banks[$c].'ΔΙΕΥΘΥΝΣΗ ΕΔΡΑΣ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)' )
        {
            $region = "-"
        }
        else
        {
            $region = $Banks[$c].'ΔΙΕΥΘΥΝΣΗ ΕΔΡΑΣ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)'
        }
	
	    if(!$Banks[$c].'ΗΛΕΚΤΡΟΝΙΚΗ ΔΙΕΥΘΥΝΣΗ-URL' )
        {
            $website = "-"
        }
        else
        {
            $website = $Banks[$c].'ΗΛΕΚΤΡΟΝΙΚΗ ΔΙΕΥΘΥΝΣΗ-URL'
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
        $c=$c+1
    }
    if($l -eq $stop_l){
        break
    }
}

#delete all extra items of sharepoint list
if($c -eq $stop_c){
    for ( $i=$l; $i -lt $stop_l; $i++)
    {
        $id= Get-PnPProperty -ClientObject $items[$i] -Property Id
        Remove-PnPListItem -List "BANKS" -Identity $id -Force
    }
}

#add all extra items of csv
if($l -eq $stop_l){
    for($i=$c; $i -lt $stop_c; $i++)
    {
        $code=$Banks[$i].'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ'
        if(!$Banks[$i].'SWIFT BIC' )
        {
            $bic = "-"
        }
        else
        {
            $bic = $Banks[$i].'SWIFT BIC'
        }
	
	    if(!$Banks[$i].'ΕΠΙΣΗΜΗ ΟΝΟΜΑΣΙΑ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)' )
        {
            $name = "-"
        }
        else
        {
            $name = $Banks[$i].'ΕΠΙΣΗΜΗ ΟΝΟΜΑΣΙΑ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)'
        }
	
	    if(!$Banks[$i].'ΤΗΛΕΦΩΝΙΚΟ ΚΕΝΤΡΟ' )
        {
            $tel = "-"
        }
        else
        {
            $tel = $Banks[$i].'ΤΗΛΕΦΩΝΙΚΟ ΚΕΝΤΡΟ'
        }
	
	    if(!$Banks[$i].'ΚΕΝΤΡΙΚΟFAX' )
        {
            $fax = "-"
        }
        else
        {
            $fax = $Banks[$i].'ΚΕΝΤΡΙΚΟFAX'
        }
	
	    if(!$Banks[$i].'ΔΙΕΥΘΥΝΣΗ ΕΔΡΑΣ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)' )
        {
            $region = "-"
        }
        else
        {
            $region = $Banks[$i].'ΔΙΕΥΘΥΝΣΗ ΕΔΡΑΣ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)'
        }
	
	    if(!$Banks[$i].'ΗΛΕΚΤΡΟΝΙΚΗ ΔΙΕΥΘΥΝΣΗ-URL' )
        {
            $website = "-"
        }
        else
        {
            $website = $Banks[$i].'ΗΛΕΚΤΡΟΝΙΚΗ ΔΙΕΥΘΥΝΣΗ-URL'
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
}

#end of import on banks
#=======================================================================================================================================================

#start to import on branches
$l=0
$c=0
$stop_l=$items_Branch.Count
$stop_c=$Branch.Count

while($c -lt $stop_c){
    $csvitem = $Branch[$c].'ΚΩΔΙΚΟΣ HEBIC'
    $sharepointItem = $items_Branch[$l].FieldValues['branchHebic']
    #check the 2 hebics
    if([int]$csvitem -eq [int]$sharepointItem){
        #if items is equals go to next
        $l= $l + 1
        $c= $c + 1
    }
    elseif([int]$csvitem -gt [int]$sharepointItem){
        #if hebic of csv is greater than sharepoint's we dont need sharepoint's item anymore
        $id= Get-PnPProperty -ClientObject $items_Branch[$l] -Property Id
        Remove-PnPListItem -List "BRANCHES" -Identity $id -Force
        $l= $l + 1
    }
    else{
        #else add the new item on sharepoint
        if([int]$Branch[$c].'ΓΡΑΦΕΙΟ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ' -ge 100)
        {
            $tk1 = $Branch[$c].'ΓΡΑΦΕΙΟ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
        }
        else
        {
            $tk1 = "0"+$Branch[$c].'ΓΡΑΦΕΙΟ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
        }
        if([int]$Branch[$c].'ΔΙΑΔΡΟΜΗ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ' -ge 10)
        {
            $tk2 = $Branch[$c].'ΔΙΑΔΡΟΜΗ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
        }
        else
        {
            $tk2 = "0"+$Branch[$c].'ΔΙΑΔΡΟΜΗ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
        }
	
	    if(!$Branch[$c].'ΟΝΟΜΑΣΙΑ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)' )
        {
            $name = "-"
        }
        else
        {
            $name = $Branch[$c].'ΟΝΟΜΑΣΙΑ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)'
        }
	
	    if(!$Branch[$c].'ΟΝΟΜΑΣΙΑ ΤΟΠΟΘΕΣΙΑΣ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)' )
        {
            $region = "-"
        }
        else
        {
            $region = $Branch[$c].'ΟΝΟΜΑΣΙΑ ΤΟΠΟΘΕΣΙΑΣ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)'
        }
	
	    if(!$Branch[$c].'Διεύθυνση ΕΛΛΗΝΙΚΑ' )
        {
            $address = "-"
        }
        else
        {
            $address = $Branch[$c].'Διεύθυνση ΕΛΛΗΝΙΚΑ'
        }
		
	    if(!$Branch[$c].'ΑΡΙΘΜΟΣ ΤΗΛΕΦΩΝΟΥ' )
        {
            $tel = "-"
        }
        else
        {
            $tel = $Branch[$c].'ΑΡΙΘΜΟΣ ΤΗΛΕΦΩΝΟΥ'
        }
			
	    if(!$Branch[$c].'ΤΑΧΥΔΡΟΜΙΚΗ ΠΕΡΙΟΧΗ (ΕΛΛΗΝΙΚΑ)' )
        {
            $community = "-"
        }
        else
        {
            $community = $Branch[$c].'ΤΑΧΥΔΡΟΜΙΚΗ ΠΕΡΙΟΧΗ (ΕΛΛΗΝΙΚΑ)'
        }	
	
	    if(!$Branch[$c].'ΔΗΜΟΣ/ ΚΟΙΝΟΤΗΤΑ' )
        {
            $municipality = "-"
        }
        else
        {
            $municipality = $Branch[$c].'ΔΗΜΟΣ/ ΚΟΙΝΟΤΗΤΑ'
        }
        Add-PnPListItem -List "BRANCHES" -Values @{
        "branchHebic"=$csvitem;                                                   
        "branchName"= $name;                    
        "branchRegion"=$region;
        "branchAddress"= $address;
        "branchTel"= $tel;
        "branchCommunity"= $community;
        "branchMunicipality"= $municipality;
        "branchZipCode"= $tk1+$tk2;
        }
        $c=$c+1 
    }
}

#delete all extra items of sharepoint list
if($c -eq $stop_c){
    for ( $i=$l; $i -lt $stop_l; $i++)
    {
        $id= Get-PnPProperty -ClientObject $items_Branch[$i] -Property Id
        Remove-PnPListItem -List "BRANCHES" -Identity $id -Force
    }
}

#add all extra items of csv 
if($l -eq $stop_l){
    for($i=$c; $i -lt $stop_c; $i++)
    {
        $code=$Branch[$i].'ΚΩΔΙΚΟΣ HEBIC'
        if([int]$Branch[$i].'ΓΡΑΦΕΙΟ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ' -ge 100)
        {
            $tk1 = $Branch[$i].'ΓΡΑΦΕΙΟ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
        }
        else
        {
            $tk1 = "0"+$Branch[$i].'ΓΡΑΦΕΙΟ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
        }
        if([int]$Branch[$i].'ΔΙΑΔΡΟΜΗ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ' -ge 10)
        {
            $tk2 = $Branch[$i].'ΔΙΑΔΡΟΜΗ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
        }
        else
        {
            $tk2 = "0"+$Branch[$i].'ΔΙΑΔΡΟΜΗ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
        }
	
	    if(!$Branch[$i].'ΟΝΟΜΑΣΙΑ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)' )
        {
            $name = "-"
        }
        else
        {
            $name = $Branch[$i].'ΟΝΟΜΑΣΙΑ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)'
        }
	
	    if(!$Branch[$i].'ΟΝΟΜΑΣΙΑ ΤΟΠΟΘΕΣΙΑΣ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)' )
        {
            $region = "-"
        }
        else
        {
            $region = $Branch[$i].'ΟΝΟΜΑΣΙΑ ΤΟΠΟΘΕΣΙΑΣ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)'
        }
	
	    if(!$Branch[$i].'Διεύθυνση ΕΛΛΗΝΙΚΑ' )
        {
            $address = "-"
        }
        else
        {
            $address = $Branch[$i].'Διεύθυνση ΕΛΛΗΝΙΚΑ'
        }
		
	    if(!$Branch[$i].'ΑΡΙΘΜΟΣ ΤΗΛΕΦΩΝΟΥ' )
        {
            $tel = "-"
        }
        else
        {
            $tel = $Branch[$i].'ΑΡΙΘΜΟΣ ΤΗΛΕΦΩΝΟΥ'
        }
			
	    if(!$Branch[$i].'ΤΑΧΥΔΡΟΜΙΚΗ ΠΕΡΙΟΧΗ (ΕΛΛΗΝΙΚΑ)' )
        {
            $community = "-"
        }
        else
        {
            $community = $Branch[$i].'ΤΑΧΥΔΡΟΜΙΚΗ ΠΕΡΙΟΧΗ (ΕΛΛΗΝΙΚΑ)'
        }	
	
	    if(!$Branch[$i].'ΔΗΜΟΣ/ ΚΟΙΝΟΤΗΤΑ' )
        {
            $municipality = "-"
        }
        else
        {
            $municipality = $Branch[$i].'ΔΗΜΟΣ/ ΚΟΙΝΟΤΗΤΑ'
        }
        Add-PnPListItem -List "BRANCHES" -Values @{
        "branchHebic"=$csvitem;                                                   
        "branchName"= $name;                    
        "branchRegion"=$region;
        "branchAddress"= $address;
        "branchTel"= $tel;
        "branchCommunity"= $community;
        "branchMunicipality"= $municipality;
        "branchZipCode"= $tk1+$tk2;
        }
}
}

#end of branches
#=================================================================================================