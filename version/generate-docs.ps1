# Dependent on XMLDoc2Markdown dotnet tool:
#   $> dotnet tool install -g XMLDoc2Markdown

param(
    [string]$Configuration = "Debug",
    [string]$User
)

Remove-Item -Path "..\docs" -Recurse -Force -ErrorAction SilentlyContinue

$directoryNames = (Get-ChildItem -Path "..\src" -Directory).Name

if ([string]::IsNullOrEmpty($User)) {
    $User = [System.Environment]::UserName
}

$nugetPackagesPath = "C:\Users\$User\.nuget\packages\"

# Define packages that need to be copied for each project
$projectDependencies = @{
    "TextTabulator.Adapters.Json"     = @("System.Text.Json")
    "TextTabulator.Adapters.CsvHelper" = @("CsvHelper")
    "TextTabulator.Adapters.YamlDotNet" = @("YamlDotNet")
    "TextTabulator.Adapters.MLDotNet" = @("Microsoft.ML")
}

# Function to copy required dependencies recursively
function Copy-Dependencies {
    param(
        [string]$DllDir,
        [string]$DepsJsonName,
        [string[]]$DependenciesToCopy
    )
    
    $depsPath = "$DllDir\$DepsJsonName.deps.json"
    $depsContent = Get-Content $depsPath | ConvertFrom-Json
    $copiedDlls = @()
    $processedPackages = @{}
    
    # Helper function to recursively process a package and its dependencies
    function Process-Package {
        param(
            [string]$PackageName,
            [PSObject]$DepsObject,
            [hashtable]$Processed
        )
        
        # Avoid processing the same package twice
        if ($Processed.ContainsKey($PackageName)) {
            return @()
        }
        $Processed[$PackageName] = $true
        
        $localCopiedDlls = @()
        $packageDlls = @()
        $packageVersion = $null
        
        # Search for the package in targets
        foreach ($target in $DepsObject.targets.PSObject.Properties) {
            foreach ($package in $target.Value.PSObject.Properties) {
                if ($package.Name -like "$PackageName/*") {
                    $parts = $package.Name -split '/'
                    $packageVersion = $parts[1]
                    
                    # Get DLLs for this package
                    if ($package.Value.runtime) {
                        $runtimeDlls = $package.Value.runtime.PSObject.Properties | Where-Object { $_.Name -like "*.dll" }
                        if ($runtimeDlls) {
                            if ($runtimeDlls -is [array]) {
                                $packageDlls += $runtimeDlls | ForEach-Object { $_.Name }
                            } else {
                                $packageDlls += $runtimeDlls.Name
                            }
                        }
                    }
                    
                    # Get dependencies of this package
                    if ($package.Value.dependencies) {
                        $depsList = $package.Value.dependencies.PSObject.Properties
                        foreach ($dep in $depsList) {
                            $depPackageName = $dep.Name -split '/' | Select-Object -First 1
                            # Recursively process transitive dependencies
                            $localCopiedDlls += Process-Package -PackageName $depPackageName -DepsObject $DepsObject -Processed $Processed
                        }
                    }
                    break
                }
            }
            if ($packageVersion) { break }
        }
        
        # Copy all DLLs for this package
        foreach ($dll in $packageDlls) {
            $sourcePath = "$nugetPackagesPath$PackageName\$packageVersion\$dll"
            if (Test-Path $sourcePath) {
                Write-Host "Copying $dll to $DllDir"
                Copy-Item $sourcePath $DllDir
                # Extract just the filename from the path
                $filename = Split-Path -Leaf $dll
                $localCopiedDlls += $filename
            }
            else {
                Write-Warning "Could not find $dll at expected path: $sourcePath"
            }
        }
        
        return $localCopiedDlls
    }
    
    # Process each top-level dependency
    foreach ($depName in $DependenciesToCopy) {
        $copiedDlls += Process-Package -PackageName $depName -DepsObject $depsContent -Processed $processedPackages
    }
    
    return $copiedDlls
}

foreach ($name in $directoryNames) {
    if ($name -eq "TextTabulator.Cli") {
        continue
    }

    $dllDir = "..\src\$name\bin\$Configuration\netstandard2.1"

    # Check if this project has dependencies to copy
    if ($projectDependencies.ContainsKey($name)) {
        $copiedDlls = Copy-Dependencies -DllDir $dllDir -DepsJsonName $name -DependenciesToCopy $projectDependencies[$name]
        
        # Run xmldoc2md
        xmldoc2md "$dllDir\$name.dll" -o "..\docs"
        
        # Clean up copied dependencies
        foreach ($depDll in $copiedDlls) {
            $depDllPath = "$dllDir\$depDll"
            if (Test-Path $depDllPath) {
                Remove-Item $depDllPath
            }
        }
    }
    else {
        xmldoc2md "$dllDir\$name.dll" -o "..\docs"
    }
}
