#!/bin/bash

ApiKey=$1
Config=$2
ProjectFileName=$3
ProjectPath=$4
Version=$5

nuget pack ${ProjectPath}${ProjectFileName}.csproj -Version $Version -Verbosity detailed -Symbols -SymbolPackageFormat snupkg -Prop Configuration=$Config
nuget push ./$ProjectName.*.nupkg -Verbosity detailed -ApiKey $ApiKey -source https://www.nuget.org