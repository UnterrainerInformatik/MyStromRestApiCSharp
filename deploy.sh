ApiKey=$1

nuget pack -sym ./MyStromRestApiCSharp/MyStromRestApiCSharp.csproj -Verbosity detailed
nuget push ./MyStromRestApiCSharp.*.nupkg -Verbosity detailed -ApiKey $ApiKey -source https://www.nuget.org