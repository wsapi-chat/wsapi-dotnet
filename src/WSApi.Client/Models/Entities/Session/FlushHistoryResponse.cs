using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Session;

public record FlushHistoryResponse
{
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
}
