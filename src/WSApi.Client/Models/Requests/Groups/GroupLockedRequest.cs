using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupLockedRequest
{
    [JsonPropertyName("locked")] public bool Locked { get; init; }
}
