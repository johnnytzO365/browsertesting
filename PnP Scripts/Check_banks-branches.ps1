<#
.SYNOPSIS
  <Overview of script>
.DESCRIPTION
  <Brief description of script>
.PARAMETER <Parameter_Name>
    <Brief description of parameter input required. Repeat this attribute if required>
.INPUTS
  <Inputs if any, otherwise state None>
.OUTPUTS
  <Outputs if any, otherwise state None - example: Log file stored in C:\Windows\Temp\<name>.log>
.NOTES
  Version:        1.0
  Author:         <Name>
  Creation Date:  <Date>
  Purpose/Change: Initial script development
  
.EXAMPLE
  <Example goes here. Repeat this attribute for more than one example>
#>

#---------------------------------------------------------[Initialisations]--------------------------------------------------------
$Url = "http://swisspost.spdev.local/"
$branchesCSV = "C:\Users\TheocharisGIANNOPOUL\Documents\GitHub\browsertesting\PnP Scripts\BRANCHES.csv"
$banksCSV = "C:\Users\TheocharisGIANNOPOUL\Documents\GitHub\browsertesting\PnP Scripts\BANKS.csv"

#----------------------------------------------------------[Declarations]----------------------------------------------------------
#connect
$UserName = "spsetup"
$pwd = "p@ssw0rd"
[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)
Connect-PnPOnline -Url $Url -Credentials $Credentials

#======================================================================================================================================================
$items =Get-PnPListItem -List “BANKS” -Query "<View><Query><OrderBy><FieldRef Name='bankCode' Ascending='True' /></OrderBy></Query></View>"
$Banks = import-csv -Delimiter ";" -Path $banksCSV -Encoding UTF8 | sort 'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ'
#======================================================================================================================================================
$Branch = Import-Csv -Delimiter ";" -Path $branchesCSV -Encoding UTF8 | sort 'ΚΩΔΙΚΟΣ HEBIC'
$items_Branch = Get-PnPListItem -List “BRANCHES” -Query "<View><Query><OrderBy><FieldRef Name='branchHebic' Ascending='True' /></OrderBy></Query></View>"

#-----------------------------------------------------------[Functions]------------------------------------------------------------

function Take_Item_Index_Bank($Bank,$c,$bic,$name,$tel,$fax,$region,$website)
{
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
}
function Take_Item_Index_Branch($Branch,$c,$name,$region,$address,$tel,$community,$municipality,$tk1,$tk2)
{
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
}
#-----------------------------------------------------------[Execution]------------------------------------------------------------

#start to import on banks
$l=0
$c=0
$stop_l=$items.Count
$stop_c=$Banks.Count

while($c -lt $stop_c)
{
    $csvitem = $Banks[$c].'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ'
    $sharepointItem = $items[$l].FieldValues['bankCode']
    if([int]$csvitem -eq [int]$sharepointItem)
    {
        Take_Item_Index_Bank($Bank,$c,$bic,$name,$tel,$fax,$region,$website,$csvitem)
        $id= Get-PnPProperty -ClientObject $items[$l] -Property Id
       
        if($items[$l].FieldValues['bankName'] -ne $name){
           Set-PnPListItem -List "BANKS" -Identity $id -Values @{
               "bankName"= $name;
            }
        }
        if($items[$l].FieldValues['bankRegion'] -ne $region){
            Set-PnPListItem -List "BANKS" -Identity $id -Values @{
                "bankRegion"= $region;
            }
        }
        if($items[$l].FieldValues['bankBic'] -ne $bic){
            Set-PnPListItem -List "BANKS" -Identity $id -Values @{
                "branchAddress"= $address;
            }
        }
        if($items[$l].FieldValues['bankTel'] -ne $tel){
            Set-PnPListItem -List "BANKS" -Identity $id -Values-Values @{
                "bankTel"= $tel;
            }
        }
        if($items[$l].FieldValues['bankFax'] -ne $fax){
            Set-PnPListItem -List "BANKS" -Identity $id -Values @{
                "bankFax"= $fax;
            }
        }
        if($items[$l].FieldValues['bankWebSite'] -ne $website){
            Set-PnPListItem -List "BANKS" -Identity $id -Values @{
                "bankWebSite"= $website;
            }
        }
        
        $l= $l + 1
        $c= $c + 1
        }
    elseif([int]$csvitem -gt [int]$sharepointItem)
    {
        $id= Get-PnPProperty -ClientObject $items[$l] -Property Id
        Remove-PnPListItem -List "BANKS" -Identity $id -Force
        $l= $l + 1
    }
    else
    {
        Take_Item_Index_Bank($Bank,$c,$bic,$name,$tel,$fax,$region,$website)
        Add-PnPListItem -List "BANKS" -Values @{
            "bankCode"=$csvitem;
            "bankBic"=$bic;                                                   
            "bankName"= $name;                    
            "bankTel"= $tel;
            "bankFax"= $fax;
            "bankRegion"= $region;
            "bankWebSite"= $website;
            }

        $c=$c+1
      }
    
    if($l -eq $stop_l)
    {
        break
    }
}

#delete all extra items of sharepoint list
if($c -eq $stop_c)
{
    for ( $i=$l; $i -lt $stop_l; $i++)
    {
        $id= Get-PnPProperty -ClientObject $items[$i] -Property Id
        Remove-PnPListItem -List "BANKS" -Identity $id -Force
    }
}

#add all extra items of csv
if($l -eq $stop_l)
{
    for($i=$c; $i -lt $stop_c; $i++)
    {
        $code=$Banks[$i].'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ'
        Take_Item_Index_Bank($Bank,$i,$bic,$name,$tel,$fax,$region,$website)

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
    if([int]$csvitem -eq [int]$sharepointItem)
    {
        #if items is equals go to next
        $l= $l + 1
        $c= $c + 1
    }
    elseif([int]$csvitem -gt [int]$sharepointItem)
    {
        #if hebic of csv is greater than sharepoint's we dont need sharepoint's item anymore
        $id= Get-PnPProperty -ClientObject $items_Branch[$l] -Property Id
        Remove-PnPListItem -List "BRANCHES" -Identity $id -Force
        $l= $l + 1
    }
    else
    {
        #else add the new item on sharepoint
        Take_Item_Index_Branch($Branch,$c,$name,$region,$address,$tel,$community,$municipality,$tk1,$tk2)
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
        Take_Item_Index_Branch($Branch,$i,$name,$region,$address,$tel,$community,$municipality,$tk1,$tk2)
        Add-PnPListItem -List "BRANCHES" -Values @{
        "branchHebic"=$code;                                                   
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