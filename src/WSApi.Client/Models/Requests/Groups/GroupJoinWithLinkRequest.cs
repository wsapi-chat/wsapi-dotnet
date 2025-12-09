using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupJoinWithLinkRequest
{
    [JsonPropertyName("code")] public string Code { get; init; } = null!;
}
