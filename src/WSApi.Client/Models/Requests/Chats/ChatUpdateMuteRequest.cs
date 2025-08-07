using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Chats;

public record ChatUpdateMuteRequest
{
    [JsonPropertyName("duration")] public string? Duration { get; init; }
} 