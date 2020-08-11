$ver = $host | select version
if ($ver.Version.Major -gt 1) {$host.Runspace.ThreadOptions = "ReuseThread"} 
if ((Get-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue) -eq $null) {
    Add-PSSnapin "Microsoft.SharePoint.PowerShell"
}
#initializations
$Url = "http://swisspost.spdev.local"
$propCsv="C:\Users\spsetup\Desktop\Properties.csv"

$props = import-csv -Delimiter ";" -Path $propCsv -Encoding Unicode

foreach ($prop in $props){
    #Search service app
    $CrawledPropertyName=$prop.'Column1';
    $ManagedPropertyName=$prop.'Column2';
    $ssa= Get-SPEnterpriseSearchServiceApplication -Identity 'Search Service Application'
    $property = Get-SPEnterpriseSearchMetadataCrawledProperty -SearchApplication $ssa -name $CrawledPropertyName


    
    # If the Crawled Property is present  
    if ($property) {  
            #Create Managed Property  
            New-SPEnterpriseSearchMetadataManagedProperty -Name $ManagedPropertyName -SearchApplication $ssa -Type 1 -FullTextQueriable $true -Queryable $true -Retrievable $true -EnabledForQueryIndependentRank $true 
            $ManagedProperty = Get-SPEnterpriseSearchMetadataManagedProperty -SearchApplication $ssa -Identity $ManagedPropertyName  
            # Map the Managed Property with the Crawled Property 
            New-SPEnterpriseSearchMetadataMapping -SearchApplication $ssa -ManagedProperty $ManagedProperty –CrawledProperty $property  
            $ManagedProperty.Sortable = $true
            $ManagedProperty.Refinable = $true
            $ManagedProperty.Update()
    }  
}