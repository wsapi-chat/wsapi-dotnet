using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupLockedRequest
{
    [JsonPropertyName("enabled")] public bool Locked { get; init; }
}
