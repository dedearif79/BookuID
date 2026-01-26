@echo off
REM ============================================
REM PUBLISH-RELEASE.bat
REM Build Booku Assistant - Release (Windows x64)
REM ============================================

echo.
echo ============================================
echo   BOOKU ASSISTANT - RELEASE BUILD
echo   Target: Windows x64
echo ============================================
echo.

REM Set working directory ke lokasi script
cd /d "%~dp0"

REM Clean previous build
echo [1/3] Cleaning previous build...
dotnet clean "Booku Assistant.vbproj" -c Release -v quiet
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Clean failed!
    pause
    exit /b 1
)
echo       Done.
echo.

REM Clean output folder
echo [2/4] Cleaning output folder...
if exist "bin\Release\Final" (
    rmdir /s /q "bin\Release\Final"
)
mkdir "bin\Release\Final"
echo       Done.
echo.

REM Publish Release (Single File + Self-Contained + Compressed)
echo [3/4] Publishing Release...
echo       - Single Executable: Yes
echo       - Self-Contained: Yes
echo       - Compression: Yes
echo       - Target: win-x64
echo.

dotnet publish "Booku Assistant.vbproj" -c Release -o "bin\Release\Final" -v minimal
if %ERRORLEVEL% NEQ 0 (
    echo.
    echo [ERROR] Publish failed!
    pause
    exit /b 1
)
echo.

REM Clean up extra files (keep only .exe)
echo [4/4] Cleaning up extra files...
del /q "bin\Release\Final\*.pdb" 2>nul
del /q "bin\Release\Final\*.json" 2>nul
del /q "bin\Release\Final\*.xml" 2>nul
del /q "bin\Release\Final\*.config" 2>nul
echo       Done.
echo.

REM Show output
echo Build completed successfully!
echo.
echo ============================================
echo   OUTPUT FILES:
echo ============================================
echo.
echo   Location:
echo   %~dp0bin\Release\Final\
echo.
echo   Executable:
echo   Booku Assistant.exe
echo.

REM Show file size
if exist "bin\Release\Final\Booku Assistant.exe" (
    for %%I in ("bin\Release\Final\Booku Assistant.exe") do (
        set SIZE=%%~zI
    )
    setlocal enabledelayedexpansion
    set /a SIZE_MB=!SIZE! / 1048576
    echo   Size: ~!SIZE_MB! MB
    endlocal
)
echo.

echo ============================================
echo   BUILD COMPLETED
echo ============================================
echo.
echo   Deploy:
echo   1. Copy 'Booku Assistant.exe' to target
echo   2. Run 'Booku Assistant.exe'
echo.
echo   Features:
echo   - Single Instance (Mutex protection)
echo   - Self-Contained (No .NET runtime required)
echo   - Compressed (Smaller file size)
echo.

pause
