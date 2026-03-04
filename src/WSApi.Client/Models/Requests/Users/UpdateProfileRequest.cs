using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Users;

public record UpdateProfileRequest
{
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("status")] public string? Status { get; init; }
    [JsonPropertyName("picture")] public string? Picture { get; init; }
}
