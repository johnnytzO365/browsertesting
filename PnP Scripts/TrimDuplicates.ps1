$inputPath = "C:\Users\KyriakiBousiou\Desktop\NBGLinks-duplicates.txt"
$outputPath = "C:\Users\KyriakiBousiou\Desktop\NBGLinks.txt"

gc $inputPath | sort -CaseSensitive | get-unique > $outputPath