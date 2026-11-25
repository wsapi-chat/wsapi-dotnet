using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Session;

public record SessionStatus
{
    [JsonPropertyName("isConnected")] public bool Connected { get; init; }
    [JsonPropertyName("isLoggedIn")] public bool IsLoggedIn { get; init; }
    [JsonPropertyName("deviceId")] public int? DeviceId { get; init; }

    /// <summary>
    /// Passkey verification state (same as <see cref="PasskeyState.Status"/>; see
    /// <see cref="WSApi.Client.Models.Constants.PasskeyStatuses"/>). While it is <c>required</c>,
    /// <c>submitted</c> or <c>confirmation_required</c>, stop polling the QR / pair-code endpoints.
    /// </summary>
    [JsonPropertyName("passkeyStatus")] public string? PasskeyStatus { get; init; }
}
