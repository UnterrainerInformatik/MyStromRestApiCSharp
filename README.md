[![NuGet](https://img.shields.io/nuget/v/MyStromRestApiCSharp.svg)](https://www.nuget.org/packages/MyStromRestApiCSharp/) [![NuGet](https://img.shields.io/nuget/dt/MyStromRestApiCSharp.svg)](https://www.nuget.org/packages/MyStromRestApiCSharp/) [![Build Status](https://travis-ci.org/UnterrainerInformatik/MyStromRestApiCSharp.svg?branch=master)](https://travis-ci.org/UnterrainerInformatik/MyStromRestApiCSharp)
 [![license](https://img.shields.io/github/license/unterrainerinformatik/MyStromRestApiCSharp.svg?maxAge=2592000)](http://unlicense.org)  [![Twitter Follow](https://img.shields.io/twitter/follow/throbax.svg?style=social&label=Follow&maxAge=2592000)](https://twitter.com/throbax)  

# General

This section contains various useful projects that should help your development-process.  

This section of our GIT repositories is free. You may copy, use or rewrite every single one of its contained projects to your hearts content.  
In order to get help with basic GIT commands you may try [the GIT cheat-sheet][coding] on our [homepage][homepage].  

This repository located on our  [homepage][homepage] is private since this is the master- and release-branch. You may clone it, but it will be read-only.  
If you want to contribute to our repository (push, open pull requests), please use the copy on github located here: [the public github repository][github]  

# MyStromRestApiCSharp
An implementation of the [MyStrom REST API][mystromapi] in C#.

> **If you like this repo, please don't forget to star it.**
> **Thank you.**



## Getting Started

First you device has to be in your LAN. You can achieve that by following the instructions of that device until you are able to use it with the [MyStrom][mystrom] App.
After that just note the IP-Address (and the Token if you set one) and you can remove it from the [MyStrom][mystrom]App again.

Then just import this repository into your application as nuget-package, create your first device and start sending and receiving messages.

## Example

```c#
[Test]
public void SetColorTest()
{
    var b = new MyStromBulb("Testbulb", "192.168.0.123", "600365A444BA", "93fnafh4o9f8h943rh");
    ToggleResultJson r;
    r = b.SetColor("2050ff80");
    r = b.SetColor("300;100;100", 1000);
    r = b.SetColor("12;100");

    Console.Out.WriteLine();
}
```

```c#
[Test]
public void ToggleTest()
{
    var b = new MyStromBulb("Testbulb", "192.168.0.123", "600365A444BA", "93fnafh4o9f8h943rh");
    ToggleResultJson r;
    r = b.SendToggle();
    r = b.SendToggle();

    Console.Out.WriteLine();
}
```

```c#
[Test]
public void ScannedWifisDetailedTest()
{
    var b = new MyStromSwitch("Testswitch", "192.168.0.124", "93fnafh4o9f8h943rh");

    var r = b.ScanForWifisDetailed();

    Console.Out.WriteLine();
}
```

```c#
[Test]
public void ReceiveDiscoveryTests()
{
    DeviceDiscovery.Start((json) =>
                          Console.Out.WriteLine($"[{json.IpEndPoint}]: {json.MacAddress}-{json.DeviceType}"));

    Thread.Sleep(5000);
    DeviceDiscovery.Stop();
    Thread.Sleep(1000);
}
```





### Attributions

Thx to [MyStrom][mystrom] for providing a [REST API][mystromapi].



# References

- [MyStrom][mystrom]
- [REST API][mystromapi]



[homepage]: http://www.unterrainer.info
[coding]: http://www.unterrainer.info/Home/Coding
[github]: https://github.com/UnterrainerInformatik/MyStromRestApiCSharp
[mystromapi]:  https://api.mystrom.ch/?version=latest
[mystrom]: https://mystrom.com