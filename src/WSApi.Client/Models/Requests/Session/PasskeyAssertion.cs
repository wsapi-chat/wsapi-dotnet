using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Session;

/// <summary>
/// WebAuthn assertion as produced by <c>PublicKeyCredential.toJSON()</c> in the browser
/// (binary fields base64url-encoded). It can also be deserialized directly from that JSON.
/// </summary>
public record PasskeyAssertion
{
    /// <summary>Credential ID.</summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; init; }

    /// <summary>Raw credential ID (base64url). Required.</summary>
    [JsonPropertyName("rawId")] public string RawId { get; init; } = null!;

    /// <summary>Credential type (e.g. <c>public-key</c>).</summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; init; }

    /// <summary>Authenticator response. Required.</summary>
    [JsonPropertyName("response")] public PasskeyAssertionResponse Response { get; init; } = null!;
}

/// <summary>
/// The <c>response</c> part of a <see cref="PasskeyAssertion"/>.
/// </summary>
public record PasskeyAssertionResponse
{
    /// <summary>Client data JSON (base64url). Required.</summary>
    [JsonPropertyName("clientDataJSON")] public string ClientDataJson { get; init; } = null!;

    /// <summary>Authenticator data (base64url).</summary>
    [JsonPropertyName("authenticatorData")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AuthenticatorData { get; init; }

    /// <summary>Assertion signature (base64url). Required.</summary>
    [JsonPropertyName("signature")] public string Signature { get; init; } = null!;

    /// <summary>User handle (base64url), if returned by the authenticator. May be null.</summary>
    [JsonPropertyName("userHandle")] public string? UserHandle { get; init; }
}
