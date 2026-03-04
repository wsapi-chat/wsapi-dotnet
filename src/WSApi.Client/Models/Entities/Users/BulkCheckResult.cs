using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Users;

public record BulkCheckResult
{
    [JsonPropertyName("query")] public string Query { get; init; } = null!;
    [JsonPropertyName("isInWhatsApp")] public bool IsInWhatsApp { get; init; }
    [JsonPropertyName("jid")] public string? Jid { get; init; }
}
