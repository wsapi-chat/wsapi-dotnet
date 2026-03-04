using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Users;

public record BulkCheckRequest
{
    [JsonPropertyName("phones")] public string[] Phones { get; init; } = [];
}
