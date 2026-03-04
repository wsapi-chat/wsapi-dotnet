using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Status;

public record PostTextStatusRequest
{
    [JsonPropertyName("text")] public string Text { get; init; } = null!;
}
