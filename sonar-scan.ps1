Get-Content .env | ForEach-Object {
    $name, $value = $_.split('=')
    if ($name -and $value) {
        [System.Environment]::SetEnvironmentVariable($name, $value, [System.EnvironmentVariableTarget]::Process)
        Write-Host "Setting environment variable $name to $value"
    }
}

dotnet-coverage collect "dotnet test ./tests/CC.CSX.Tests/" -f xml -o "general-coverage.xml" 
dotnet-coverage collect "dotnet test ./tests/CC.CSX.Htmx.Tests/" -f xml -o "htmx-coverage.xml"

SonarScan-Dotnet `
    -BuildScript { 
        Build-Dotnet -ProjectOrSlnPath "htnet.sln" -BuildArgs @("--no-incremental") -DryRun $false
        dotnet test -t
    } `
    -AdditionalOptions @(
        "/d:sonar.cs.vscoveragexml.reportsPaths=*-coverage.xml"
    ) `
    -DryRun $false `
    -SonarToken $env:SONAR_TOKEN `
    -SonarKey $env:SONAR_PROJECT_KEY `
    -SonarOrganization $env:SONAR_ORGANIZATION `
    -SonarHostUrl $env:SONAR_HOST_URL
