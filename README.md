# ASP.NET Core SignalR Playground

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=f2calv_signalrcore-dotnet&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=f2calv_signalrcore-dotnet)

A .NET 10 playground for ASP.NET Core SignalR clients, hubs and protocol options.

## Projects

| Project | Purpose |
| --- | --- |
| `WebApp` | Hosts the SignalR hub. |
| `ClientAppA`, `ClientAppB` | Exercise client connection and messaging behavior. |
| `ClientLibrary` | Provides reusable client behavior. |
| `SharedLibrary` | Contains contracts shared by the host and clients. |

## Prerequisites

- A .NET 10 SDK
- Visual Studio 2026, VS Code with C# Dev Kit, or another .NET-compatible editor

## Run the playground

Restore and build the root solution:

```pwsh
dotnet restore .\signalrcore-dotnet.slnx
dotnet build .\signalrcore-dotnet.slnx --no-restore
```

Start `WebApp` before starting either client application. When changing a hub method or shared
message contract, update both clients in the same change.

This repository contains demonstration code and does not deploy a hosted service.
