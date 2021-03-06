﻿<#
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
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$Url,
        
    [Parameter(Mandatory)]
    [string]$branchesCSV,

    [Parameter(Mandatory)]
    [string]$banksCSV
)
#---------------------------------------------------------[Initialisations]--------------------------------------------------------
#$Url = "http://mynbgportal/sites/hebic/"
#$branchesCSV = "C:\Users\e82276\Desktop\branches. 31 12 19.csv"
#$banksCSV = "C:\Users\e82276\Desktop\banks. 31 12 19.csv"

#----------------------------------------------------------[Declarations]----------------------------------------------------------
#connect
#$UserName = "e82331"
#$pwd = "Y?Ugjxgar"
#[SecureString]$SecurePwd = ConvertTo-SecureString $pwd -AsPlainText -Force
#$Credentials = New-Object System.Management.Automation.PSCredential($UserName,$SecurePwd)


#-----------------------------------------------------------[Functions]------------------------------------------------------------

function Take_Item_Index_Bank($bank_to_check,$csvitem1)
{
    if(!$bank_to_check.'SWIFT BIC' )
    {
        $csvitem1[0] = "-"
    }
    else
    {
        $csvitem1[0] = $bank_to_check.'SWIFT BIC'
    }
	
	if(!$bank_to_check.'ΕΠΙΣΗΜΗ ΟΝΟΜΑΣΙΑ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)' )
    {
        $csvitem1[1] = "-"
    }
    else
    {
        $csvitem1[1] = $bank_to_check.'ΕΠΙΣΗΜΗ ΟΝΟΜΑΣΙΑ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)'
    }
	
	if(!$bank_to_check.'ΤΗΛΕΦΩΝΙΚΟ ΚΕΝΤΡΟ' )
    {
        $csvitem1[2]= "-"
    }
    else
    {
        $csvitem1[2] = $bank_to_check.'ΤΗΛΕΦΩΝΙΚΟ ΚΕΝΤΡΟ'
    }
	
	if(!$bank_to_check.'ΚΕΝΤΡΙΚΟFAX' )
    {
        $csvitem1[3] = "-"
    }
    else
    {
        $csvitem1[3] = $bank_to_check.'ΚΕΝΤΡΙΚΟFAX'
    }
	
	if(!$bank_to_check.'ΔΙΕΥΘΥΝΣΗ ΕΔΡΑΣ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)' )
    {
        $csvitem1[4] = "-"
    }
    else
    {
        $csvitem1[4] = $bank_to_check.'ΔΙΕΥΘΥΝΣΗ ΕΔΡΑΣ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)'
    }
	
	if(!$bank_to_check.'ΗΛΕΚΤΡΟΝΙΚΗ ΔΙΕΥΘΥΝΣΗ-URL' )
    {
        $csvitem1[5] = "-"
    }
    else
    {
        $csvitem1[5]= $bank_to_check.'ΗΛΕΚΤΡΟΝΙΚΗ ΔΙΕΥΘΥΝΣΗ-URL'
    }
    
}
function Take_Item_Index_Branch($branch_to_check,$csvitem2)
{

    $csvitem2[0] = $branch_to_check.'ΓΡΑΦΕΙΟ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
    $csvitem2[1] = $branch_to_check.'ΔΙΑΔΡΟΜΗ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ';
	
	if(!$branch_to_check.'ΟΝΟΜΑΣΙΑ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)' )
    {
        $csvitem2[2] = "-"
    }
    else
    {
        $csvitem2[2] = $branch_to_check.'ΟΝΟΜΑΣΙΑ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)'
    }
	
	if(!$branch_to_check.'ΟΝΟΜΑΣΙΑ ΤΟΠΟΘΕΣΙΑΣ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)' )
    {
        $csvitem2[3] = "-"
    }
    else
    {
        $csvitem2[3] = $branch_to_check.'ΟΝΟΜΑΣΙΑ ΤΟΠΟΘΕΣΙΑΣ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)'
    }
	
	if(!$branch_to_check.'Διεύθυνση ΕΛΛΗΝΙΚΑ' )
    {
        $csvitem2[4] = "-"
    }
    else
    {
        $csvitem2[4] = $branch_to_check.'Διεύθυνση ΕΛΛΗΝΙΚΑ'
    }
		
	if(!$branch_to_check.'ΑΡΙΘΜΟΣ ΤΗΛΕΦΩΝΟΥ' )
    {
        $csvitem2[5] = "-"
    }
    else
    {
        $csvitem2[5]= $branch_to_check.'ΑΡΙΘΜΟΣ ΤΗΛΕΦΩΝΟΥ'
    }
			
	if(!$branch_to_check.'ΤΑΧΥΔΡΟΜΙΚΗ ΠΕΡΙΟΧΗ (ΕΛΛΗΝΙΚΑ)' )
    {
        $csvitem2[6] = "-"
    }
    else
    {
        $csvitem2[6] = $branch_to_check.'ΤΑΧΥΔΡΟΜΙΚΗ ΠΕΡΙΟΧΗ (ΕΛΛΗΝΙΚΑ)'
    }	
	
	if(!$branch_to_check.'ΔΗΜΟΣ/ ΚΟΙΝΟΤΗΤΑ' )
    {
        $csvitem2[7] = "-"
    }
    else
    {
        $csvitem2[7] = $branch_to_check.'ΔΗΜΟΣ/ ΚΟΙΝΟΤΗΤΑ'
    }
}
#-----------------------------------------------------------[Execution]------------------------------------------------------------
Connect-PnPOnline -Url $Url -Credentials $Credentials

#======================================================================================================================================================
$items = Get-PnPListItem -List “BANKS” -Query "<View><Query><OrderBy><FieldRef Name='bankCode' Ascending='True' /></OrderBy></Query></View>"
$Banks = import-csv -Delimiter ";" -Path $banksCSV -Encoding Default | sort 'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ'
#======================================================================================================================================================
$Branch = Import-Csv -Delimiter ";" -Path $branchesCSV -Encoding Default | sort 'ΚΩΔΙΚΟΣ HEBIC'
$items_Branch = Get-PnPListItem -List “BRANCHES” -Query "<View><Query><OrderBy><FieldRef Name='branchHebic' Ascending='True' /></OrderBy></Query></View>"

#start to import on banks
$l=0
$c=0
$stop_c=$Banks.Count
$stop_l=$items.Count

while($c -lt $stop_c)
{
    $csvitem = $Banks[$c].'ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ'
    $csvitem1=@("","","","","","")
    $sharepointItem = $items[$l].FieldValues['bankCode']
    $bank_to_check=$Banks[$c]
    if([int]$csvitem -eq [int]$sharepointItem)
    {
        
        Take_Item_Index_Bank $bank_to_check $csvitem1
        $id= Get-PnPProperty -ClientObject $items[$l] -Property Id
       
        if($items[$l].FieldValues['bankName'] -ne $csvitem1[1]){
           Set-PnPListItem -List "BANKS" -Identity $id -Values @{
               "bankName"= $csvitem1[1]
            }
        }
        if($items[$l].FieldValues['bankRegion'] -ne $csvitem1[4]){
            Set-PnPListItem -List "BANKS" -Identity $id -Values @{
                "bankRegion"= $csvitem1[4]
            }
        }
        if($items[$l].FieldValues['bankBic'] -ne $csvitem1[0]){
            Set-PnPListItem -List "BANKS" -Identity $id -Values @{
                "bankBic"= $csvitem1[0]
            }
        }
        if($items[$l].FieldValues['bankTel'] -ne $csvitem1[2]){
            Set-PnPListItem -List "BANKS" -Identity $id -Values @{
                "bankTel"= $csvitem1[2]
            }
        }
        if($items[$l].FieldValues['bankFax'] -ne $csvitem1[3]){
            Set-PnPListItem -List "BANKS" -Identity $id -Values @{
                "bankFax"= $csvitem1[3]
            }
        }
        if($items[$l].FieldValues['bankWebSite'] -ne $csvitem1[5]){
            Set-PnPListItem -List "BANKS" -Identity $id -Values @{
                "bankWebSite"= $csvitem1[5]
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
        Take_Item_Index_Bank $bank_to_check $csvitem1
        Add-PnPListItem -List "BANKS" -Values @{
            "bankCode"=$csvitem;
            "bankBic"=$csvitem1[0];                                                   
            "bankName"= $csvitem1[1];                    
            "bankTel"= $csvitem1[2];
            "bankFax"= $csvitem1[3];
            "bankRegion"= $csvitem1[4];
            "bankWebSite"= $csvitem1[5];
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
        $bank_to_check=$Banks[$i]
        Take_Item_Index_Bank $bank_to_check,$cvitem1

        Add-PnPListItem -List "BANKS" -Values @{
        "bankCode"=$code;
        "bankBic"=$csvitem1[0];                                                   
        "bankName"= $csvitem1[1];                    
        "bankTel"= $csvitem1[2];
        "bankFax"= $csvitem1[3];
        "bankRegion"= $csvitem1[4];
        "bankWebSite"= $csvitem1[5];
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
    $csvitem2=@("","","","","","","","")
    $id= Get-PnPProperty -ClientObject $items_Branch[$l] -Property Id

    #check the 2 hebics
    if([int]$csvitem -eq [int]$sharepointItem)
    {
        $branch_to_check=$Branch[$c]
        Take_Item_Index_Branch $branch_to_check $csvitem2
        if($items_Branch[$l].FieldValues['branchName'] -ne $csvitem2[2] ){
            Set-PnPListItem -List "BRANCHES" -Identity $id -Values @{
                "branchName"= $csvitem2[2];
            }
        }
        if($items_Branch[$l].FieldValues['branchRegion'] -ne $csvitem2[3]){
            Set-PnPListItem -List "BRANCHES" -Identity $id -Values @{
                "branchRegion"= $csvitem2[3];
            }
        }
        if($items_Branch[$l].FieldValues['branchAddress'] -ne $csvitem2[4]){
            Set-PnPListItem -List "BRANCHES" -Identity $id -Values @{
                "branchAddress"= $csvitem2[4];
            }
        }
        if($items_Branch[$l].FieldValues['branchTel'] -ne $csvitem2[5]){
            Set-PnPListItem -List "BRANCHES" -Identity $id -Values @{
                "branchTel"= $csvitem2[5];
            }
        }
       if($items_Branch[$l].FieldValues['branchCommunity'] -ne $csvitem2[6]){
            Set-PnPListItem -List "BRANCHES" -Identity $id -Values @{
                "branchCommunity"= $csvitem2[6];
            }
        }
       if($items_Branch[$l].FieldValues['branchMunicipality'] -ne $csvitem2[7]){
            Set-PnPListItem -List "BRANCHES" -Identity $id -Values @{
                "branchMunicipality"= $csvitem2[7];
            }
        }
        $zip=$csvitem2[0]+$csvitem2[1]
       if($items_Branch[$l].FieldValues['branchZipCode'] -ne $zip){
            Set-PnPListItem -List "BRANCHES" -Identity $id -Values @{
                "branchZipCode"= $zip;
            }
        }
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
        $branch_to_check=$Branch[$c]
        Take_Item_Index_Branch $branch_to_check $csvitem2
        Add-PnPListItem -List "BRANCHES" -Values @{
        "branchHebic"=$csvitem;                                                   
        "branchName"= $csvitem2[2];                    
        "branchRegion"=$csvitem2[3];
        "branchAddress"= $csvitem2[4];
        "branchTel"= $csvitem2[5];
        "branchCommunity"= $csvitem2[6];
        "branchMunicipality"= $csvitem2[7];
        "branchZipCode"= $csvitem2[0]+$csvitem2[1];
        }
        $c=$c+1 
    }
    if($l -eq $stop_l)
    {
        break
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
        $branch_to_check=$Branch[$i]
        Take_Item_Index_Branch $branch_to_check $csvitem2
        Add-PnPListItem -List "BRANCHES" -Values @{
        "branchHebic"=$code;                                                   
        "branchName"= $csvitem2[2];                    
        "branchRegion"=$csvitem2[3];
        "branchAddress"= $csvitem2[4];
        "branchTel"= $csvitem2[5];
        "branchCommunity"= $csvitem2[6];
        "branchMunicipality"= $csvitem2[7];
        "branchZipCode"= $csvitem2[0]+$csvitem2[1];
        }
    }
}

#end of branches
#=================================================================================================