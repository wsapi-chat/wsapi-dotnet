using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Messages;

public record MessageMedia
{
    [JsonPropertyName("mediaType")] public string MediaType { get; init; } = null!;
    [JsonPropertyName("url")] public string Url { get; init; } = null!;
    [JsonPropertyName("mimeType")] public string? Mimetype { get; init; }
    [JsonPropertyName("fileLength")] public long FileLength { get; init; }
    [JsonPropertyName("fileSHA256")] public string? FileSHA256 { get; init; }
    [JsonPropertyName("fileEncSHA256")] public string? FileEncSHA256 { get; init; }
    [JsonPropertyName("mediaKey")] public string? MediaKey { get; init; }
    [JsonPropertyName("caption")] public string? Caption { get; init; }
    [JsonPropertyName("height")] public int Height { get; init; }
    [JsonPropertyName("width")] public int Width { get; init; }
    [JsonPropertyName("jpegThumbnail")] public string? JpegThumbnail { get; init; }
    [JsonPropertyName("directPath")] public string? DirectPath { get; init; }
    [JsonPropertyName("duration")] public int Duration { get; init; }
    [JsonPropertyName("pageCount")] public int PageCount { get; init; }
    [JsonPropertyName("title")] public string? Title { get; init; }
}