#!/bin/bash

ApiKey=$1
Config=$2
ProjectFileName=$2
ProjectPath=$3
Version=$4

nuget pack $ProjectPath/$ProjectFileName.csproj -Version $Version -Verbosity detailed -Symbols -SymbolPackageFormat snupkg -Prop Configuration=$Config
nuget push ./$ProjectName.*.nupkg -Verbosity detailed -ApiKey $ApiKey -source https://www.nuget.org