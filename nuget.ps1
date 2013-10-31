del TsSoft.Orthography.*.nupkg
del *.nuspec
del .\TsSoft.Orthography\bin\Release\*.nuspec

function GetNodeValue([xml]$xml, [string]$xpath)
{
	return $xml.SelectSingleNode($xpath).'#text'
}

function SetNodeValue([xml]$xml, [string]$xpath, [string]$value)
{
	$node = $xml.SelectSingleNode($xpath)
	if ($node) {
		$node.'#text' = $value
	}
}

Remove-Item .\TsSoft.Orthography\bin -Recurse 
Remove-Item .\TsSoft.Orthography\obj -Recurse

$build = "c:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe ""TsSoft.Orthography\TsSoft.Orthography.csproj"" /p:Configuration=Release" 
Invoke-Expression $build

<#
$ReleasenotesDir = (resolve-path ".\").path
$ReleasenotesFile = $ReleasenotesDir+"ReleaseNotes.txt"
if (-not(Test-Path $ReleasenotesFile))
{
  New-Item $ReleasenotesFile -type file
}
$ReleasenotesFile = (resolve-path $ReleasenotesFile).path
#>
$ReleasenotesFile = (resolve-path ".\ReleaseNotes.txt").path

$Artifact = (resolve-path ".\TsSoft.Orthography\bin\Release\TsSoft.Orthography.dll").path

.\.nuget\nuget spec -F -A $Artifact

Copy-Item .\TsSoft.Orthography.nuspec.xml .\TsSoft.Orthography\bin\Release\TsSoft.Orthography.nuspec

$GeneratedSpecification = (resolve-path ".\TsSoft.Orthography.nuspec").path
$TargetSpecification = (resolve-path ".\TsSoft.Orthography\bin\Release\TsSoft.Orthography.nuspec").path
$encoding = [System.Text.Encoding]::GetEncoding(1251);
$releaseNotes = [IO.File]::ReadAllText($ReleasenotesFile, $encoding)
[xml]$srcxml = Get-Content $GeneratedSpecification
[xml]$destxml = Get-Content $TargetSpecification
$version = GetNodeValue $srcxml "//version"
SetNodeValue $destxml "//version" $version;
$value = GetNodeValue $srcxml "//description"
SetNodeValue $destxml "//description" $value;
$value = GetNodeValue $srcxml "//copyright"
SetNodeValue $destxml "//copyright" $value;
SetNodeValue $destxml "//releaseNotes" $releaseNotes;

$destxml.Save($TargetSpecification)

.\.nuget\nuget pack $TargetSpecification

<#
Файл с описанием изменений формируется нарастающим итогом, файлы не переносим
Rename-Item $ReleasenotesFile $ReleasenotesDir"ReleaseNotes.v"$version
New-Item $ReleasenotesFile -type file
#>

del *.nuspec
del .\TsSoft.Orthography\bin\Release\*.nuspec

exit
