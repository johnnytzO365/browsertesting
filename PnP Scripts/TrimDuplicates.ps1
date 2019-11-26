$inputPath = "C:\Users\KyriakiBousiou\Desktop\duplicates.txt"
$outputPath = "C:\Users\KyriakiBousiou\Desktop\unique.txt"

gc $inputPath | sort | get-unique > $outputPath