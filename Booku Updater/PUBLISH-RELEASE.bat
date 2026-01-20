@echo off
REM ============================================================
REM Booku Updater - Release Publisher
REM ============================================================
REM Script ini menjalankan publish-release.ps1 untuk:
REM 1. Build project dalam mode Release
REM 2. Obfuscate assemblies
REM 3. Publish sebagai single file executable
REM ============================================================

echo.
echo ============================================================
echo        BOOKU UPDATER - RELEASE PUBLISHER
echo ============================================================
echo.

REM Jalankan PowerShell script
powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0publish-release.ps1"

echo.
pause
