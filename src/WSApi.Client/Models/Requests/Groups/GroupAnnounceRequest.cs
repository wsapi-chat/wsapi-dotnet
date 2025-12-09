using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupAnnounceRequest
{
    [JsonPropertyName("announce")] public bool Announce { get; init; }
}
