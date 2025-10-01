#!/usr/bin/env bash
set -e
dotnet restore
dotnet build -c Release
dotnet publish auth-service -c Release -o ./auth-service/publish || true
dotnet publish rooms-service -c Release -o ./rooms-service/publish || true
echo "Build finished"
