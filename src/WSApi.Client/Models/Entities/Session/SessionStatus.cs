using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Session;

public record SessionStatus
{
    [JsonPropertyName("isConnected")] public bool Connected { get; init; }
    [JsonPropertyName("isLoggedIn")] public bool IsLoggedIn { get; init; }
    [JsonPropertyName("deviceId")] public int? DeviceId { get; init; }
}
