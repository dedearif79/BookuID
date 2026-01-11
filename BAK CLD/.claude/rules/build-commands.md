# Build Commands

## Build Solution

```bash
# Build entire solution (menggunakan MSBuild Visual Studio karena COM Reference)
"/mnt/c/Program Files/Microsoft Visual Studio/2022/Community/MSBuild/Current/Bin/MSBuild.exe" "/mnt/d/vb .net project/BookuID/BookuID.sln" -t:Build -verbosity:minimal

# Build specific project
"/mnt/c/Program Files/Microsoft Visual Studio/2022/Community/MSBuild/Current/Bin/MSBuild.exe" "/mnt/d/vb .net project/BookuID/Booku/Booku.vbproj" -t:Build -verbosity:minimal

# Alternative dengan dotnet (tanpa COM Reference support)
dotnet build BookuID.sln
dotnet build "Booku/Booku.vbproj"

# Clean build artifacts
dotnet clean BookuID.sln
```

## Release Publishing (dengan Obfuscation)

```powershell
# Dari Windows PowerShell, jalankan script publish-release
cd "D:\vb .net project\BookuID\Booku"
.\publish-release.ps1

# Skip obfuscation (untuk testing cepat)
.\publish-release.ps1 -SkipObfuscation
```

Output: `Booku/bin/Release/Final/Booku.exe` (single-file executable, self-contained)
