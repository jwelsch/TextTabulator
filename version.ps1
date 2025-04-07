param(
    [Parameter(Mandatory, Position=0, HelpMessage="Specifies which command to run. One of 'list' or 'change'. The 'list' command displays all found versions. The 'change' command changes one version to another.")]
    [string] $Command,

    [Parameter(ParameterSetName="change", Position=1, HelpMessage="Which version to change.")]
    [string] $FromVersion,

    [Parameter(ParameterSetName="change", Position=2, HelpMessage="What the new version should be.")]
    [string] $ToVersion,

    [Parameter(HelpMessage="Directory in which to search for .CSPROJ files. Omit to search the current directory.")]
    [string] $Directory,

    [Parameter(HelpMessage="Include to search subdirectories, omit to only search the top level directory.")]
    [switch] $Recurse
)

function DoVersionsMatch(
    [Parameter(Mandatory, Position=0)]
    [System.Version] $Version1,

    [Parameter(Mandatory, Position=1)]
    [System.Version] $Version2
)
{
    if ($Version1.Major -ne $Version2.Major)
    {
        #Write-Host "Major didn't match - 1: $($Version1.Major), 2: $($Version2.Major)";
        return $False;
    }

    if ($Version1.Minor -ne $Version2.Minor -and !($Version1.Minor -lt 0 -and $Version2.Minor -eq 0) -and !($Version1.Minor -eq 0 -and $Version2.Minor -lt 0))
    {
        #Write-Host "Minor didn't match - 1: $($Version1.Minor), 2: $($Version2.Minor)";
        return $False;
    }

    if ($Version1.Build -ne $Version2.Build -and !($Version1.Build -lt 0 -and $Version2.Build -eq 0) -and !($Version1.Build -eq 0 -and $Version2.Build -lt 0))
    {
        #Write-Host "Build didn't match - 1: $($Version1.Build), 2: $($Version2.Build)";
        return $False;
    }

    if ($Version1.Revision -ne $Version2.Revision -and !($Version1.Revision -lt 0 -and $Version2.Revision -eq 0) -and !($Version1.Revision -eq 0 -and $Version2.Revision -lt 0))
    {
        #Write-Host "Revision didn't match - 1: $($Version1.Revision), 2: $($Version2.Revision)";
        return $False;
    }

    #Write-Host "Versions matched - 1: $Version1, 2: $Version2";

    return $True;
}

function CountCharacter(
    [Parameter(Mandatory, Position=0)]
    [string] $Text,
    
    [Parameter(Mandatory, Position=1)]
    [char] $CharToCount
)
{
    $count = 0;

    foreach ($char in $Text.ToCharArray())
    {
        if ($char -eq $CharToCount)
        {
            $count++;
        }
    }

    return $count;
}

Set-Variable -Name "csProjFileExtension" -Value "csproj" -Option Constant

# Set the search directory properly and check that it exists.
$directory = $Directory
if ($directory -eq $null -or $directory -eq "")
{
    $directory = (Get-Location).Path;
    Write-Host "Directory was not specified, current directory '$directory' will be used.";
}
elseif (-not (Test-Path -Path $directory))
{
    throw "The directory '$directory' does not exist.";
}

# Get the CSPROJ files in the search directory.
$csProjFiles = Get-ChildItem -Path $directory -Filter "*.$csProjFileExtension" -Recurse:$Recurse.IsPresent | %{$_.FullName};

if ($Command.ToLower() -eq "change")
{
    $fromVersionObj = New-Object -TypeName "System.Version" -ArgumentList $FromVersion
    $toVersionObj = New-Object -TypeName "System.Version" -ArgumentList $ToVersion
}

foreach ($csProjFile in $csProjFiles)
{
    Write-Host "$csProjFile";

    [xml]$xml = Get-Content -Path $csProjFile;

    if ($Command.ToLower() -eq "list")
    {
        Write-Host "  AssemblyVersion: $($xml.Project.PropertyGroup.AssemblyVersion)";
        Write-Host "  FileVersion: $($xml.Project.PropertyGroup.FileVersion)";
        Write-Host "  Version: $($xml.Project.PropertyGroup.Version)";
        continue;
    }

    $isDirty = $False;

    if (![string]::IsNullOrEmpty($xml.Project.PropertyGroup.AssemblyVersion) -and [char]::IsDigit($xml.Project.PropertyGroup.AssemblyVersion[0]))
    {
        $version = New-Object -TypeName "System.Version" -ArgumentList $xml.Project.PropertyGroup.AssemblyVersion;

        if (($xml.Project.PropertyGroup.AssemblyVersion -ne $null) -and ($xml.Project.PropertyGroup.AssemblyVersion -ne "") -and (DoVersionsMatch $version $fromVersionObj))
        {
            $xml.Project.PropertyGroup.AssemblyVersion = $ToVersion;
            $isDirty = $True;
            Write-Host "  AssemblyVersion: $FromVersion => $($xml.Project.PropertyGroup.AssemblyVersion)";
        }
    }

    if (![string]::IsNullOrEmpty($xml.Project.PropertyGroup.FileVersion) -and [char]::IsDigit($xml.Project.PropertyGroup.FileVersion[0]))
    {
        $version = New-Object -TypeName "System.Version" -ArgumentList $xml.Project.PropertyGroup.FileVersion;

        if (($xml.Project.PropertyGroup.FileVersion -ne $null) -and ($xml.Project.PropertyGroup.FileVersion -ne "") -and (DoVersionsMatch $version $fromVersionObj))
        {
            $xml.Project.PropertyGroup.FileVersion = $ToVersion;
            $isDirty = $True;
            Write-Host "  FileVersion: $FromVersion => $($xml.Project.PropertyGroup.FileVersion)";
        }
    }

    if (![string]::IsNullOrEmpty($xml.Project.PropertyGroup.Version) -and [char]::IsDigit($xml.Project.PropertyGroup.Version[0]))
    {
        $version = New-Object -TypeName "System.Version" -ArgumentList $xml.Project.PropertyGroup.Version;

        if (($xml.Project.PropertyGroup.Version -ne $null) -and ($xml.Project.PropertyGroup.Version -ne "") -and (DoVersionsMatch $version $fromVersionObj))
        {
            $xml.Project.PropertyGroup.Version = "$($toVersionObj.Major).$($toVersionObj.Minor).$($toVersionObj.Build)";
            $isDirty = $True;
            Write-Host "  Version: $($fromVersionObj.Major).$($fromVersionObj.Minor).$($fromVersionObj.Build) => $($xml.Project.PropertyGroup.Version)";
        }
    }

    if ($isDirty)
    {
        $xml.Save($csProjFile);
        $madeChanges = $True;
    }
}
