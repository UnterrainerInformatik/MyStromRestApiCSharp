ApiKey=$1

nuget pack ./MyStromRestApiCSharp/MyStromRestApiCSharp.csproj -Verbosity detailed -Symbols -SymbolPackageFormat snupkg -Prop Configuration=Release
nuget push ./MyStromRestApiCSharp.*.nupkg -Verbosity detailed -ApiKey $ApiKey -source https://www.nuget.org