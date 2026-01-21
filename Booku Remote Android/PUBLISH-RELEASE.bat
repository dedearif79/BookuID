@echo off
:: ============================================
:: PUBLISH-RELEASE.bat
:: Build Booku Remote Android - Release
:: ============================================

title Booku Remote Android - Release Build

echo.
echo ============================================
echo   BOOKU REMOTE ANDROID - RELEASE BUILD
echo ============================================
echo.

:: Set working directory
cd /d "%~dp0"

:: Clean previous build
echo [1/3] Cleaning previous build...
dotnet clean "Booku Remote Android.csproj" -c Release -v quiet
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Clean failed!
    pause
    exit /b 1
)
echo       Done.
echo.

:: Build Release
echo [2/3] Building Release...
echo       (This may take 2-3 minutes)
echo.
dotnet build "Booku Remote Android.csproj" -c Release -v minimal
if %ERRORLEVEL% NEQ 0 (
    echo.
    echo [ERROR] Build failed!
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
echo   %~dp0bin\Release\net8.0-android\
echo.
echo   APK File (for installation):
echo   com.bookuid.remote-Signed.apk
echo.

:: Open output folder
echo Opening output folder...
start "" "%~dp0bin\Release\net8.0-android\"

echo.
echo ============================================
echo   BUILD COMPLETED
echo ============================================
echo.
pause
