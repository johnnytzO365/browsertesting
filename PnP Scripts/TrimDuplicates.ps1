$Path = "C:\Users\e82331\Desktop\NBGLinksduplicates.txt"
$TempPath = "C:\Users\e82331\Desktop\Temp.txt"

gc $inputPath | sort -CaseSensitive | get-unique > $TempPath
Copy-Item $TempPath -Destination $inputPath
Remove-Item $TempPath