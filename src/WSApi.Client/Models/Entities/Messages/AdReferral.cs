using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Messages;

/// <summary>Optional attribution supplied by WhatsApp for Click-to-WhatsApp ads.</summary>
public record AdReferral
{
    [JsonPropertyName("ctwaClid")] public string? CtwaClid { get; init; }
    [JsonPropertyName("sourceType")] public string? SourceType { get; init; }
    [JsonPropertyName("sourceId")] public string? SourceId { get; init; }
    [JsonPropertyName("sourceUrl")] public string? SourceUrl { get; init; }
    [JsonPropertyName("sourceApp")] public string? SourceApp { get; init; }
    [JsonPropertyName("title")] public string? Title { get; init; }
    [JsonPropertyName("body")] public string? Body { get; init; }
    [JsonPropertyName("mediaType")] public string? MediaType { get; init; }
    [JsonPropertyName("thumbnailUrl")] public string? ThumbnailUrl { get; init; }
    [JsonPropertyName("conversionSource")] public string? ConversionSource { get; init; }
    [JsonPropertyName("showAdAttribution")] public bool? ShowAdAttribution { get; init; }
}
