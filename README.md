# DotNet Kick Starter Templates

## Prerequisite

- [.Net Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)

## How to install

- Clone this repo
- Pack and install templates

```
dotnet pack TemplatesPack.csproj -o Build
dotnet new -i Build\DotNet.KickStarter.Templates*
```

## How to use

```
// Create new project
dotnet new ksproject -n MyProject

// Create empty solution
dotnet new ksproject -n MyProject -e
```
