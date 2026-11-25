# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [3.2.0] - 2026-11-25

### Added
- Passkey (WebAuthn) pairing on `SessionClient` / `ISessionClient`: `GetPasskeyStateAsync`, `SubmitPasskeyAsync`, `RequestPasskeyAsync`, `ConfirmPasskeyAsync` (optional, compatibility only: the server now confirms automatically) and their `Try*` variants, for `GET/POST /session/passkey`, `POST /session/passkey/request` and `POST /session/passkey/confirm`.
- Models `PasskeyState`, `PasskeyChallenge`, `PasskeyAllowCredential`, `PasskeyAssertion`, `PasskeyAssertionResponse` and constants `PasskeyStatuses` (with `IsPending`) and `PasskeySources`.
- `SessionStatus.PasskeyStatus`.
- `UserInfo.VerifiedName` (verified business name).

### Changed
- `GroupCreateRequest.Participants` is optional: leave it empty to create a group with only the paired account.
- Documented that the QR and pair-code methods return HTTP 409 while a passkey step is pending; stop polling them until it completes, fails or expires.
- `openapi/wsapi-api.yml` updated with the passkey endpoints and schemas, `409` on QR/pair-code routes, optional `CreateGroupRequest.participants` and `UserInfo.verifiedName`.

### Fixed

- The bundled OpenAPI spec documented the `DeviceNotPaired` response with status 403; the API returns 409
- `SessionStatus.DeviceId` is now `string?` (the device JID, e.g. `15550001111:12@s.whatsapp.net`). It was `int?`, so reading the status of a paired instance threw a `JsonException`. **Breaking** for code that used it as a number

## [3.0.0]

QR image methods now read bytes and QR text methods unwrap `code`. Fixed JSON names for group/community settings and invitation responses while preserving C# request properties. Join-by-invite accepts HTTP 204 and returns the requested group ID for compatibility. Contact Update uses POST upsert. Breaking change: removed ChatsClient/IChatsClient SynchronizeAllAsync and TrySynchronizeAllAsync because their endpoint no longer exists; use ListAsync or RequestMessagesAsync for a specific chat. Added optional instance name (after CancellationToken), optional media fields, ad referral and media filename. Added an executable contract-test project.

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
