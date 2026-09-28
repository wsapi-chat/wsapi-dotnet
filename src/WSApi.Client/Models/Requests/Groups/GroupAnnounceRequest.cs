using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupAnnounceRequest
{
    [JsonPropertyName("enabled")] public bool Announce { get; init; }
}
