
write-host "**************************" -foreground "Cyan"
write-host "*   Packaging to nuget   *" -foreground "Cyan"
write-host "**************************" -foreground "Cyan"

#$location  = "C:\Sources\NoSqlRepositories";
$location  = $env:APPVEYOR_BUILD_FOLDER

$locationNuspec = $location + "\nuspec"
$locationNuspec
	
Set-Location -Path $locationNuspec

$strPath = $location + '\Src\Kit.Core45\bin\Release\Kit.Core45.dll'

$VersionInfos = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($strPath)
$ProductVersion = $VersionInfos.ProductVersion
write-host "Product version : " $ProductVersion -foreground "Green"

write-host "Packaging to nuget..." -foreground "Magenta"

write-host "Update nuspec versions" -foreground "Green"

write-host "Update nuspec versions Unofficial.HockeySDK.Core.nuspec" -foreground "DarkGray"
$nuSpecFile =  $locationNuspec + '\Unofficial.HockeySDK.Core.nuspec'
(Get-Content $nuSpecFile) | 
Foreach-Object {$_ -replace "{BuildNumberVersion}", "$ProductVersion" } | 
Set-Content $nuSpecFile

write-host "Update nuspec versions Unofficial.HockeySDK.WPF.nuspec" -foreground "DarkGray"
$nuSpecFile =  $locationNuspec + '\Unofficial.HockeySDK.WPF.nuspec'
(Get-Content $nuSpecFile) | 
Foreach-Object {$_ -replace "{BuildNumberVersion}", "$ProductVersion" } | 
Set-Content $nuSpecFile

write-host "Generate nuget packages" -foreground "Green"

write-host "Generate nuget package for Unofficial.HockeySDK.Core.nuspec" -foreground "DarkGray"
.\NuGet.exe pack Unofficial.HockeySDK.Core.nuspec

write-host "Generate nuget package for Unofficial.HockeySDK.WPF.nuspec" -foreground "DarkGray"
.\NuGet.exe pack Unofficial.HockeySDK.WPF.nuspec

$apiKey = $env:NuGetApiKey
	
write-host "Publish nuget packages" -foreground "Green"

write-host Unofficial.HockeySDK.Core.$ProductVersion.nupkg -foreground "DarkGray"
.\NuGet push Unofficial.HockeySDK.Core.$ProductVersion.nupkg -Source https://www.nuget.org/api/v2/package -ApiKey $apiKey

write-host Unofficial.HockeySDK.WPF.$ProductVersion.nupkg -foreground "DarkGray"
.\NuGet push Unofficial.HockeySDK.WPF.$ProductVersion.nupkg -Source https://www.nuget.org/api/v2/package -ApiKey $apiKey