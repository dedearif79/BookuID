#!/bin/bash
# ============================================
# PUBLISH-RELEASE.sh
# Build Booku Remote Relay - Release (Linux)
# ============================================

echo ""
echo "============================================"
echo "  BOOKU REMOTE RELAY - RELEASE BUILD"
echo "  Target: Linux x64"
echo "============================================"
echo ""

# Set working directory ke lokasi script
cd "$(dirname "$0")"

# Clean previous build
echo "[1/3] Cleaning previous build..."
dotnet clean "Booku Remote Relay.csproj" -c Release -v quiet
if [ $? -ne 0 ]; then
    echo "[ERROR] Clean failed!"
    exit 1
fi
echo "      Done."
echo ""

# Publish Release (Single File + Self-Contained + Trimmed)
echo "[2/3] Publishing Release..."
echo "      - Single Executable: Yes"
echo "      - Self-Contained: Yes"
echo "      - IL Trimming: Yes"
echo "      - Compression: Yes"
echo "      - Target: linux-x64"
echo ""

dotnet publish "Booku Remote Relay.csproj" -c Release -o "bin/Publish" -v minimal
if [ $? -ne 0 ]; then
    echo ""
    echo "[ERROR] Publish failed!"
    exit 1
fi
echo ""

# Show output
echo "[3/3] Build completed successfully!"
echo ""
echo "============================================"
echo "  OUTPUT FILES:"
echo "============================================"
echo ""
echo "  Location:"
echo "  $(pwd)/bin/Publish/"
echo ""
echo "  Executable:"
echo "  Booku Remote Relay"
echo ""

# Show file size
if [ -f "bin/Publish/Booku Remote Relay" ]; then
    SIZE=$(stat -c%s "bin/Publish/Booku Remote Relay" 2>/dev/null || stat -f%z "bin/Publish/Booku Remote Relay" 2>/dev/null)
    SIZE_MB=$((SIZE / 1048576))
    echo "  Size: ~${SIZE_MB} MB"
fi
echo ""

echo "============================================"
echo "  BUILD COMPLETED"
echo "============================================"
echo ""
echo "  Deploy to VPS Linux:"
echo "  1. Copy 'Booku Remote Relay' to VPS"
echo "  2. chmod +x 'Booku Remote Relay'"
echo "  3. ./Booku\ Remote\ Relay"
echo "     atau: ./Booku\ Remote\ Relay 8080 (custom port)"
echo ""
echo "  Run as service (systemd):"
echo "  - Lihat contoh di booku-relay.service"
echo ""
