$files = Get-ChildItem -filter *.doc -file | Select FullName
foreach ($file in $files) {
	$fileName = $file.FullName
	$command = "soffice --convert-to docx `"$fileName`""
	Invoke-Expression $command
}