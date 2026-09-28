using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Session;

public record SessionQrCode
{
    [JsonPropertyName("code")] public string Code { get; init; } = null!;
}
