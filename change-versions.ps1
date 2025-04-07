param(
    [Parameter(Position=0, HelpMessage="Which version to change.")]
    [string] $FromVersion,

    [Parameter(Position=1, HelpMessage="What the new version should be.")]
    [string] $ToVersion
)

.\version.ps1 change $FromVersion $ToVersion -Directory .\src -Recurse
