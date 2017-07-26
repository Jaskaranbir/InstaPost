#!/bin/bash

echo "Beginning to build API..."

dotnet clean
dotnet restore
dotnet build
dotnet run
