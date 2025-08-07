using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Messages;

public record MessageExtendedText
{
    [JsonPropertyName("matchedText")] public string? MatchedText { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("title")] public string? Title { get; init; }
    [JsonPropertyName("jpegThumbnail")] public string? JPEGThumbnail { get; init; }
}