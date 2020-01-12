#!/bin/bash

ApiKey=$1
SolutionName=$2
Version=$3

nuget pack ./$SolutionName/$SolutionName.csproj -Version $Version -Verbosity detailed -Symbols -SymbolPackageFormat snupkg -Prop Configuration=Release
#msbuild -t:pack ./$SolutionName/$SolutionName.csproj -p:NuspecFile=./$SolutionName/$SolutionName.nuspec -p:IncludeSymbols=true 
nuget push ./$SolutionName.*.nupkg -Verbosity detailed -ApiKey $ApiKey -source https://www.nuget.org