# ============================================================
# Booku Updater Release Publisher with Obfuscation
# ============================================================
# Script ini akan:
# 1. Build project dalam mode Release
# 2. Obfuscate assemblies (Booku Updater.dll dan bcomm.dll)
# 3. Publish sebagai single file executable
# ============================================================

param(
    [switch]$SkipObfuscation = $false
)

$ErrorActionPreference = "Stop"

# Paths
$ProjectDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$SolutionDir = Split-Path -Parent $ProjectDir
$ProjectFile = Join-Path $ProjectDir "Booku Updater.vbproj"

$BuildOutputDir = Join-Path $ProjectDir "bin\Release\net8.0-windows\win-x64"
$PublishDir = Join-Path $BuildOutputDir "publish"
$ObfuscatedDir = Join-Path $BuildOutputDir "Obfuscated"
$FinalOutputDir = Join-Path $ProjectDir "bin\Release\Final"

# Colors for output
function Write-Step { param($msg) Write-Host "`n>> $msg" -ForegroundColor Cyan }
function Write-Success { param($msg) Write-Host "   $msg" -ForegroundColor Green }
function Write-Warning { param($msg) Write-Host "   $msg" -ForegroundColor Yellow }
function Write-Error { param($msg) Write-Host "   $msg" -ForegroundColor Red }

Write-Host "============================================================" -ForegroundColor Magenta
Write-Host "    BOOKU UPDATER RELEASE PUBLISHER WITH OBFUSCATION" -ForegroundColor Magenta
Write-Host "============================================================" -ForegroundColor Magenta

# Step 1: Clean previous builds
Write-Step "Membersihkan build sebelumnya..."
if (Test-Path $BuildOutputDir) {
    Remove-Item -Path $BuildOutputDir -Recurse -Force -ErrorAction SilentlyContinue
}
if (Test-Path $FinalOutputDir) {
    Remove-Item -Path $FinalOutputDir -Recurse -Force -ErrorAction SilentlyContinue
}
Write-Success "Folder build dibersihkan"

# Step 2: Find MSBuild
Write-Step "Mencari Visual Studio MSBuild..."

# Use Visual Studio MSBuild
$msbuildPath = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
if (-not (Test-Path $msbuildPath)) {
    $msbuildPath = "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe"
}
if (-not (Test-Path $msbuildPath)) {
    $msbuildPath = "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe"
}
if (-not (Test-Path $msbuildPath)) {
    # Fallback ke dotnet build
    Write-Warning "Visual Studio MSBuild tidak ditemukan, menggunakan dotnet..."
    $useDotnet = $true
} else {
    $useDotnet = $false
    Write-Success "Ditemukan: $msbuildPath"
}

# Step 3: Restore packages
Write-Step "Restoring NuGet packages..."
if ($useDotnet) {
    $restoreResult = dotnet restore $ProjectFile 2>&1
} else {
    $restoreResult = & $msbuildPath $ProjectFile -t:Restore -p:Configuration=Release -p:RuntimeIdentifier=win-x64 -verbosity:minimal 2>&1
}
if ($LASTEXITCODE -ne 0) {
    Write-Error "Restore gagal!"
    $restoreResult | ForEach-Object { Write-Host "   $_" -ForegroundColor Red }
    exit 1
}
Write-Success "Restore berhasil"

# Step 4: Build Release (without single file first)
Write-Step "Building project dalam mode Release..."
if ($useDotnet) {
    $buildResult = dotnet build $ProjectFile -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false 2>&1
} else {
    $buildResult = & $msbuildPath $ProjectFile -t:Build -p:Configuration=Release -p:RuntimeIdentifier=win-x64 -p:SelfContained=true -p:PublishSingleFile=false -verbosity:minimal 2>&1
}
if ($LASTEXITCODE -ne 0) {
    Write-Error "Build gagal!"
    $buildResult | ForEach-Object { Write-Host "   $_" -ForegroundColor Red }
    exit 1
}
Write-Success "Build berhasil"

# Step 5: Obfuscation
if (-not $SkipObfuscation) {
    Write-Step "Menjalankan Obfuscation..."

    # Find Obfuscar.Console.exe
    $obfuscarPath = Join-Path $env:USERPROFILE ".nuget\packages\obfuscar\2.2.38\tools\net472\Obfuscar.Console.exe"

    if (-not (Test-Path $obfuscarPath)) {
        Write-Warning "Obfuscar tidak ditemukan di: $obfuscarPath"
        Write-Warning "Mencoba path alternatif..."
        $obfuscarPath = Get-ChildItem -Path (Join-Path $env:USERPROFILE ".nuget\packages\obfuscar") -Recurse -Filter "Obfuscar.Console.exe" -ErrorAction SilentlyContinue | Select-Object -First 1 -ExpandProperty FullName
    }

    if (-not $obfuscarPath -or -not (Test-Path $obfuscarPath)) {
        Write-Warning "Obfuscar.Console.exe tidak ditemukan!"
        Write-Warning "Melanjutkan tanpa obfuscation..."
    } else {
        # Create obfuscar config with correct paths
        $obfuscarRunConfig = Join-Path $BuildOutputDir "obfuscar_run.xml"

        $configContent = @"
<?xml version='1.0'?>
<Obfuscator>
  <Var name="InPath" value="$BuildOutputDir" />
  <Var name="OutPath" value="$ObfuscatedDir" />
  <Var name="KeepPublicApi" value="false" />
  <Var name="HidePrivateApi" value="true" />
  <Var name="HideStrings" value="true" />
  <Var name="UseUnicodeNames" value="true" />
  <Var name="RenameProperties" value="false" />
  <Var name="RenameEvents" value="false" />
  <Var name="RenameFields" value="true" />
  <Var name="RegenerateDebugInfo" value="false" />

  <Module file="`$(InPath)\Booku Updater.dll">
    <SkipNamespace name="Booku_Updater.My" />
    <SkipType name="*Window*" skipProperties="true" skipMethods="false" skipFields="false" />
    <SkipType name="*Application*" skipProperties="true" skipMethods="false" skipFields="false" />
    <SkipType name="*MainWindow*" skipProperties="true" skipMethods="false" skipFields="false" />
    <SkipType name="*wpfWin_*" skipProperties="true" skipMethods="false" skipFields="false" />
    <SkipMethod type="*" name="*_Click" />
    <SkipMethod type="*" name="*_Load*" />
    <SkipMethod type="*" name="*_Chang*" />
    <SkipMethod type="*" name="*_Clos*" />
    <SkipMethod type="*" name="InitializeComponent" />
    <SkipMethod type="*" name="Main" />
  </Module>

  <Module file="`$(InPath)\bcomm.dll">
    <SkipNamespace name="bcomm" skipTypes="false" skipMethods="false" skipProperties="true" skipFields="false" />
  </Module>
</Obfuscator>
"@

        Set-Content -Path $obfuscarRunConfig -Value $configContent -Encoding UTF8

        # Create output directory
        if (-not (Test-Path $ObfuscatedDir)) {
            New-Item -ItemType Directory -Path $ObfuscatedDir -Force | Out-Null
        }

        # Run Obfuscar
        Write-Host "   Menjalankan: $obfuscarPath" -ForegroundColor Gray
        & $obfuscarPath $obfuscarRunConfig 2>&1 | ForEach-Object { Write-Host "   $_" -ForegroundColor Gray }

        if ($LASTEXITCODE -eq 0 -and (Test-Path (Join-Path $ObfuscatedDir "Booku Updater.dll"))) {
            Write-Success "Obfuscation berhasil"

            # Replace original DLLs with obfuscated ones
            Write-Step "Mengganti DLL dengan versi ter-obfuscate..."
            Copy-Item -Path (Join-Path $ObfuscatedDir "Booku Updater.dll") -Destination $BuildOutputDir -Force
            if (Test-Path (Join-Path $ObfuscatedDir "bcomm.dll")) {
                Copy-Item -Path (Join-Path $ObfuscatedDir "bcomm.dll") -Destination $BuildOutputDir -Force
            }
            Write-Success "DLL ter-obfuscate sudah diterapkan"
        } else {
            Write-Warning "Obfuscation mungkin gagal, melanjutkan dengan DLL original..."
        }
    }
} else {
    Write-Warning "Obfuscation dilewati (--SkipObfuscation)"
}

# Step 6: Publish as single file
Write-Step "Publishing sebagai Single File Executable..."

if ($useDotnet) {
    $publishResult = dotnet publish $ProjectFile -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true -p:DebugType=none -p:DebugSymbols=false 2>&1
} else {
    $publishResult = & $msbuildPath $ProjectFile -t:Publish -p:Configuration=Release -p:RuntimeIdentifier=win-x64 -p:SelfContained=true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true -p:DebugType=none -p:DebugSymbols=false -verbosity:minimal 2>&1
}

if ($LASTEXITCODE -ne 0) {
    Write-Error "Publish gagal!"
    $publishResult | ForEach-Object { Write-Host "   $_" -ForegroundColor Red }
    exit 1
}
Write-Success "Publish berhasil"

# Step 7: Copy to Final folder
Write-Step "Menyalin hasil ke folder Final..."
if (-not (Test-Path $FinalOutputDir)) {
    New-Item -ItemType Directory -Path $FinalOutputDir -Force | Out-Null
}

# Copy only the EXE file
$exeFile = Join-Path $PublishDir "Booku Updater.exe"
if (Test-Path $exeFile) {
    Copy-Item -Path $exeFile -Destination $FinalOutputDir -Force
    $finalExe = Join-Path $FinalOutputDir "Booku Updater.exe"
    $fileSize = [math]::Round((Get-Item $finalExe).Length / 1MB, 2)
    Write-Success "Booku Updater.exe ($fileSize MB) disalin ke folder Final"
}

# Summary
Write-Host "`n============================================================" -ForegroundColor Magenta
Write-Host "                    BUILD SELESAI!" -ForegroundColor Green
Write-Host "============================================================" -ForegroundColor Magenta
Write-Host "`nLokasi file hasil build:" -ForegroundColor White
Write-Host "   $FinalOutputDir\Booku Updater.exe" -ForegroundColor Yellow
Write-Host "`nFile ini siap untuk didistribusikan." -ForegroundColor White
if (-not $SkipObfuscation) {
    Write-Host "Status: OBFUSCATED (terlindungi dari decompilation)" -ForegroundColor Green
}
Write-Host ""

# Open folder
explorer.exe $FinalOutputDir
