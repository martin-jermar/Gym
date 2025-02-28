# Spustí dotnet format
dotnet format

# Pokud formátování selže, přeruší commit
if ($LASTEXITCODE -ne 0) {
    Write-Host "Code formatting failed! Commit aborted."
    exit 1
}

# Spustí dotnet build
dotnet build --no-restore

# Pokud build selže, přeruší commit
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed! Commit aborted."
    exit 1
}

# Pokračuje v commitu, pokud vše proběhlo v pořádku
exit 0
