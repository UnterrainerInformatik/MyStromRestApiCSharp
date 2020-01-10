ApiKey=$1

#nuget pack ./MyStromRestApiCSharp/MyStromRestApiCSharp.csproj -Verbosity detailed -Symbols -SymbolPackageFormat snupkg -Prop Configuration=Release
msbuild -t:pack ./MyStromRestApiCSharp/MyStromRestApiCSharp.csproj -p:NuspecFile=./MyStromRestApiCSharp/MyStromRestApiCSharp.nuspec -p:IncludeSymbols=true
nuget push ./MyStromRestApiCSharp.*.nupkg -Verbosity detailed -ApiKey $ApiKey -source https://www.nuget.org