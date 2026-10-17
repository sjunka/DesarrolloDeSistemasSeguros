#!/bin/bash
# Genera ../Marketplace_SergioJunca.tm7 con el codigo real del Microsoft Threat Modeling Tool 7.3 y lo valida:
# lo vuelve a abrir como lo hace la herramienta y deja una vista previa en preview.svg.
# Requiere el SDK de .NET 8 (sin sudo: curl -sSL https://dot.net/v1/dotnet-install.sh | bash -s -- --channel 8.0).
# Uso: ./build.sh            genera y valida
#      ./build.sh archivo.tm7  solo valida otro archivo (p. ej. uno hecho en la interfaz)
set -euo pipefail
cd "$(dirname "$0")"
[ -x "$HOME/.dotnet/dotnet" ] && export DOTNET_ROOT="$HOME/.dotnet" PATH="$HOME/.dotnet:$PATH"
export DOTNET_CLI_TELEMETRY_OPTOUT=1 DOTNET_NOLOGO=1
CACHE="${TMT_CACHE:-$HOME/.cache/tmt7}"   # binarios del TMT: fuera del repo

for p in WindowsBase PresentationCore PresentationFramework; do dotnet build -c Release -v q -o "$CACHE/shim" "shim/$p" >/dev/null; done
dotnet build -c Release -v q harness >/dev/null
H=harness/bin/Release/net8.0/harness
[ -f "$CACHE/tmt/ThreatModeling.Model.dll" ] || "$H" "$CACHE" fetch

if [ $# -gt 0 ]; then exec "$H" "$CACHE" open "$1" --list; fi
"$H" "$CACHE" build ../Marketplace_SergioJunca.tm7
"$H" "$CACHE" preview ../Marketplace_SergioJunca.tm7 preview.svg
