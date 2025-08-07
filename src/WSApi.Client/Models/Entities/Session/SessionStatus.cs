using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Session;

public record SessionStatus
{
    [JsonPropertyName("connected")] public bool Connected { get; init; }
    [JsonPropertyName("isLoggedIn")] public bool IsLoggedIn { get; init; }
}