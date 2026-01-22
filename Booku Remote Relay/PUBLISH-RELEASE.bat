@echo off
:: ============================================
:: PUBLISH-RELEASE.bat
:: Build Booku Remote Relay - Release
:: ============================================

title Booku Remote Relay - Release Build

echo.
echo ============================================
echo   BOOKU REMOTE RELAY - RELEASE BUILD
echo ============================================
echo.

:: Set working directory
cd /d "%~dp0"

:: Clean previous build
echo [1/3] Cleaning previous build...
dotnet clean "Booku Remote Relay.csproj" -c Release -v quiet
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Clean failed!
    pause
    exit /b 1
)
echo       Done.
echo.

:: Publish Release (Single File + Self-Contained + Trimmed)
echo [2/3] Publishing Release...
echo       - Single Executable: Yes
echo       - Self-Contained: Yes
echo       - IL Trimming: Yes
echo       - Compression: Yes
echo.
dotnet publish "Booku Remote Relay.csproj" -c Release -o "bin\Publish" -v minimal
if %ERRORLEVEL% NEQ 0 (
    echo.
    echo [ERROR] Publish failed!
    pause
    exit /b 1
)
echo.

:: Show output
echo [3/3] Build completed successfully!
echo.
echo ============================================
echo   OUTPUT FILES:
echo ============================================
echo.
echo   Location:
echo   %~dp0bin\Publish\
echo.
echo   Executable:
echo   Booku Remote Relay.exe
echo.

:: Show file size
for %%A in ("bin\Publish\Booku Remote Relay.exe") do (
    set SIZE=%%~zA
    set /a SIZE_MB=%%~zA / 1048576
)
echo   Size: ~%SIZE_MB% MB
echo.

:: Open output folder
echo Opening output folder...
start "" "%~dp0bin\Publish\"

echo.
echo ============================================
echo   BUILD COMPLETED
echo ============================================
echo.
echo   Deploy to VPS:
echo   1. Copy "Booku Remote Relay.exe" to VPS
echo   2. Run as Administrator (port 443 requires admin)
echo   3. Server will listen on port 443
echo.
pause
