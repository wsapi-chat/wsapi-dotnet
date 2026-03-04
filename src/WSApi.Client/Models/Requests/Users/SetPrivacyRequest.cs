using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Users;

public record SetPrivacyRequest
{
    [JsonPropertyName("setting")] public string Setting { get; init; } = null!;
    [JsonPropertyName("value")] public string Value { get; init; } = null!;
}
