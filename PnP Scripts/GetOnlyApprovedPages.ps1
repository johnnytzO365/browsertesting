$url ="https://authqa.nbg.gr/english/the-group/investor-relations/annual-report-offerring-circular"
Connect-PnPOnline -Url $url -UseWebLogin

$pages1 = Get-PnPListItem -List "Pages"
$pages2 = Get-PnPListItem -List "Pages" -Query "<View><Query><Where><Eq><FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>Approved</Value></Eq></Where></Query></View>"
$pages1.Count
$pages2.Count