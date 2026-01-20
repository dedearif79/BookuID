@echo off
echo ============================================================
echo     BOOKU REMOTE RELEASE PUBLISHER WITH OBFUSCATION
echo ============================================================
echo.
echo Menjalankan script publish...
echo.

cd /d "%~dp0"
powershell -ExecutionPolicy Bypass -File "publish-release.ps1"

echo.
pause
