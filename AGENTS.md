# AGENTS.md

This file provides guidance to AI agents on how to work with this repository. It describes the project structure, build commands, key patterns, and coding conventions to follow when making changes.

## Project

WSApi.Client — .NET SDK for the WSApi WhatsApp API. NuGet package targeting net7.0, net8.0, net9.0.

## Commands

```bash
dotnet restore src/WSApi.Client.sln
dotnet build src/WSApi.Client.sln --configuration Release
dotnet test src/WSApi.Client.sln --configuration Release
dotnet format style src/WSApi.Client.sln --verify-no-changes
dotnet format analyzers src/WSApi.Client.sln --verify-no-changes
```

## Structure

- `src/WSApi.Client/` — core SDK (NuGet package)
  - `ApiClient/` — domain clients (Messages, Chats, Groups, Contacts, Media, etc.)
  - `Models/` — entities, requests, events, constants
  - `SSE/` — Server-Sent Events client
  - `EventFactory.cs` — parses JSON into typed event objects
  - `Extensions.cs` — DI registration (`AddWSApiClient()`)
- `src/WSAPI.Client.Examples.Webhook/` — webhook example app
- `src/WSAPI.Client.Examples.SSE/` — SSE example app

## Key Patterns

- **Client composition**: `WSApiClient.Messages`, `.Chats`, `.Groups`, etc.
- **Try pattern**: all API methods have `Try*` variants returning `ApiResponse<T>` instead of throwing
- **EventFactory**: central parser for webhook/SSE JSON payloads
- Commits follow Conventional Commits (`feat:`, `fix:`, `chore:`, etc.)

## Linting

Style and analyzer rules are defined in `.editorconfig`. Both `dotnet format style` and `dotnet format analyzers` must pass with `--verify-no-changes` before merging.
