# Contributing to wsapi-dotnet

Thank you for your interest in contributing to the WSAPI .NET SDK! This guide will help you get started.

## Prerequisites

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download) (builds also target .NET 8.0 and 9.0)
- A code editor (Visual Studio, Rider, or VS Code with C# Dev Kit)
- Git

## Getting Started

1. **Fork** the repository on GitHub
2. **Clone** your fork locally:
   ```bash
   git clone https://github.com/<your-username>/wsapi-dotnet.git
   cd wsapi-dotnet
   ```
3. **Create a branch** for your change:
   ```bash
   git checkout -b feat/my-feature
   ```

## Development Workflow

```bash
# Restore dependencies
dotnet restore src/WSApi.Client.sln

# Build in Release mode
dotnet build src/WSApi.Client.sln --configuration Release

# Run tests
dotnet test src/WSApi.Client.sln

# Check formatting (must pass before submitting PR)
dotnet format src/WSApi.Client.sln --verify-no-changes
```

## Project Structure

| Path | Description |
|------|-------------|
| `src/WSApi.Client/` | Core SDK library (NuGet package) |
| `src/WSApi.Client/ApiClient/` | Domain-specific API clients (Messages, Chats, Groups, etc.) |
| `src/WSApi.Client/Models/` | Request and response models |
| `src/WSApi.Client/Models/Constants/` | Enum-like constants (event types, status codes) |
| `src/WSApi.Client/Models/Entities/` | Domain entities (Message, Chat, Contact, etc.) |
| `src/WSApi.Client/Models/Events/` | Strongly-typed webhook/SSE event models |
| `src/WSApi.Client/Models/Requests/` | API request DTOs |
| `src/WSApi.Client/SSE/` | Server-Sent Events client and connection management |
| `src/WSAPI.Client.Examples.Webhook/` | Example: receiving events via webhook |
| `src/WSAPI.Client.Examples.SSE/` | Example: receiving events via SSE |

## Key Patterns

- **Client composition** — `WSApiClient` exposes domain-specific clients (`.Messages`, `.Chats`, `.Groups`, etc.) rather than a single monolithic interface.
- **Try pattern** — All API methods have a `Try` variant (e.g., `TrySendTextMessage`) that returns `ApiResponse<T>` instead of throwing on HTTP errors.
- **EventFactory** — Centralised parsing of webhook/SSE JSON payloads into strongly-typed event objects.
- **Dependency injection** — `AddWSApiClient()` extension method for `IServiceCollection` wires up `HttpClient` and all services.

## Adding Features

| What you're adding | Where to put it |
|--------------------|----------------|
| New API endpoint | `src/WSApi.Client/ApiClient/` — add method to existing client or create a new `*Client.cs` |
| New request/response model | `src/WSApi.Client/Models/Requests/` or `src/WSApi.Client/Models/Entities/` |
| New event type | `src/WSApi.Client/Models/Events/` and register in `EventFactory.cs` |
| New SSE feature | `src/WSApi.Client/SSE/` |
| New example | Create a new project under `src/` and add it to `WSApi.Client.sln` |

## Commit Messages

This project follows [Conventional Commits](https://www.conventionalcommits.org/):

```
feat: add newsletter subscription endpoint
fix: correct HMAC signature validation for empty bodies
docs: update README with SSE usage example
chore: bump Microsoft.Extensions.Http to 9.0.1
```

## Submitting a Pull Request

1. Ensure your code builds without warnings:
   ```bash
   dotnet build src/WSApi.Client.sln --configuration Release
   ```
2. Ensure formatting passes:
   ```bash
   dotnet format src/WSApi.Client.sln --verify-no-changes
   ```
3. Push your branch and open a Pull Request against `main`
4. Fill in the PR template — describe your change, check the relevant boxes
5. A maintainer will review your PR

## Reporting Issues

- Use [GitHub Issues](https://github.com/wsapi-chat/wsapi-dotnet/issues) for bug reports and feature requests
- Use [GitHub Discussions](https://github.com/wsapi-chat/wsapi-dotnet/discussions) for questions and general discussion

## License

By contributing, you agree that your contributions will be licensed under the [MIT License](LICENSE).
