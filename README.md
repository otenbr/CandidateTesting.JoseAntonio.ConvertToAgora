# CandidateTesting.JoseAntonio.ConvertToAgora

This console application perform the convertion of log files between two different platforms.
One platform is "MINHA CDN", is a company that provides the CDN service and another platform is "Agora", that generating billing reports from the CDN logs.

Minha CDN file example:

```
312|200|HIT|"GET /robots.txt HTTP/1.1"|100.2
101|200|MISS|"POST /myImages HTTP/1.1"|319.4
199|404|MISS|"GET /not-found HTTP/1.1"|142.9
312|200|INVALIDATE|"GET /robots.txt HTTP/1.1"|245.1
```

Agora file example:

```
#Version: 1.0
#Date: 31/03/2020 00:39:05
#Fields: provider http-method status-code uri-path time-taken response-size cache-status
"Minha CDN"	GET	200	/robots.txt	100	312	HIT
"Minha CDN"	POST	200	/myImages	319	101	MISS
"Minha CDN"	GET	404	/not-found	143	199	MISS
"Minha CDN"	GET	200	/robots.txt	245	312	INVALIDATE
```

## Getting Started

### Prerequisites

-   .Net Core 3.1 SDK
-   Visual Studio (_optional_)

### Building

Clone the repository to your machine.

```sh
git clone https://github.com/otenbr/CandidateTesting.JoseAntonio.ConvertToAgora.git ConvertToAgora
```

Change to solution directory and run the command `dotnet build`.

```sh
$ cd ConvertToAgora
$ dotnet build

Microsoft (R) Build Engine version 16.5.0+d4cbfca49 for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Restore completed in 48,65 ms for D:\Projects\dotnet\CandidateTesting.JoseAntonio\ConvertToAgora\ConvertToAgora.Tests\ConvertToAgora.Tests.csproj.
  Restore completed in 47,83 ms for D:\Projects\dotnet\CandidateTesting.JoseAntonio\ConvertToAgora\ConvertToAgora\ConvertToAgora.Convert.csproj.
  ConvertToAgora.Convert -> D:\Projects\dotnet\CandidateTesting.JoseAntonio\ConvertToAgora\ConvertToAgora\bin\Debug\netcoreapp3.1\ConvertToAgora.dll
  ConvertToAgora.Tests -> D:\Projects\dotnet\CandidateTesting.JoseAntonio\ConvertToAgora\ConvertToAgora.Tests\bin\Debug\netcoreapp3.1\ConvertToAgora.Tests.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:03.96
```

### Publishing

To publish a version for distribution, run the command `dotnet publish`. The package will be find in _\ConvertToAgora\bin\Release\netcoreapp3.1\publish\_, files \_ConvertToAgora.exe_ and _ConvertToAgora.dll_ .

```sh
$ dotnet publish -c Release

Microsoft (R) Build Engine version 16.5.0+d4cbfca49 for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Restore completed in 46,59 ms for D:\Projects\dotnet\CandidateTesting.JoseAntonio\ConvertToAgora\ConvertToAgora.Tests\ConvertToAgora.Tests.csproj.
  Restore completed in 45,87 ms for D:\Projects\dotnet\CandidateTesting.JoseAntonio\ConvertToAgora\ConvertToAgora\ConvertToAgora.Convert.csproj.
  ConvertToAgora.Convert -> D:\Projects\dotnet\CandidateTesting.JoseAntonio\ConvertToAgora\ConvertToAgora\bin\Release\netcoreapp3.1\ConvertToAgora.dll
  ConvertToAgora.Convert -> D:\Projects\dotnet\CandidateTesting.JoseAntonio\ConvertToAgora\ConvertToAgora\bin\Release\netcoreapp3.1\publish\
  ConvertToAgora.Tests -> D:\Projects\dotnet\CandidateTesting.JoseAntonio\ConvertToAgora\ConvertToAgora.Tests\bin\Release\netcoreapp3.1\ConvertToAgora.Tests.dll
  ConvertToAgora.Tests -> D:\Projects\dotnet\CandidateTesting.JoseAntonio\ConvertToAgora\ConvertToAgora.Tests\bin\Release\netcoreapp3.1\publish\
```

### Testing

To execute the unit tests, run the command `dotnet test`.

```sh
$ dotnet test

Test run for D:\Projects\dotnet\CandidateTesting.JoseAntonio\ConvertToAgora\ConvertToAgora.Tests\bin\Debug\netcoreapp3.1\ConvertToAgora.Tests.dll(.NETCoreApp,Version=v3.1)
Microsoft (R) Test Execution Command Line Tool Version 16.5.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

A total of 1 test files matched the specified pattern.

Test Run Successful.
Total tests: 6
     Passed: 6
 Total time: 1,0491 Seconds
```

### Usage

Copy the files _ConvertToAgora.exe_ and _ConvertToAgora.dll_ to the target directory. Run the program _ConvertToAgora.exe_ with the right parameters.

**Example:**

```sh
$ ConvertToAgora.exe http://logstorage.com/minhaCdn1.txt ./output/minhaCdn1.txt

The file was successfully created in the path: ./output/minhaCdn1.txt

```
