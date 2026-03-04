# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [2.0.0] - 2026-03-04

### Changed
- Added .NET 10.0 target framework support
- Dropped .NET 7.0 target framework support (end of life)

## [1.0.18] - 2025-03-04

### Added
- Multi-target framework support for .NET 7.0, 8.0, and 9.0
- Comprehensive API client with domain-specific clients: Messages, Chats, Groups, Contacts, Media, Calls, Communities, Newsletters, Status, Users, and Session
- Webhook integration with HMAC-SHA256 signature verification and middleware
- Server-Sent Events (SSE) client with automatic reconnection and connection state management
- Strongly-typed event model with `EventFactory` for parsing webhook/SSE payloads
- Dependency injection extensions for easy integration with `IServiceCollection`
- `Try` pattern on all API methods returning `ApiResponse<T>` for error-safe calls
- NuGet package with embedded README and icon

### Changed
- Complete rewrite of the SDK from v1/v2 to a modern, composable architecture
- Moved from single monolithic client to domain-specific client composition (`WSApiClient.Messages`, `.Chats`, etc.)

[Unreleased]: https://github.com/wsapi-chat/wsapi-dotnet/compare/v2.0.0...HEAD
[2.0.0]: https://github.com/wsapi-chat/wsapi-dotnet/compare/v1.0.18...v2.0.0
[1.0.18]: https://github.com/wsapi-chat/wsapi-dotnet/releases/tag/v1.0.18
