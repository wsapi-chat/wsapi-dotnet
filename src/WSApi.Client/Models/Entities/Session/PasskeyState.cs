using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Session;

/// <summary>
/// State of the passkey (WebAuthn) verification step required by some WhatsApp accounts to link a device.
/// </summary>
public record PasskeyState
{
    /// <summary>Current state; see <see cref="WSApi.Client.Models.Constants.PasskeyStatuses"/>.</summary>
    [JsonPropertyName("status")] public string Status { get; init; } = null!;

    /// <summary>Where the challenge came from; see <see cref="WSApi.Client.Models.Constants.PasskeySources"/>. Omitted when no step is in progress.</summary>
    [JsonPropertyName("source")] public string? Source { get; init; }

    /// <summary>WebAuthn <c>PublicKeyCredentialRequestOptionsJSON</c> to sign on <see cref="Origin"/>. Only set while the status is <c>required</c>.</summary>
    [JsonPropertyName("challenge")] public PasskeyChallenge? Challenge { get; init; }

    /// <summary>Origin the challenge must be signed on (e.g. <c>https://web.whatsapp.com</c>). Only set while the status is <c>required</c>.</summary>
    [JsonPropertyName("origin")] public string? Origin { get; init; }

    /// <summary>Code to show the user while the status is <c>confirmation_required</c>.</summary>
    [JsonPropertyName("confirmationCode")] public string? ConfirmationCode { get; init; }

    /// <summary>True once the confirmation has been sent to WhatsApp (sent automatically). Null until then.</summary>
    [JsonPropertyName("confirmationSent")] public bool? ConfirmationSent { get; init; }

    /// <summary>Failure reason when the status is <c>error</c>.</summary>
    [JsonPropertyName("error")] public string? Error { get; init; }

    /// <summary>When WhatsApp requested the passkey verification.</summary>
    [JsonPropertyName("requestedAt")] public DateTimeOffset? RequestedAt { get; init; }

    /// <summary>When the step expires (5 minutes after <see cref="RequestedAt"/>).</summary>
    [JsonPropertyName("expiresAt")] public DateTimeOffset? ExpiresAt { get; init; }
}

/// <summary>
/// WebAuthn <c>PublicKeyCredentialRequestOptionsJSON</c> with binary fields base64url-encoded
/// (accepted by <c>PublicKeyCredential.parseRequestOptionsFromJSON</c> in the browser).
/// </summary>
public record PasskeyChallenge
{
    /// <summary>Challenge bytes (base64url, unpadded).</summary>
    [JsonPropertyName("challenge")] public string? Challenge { get; init; }

    /// <summary>Timeout in milliseconds.</summary>
    [JsonPropertyName("timeout")] public long? Timeout { get; init; }

    /// <summary>Relying party ID.</summary>
    [JsonPropertyName("rpId")] public string? RpId { get; init; }

    /// <summary>Allowed credentials (empty for a discoverable credential).</summary>
    [JsonPropertyName("allowCredentials")] public PasskeyAllowCredential[]? AllowCredentials { get; init; }

    /// <summary>User verification requirement (e.g. <c>required</c>).</summary>
    [JsonPropertyName("userVerification")] public string? UserVerification { get; init; }

    /// <summary>WebAuthn extensions, passed through as raw JSON.</summary>
    [JsonPropertyName("extensions")] public Dictionary<string, JsonElement>? Extensions { get; init; }
}

/// <summary>
/// Entry of <see cref="PasskeyChallenge.AllowCredentials"/>.
/// </summary>
public record PasskeyAllowCredential
{
    /// <summary>Credential ID (base64url, unpadded).</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>Credential type (e.g. <c>public-key</c>).</summary>
    [JsonPropertyName("type")] public string? Type { get; init; }

    /// <summary>Authenticator transports.</summary>
    [JsonPropertyName("transports")] public string[]? Transports { get; init; }
}
