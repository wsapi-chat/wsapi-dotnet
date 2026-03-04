using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Status;

public record PostMediaStatusRequest
{
    [JsonPropertyName("data")] public string? Data { get; init; }
    [JsonPropertyName("url")] public string? Url { get; init; }
    [JsonPropertyName("mimeType")] public string MimeType { get; init; } = null!;
    [JsonPropertyName("caption")] public string? Caption { get; init; }
}
