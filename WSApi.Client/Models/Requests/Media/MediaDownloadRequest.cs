using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Messages;
using WSApi.Client.Models.Events.Messages;

namespace WSApi.Client.Models.Requests.Media;

public record MediaDownloadRequest
{
    [JsonPropertyName("mediaType")] public string MediaType { get; init; } = null!;
    [JsonPropertyName("url")] public string Url { get; init; } = null!;
    [JsonPropertyName("directPath")] public string? DirectPath { get; init; }
    [JsonPropertyName("mediaKey")] public string? MediaKey { get; init; } = null!;
    [JsonPropertyName("mimeType")] public string? MimeType { get; init; }
    [JsonPropertyName("fileLength")] public long FileLength { get; init; }
    [JsonPropertyName("fileSHA256")] public string? FileSHA256 { get; init; }
    [JsonPropertyName("fileEncSHA256")] public string? FileEncSHA256 { get; init; }
    [JsonPropertyName("fileName")] public string? FileName { get; init; }

    public static MediaDownloadRequest FromMessageMedia(MessageMedia messageMedia)
    {
        return new MediaDownloadRequest
        {
            MediaType = messageMedia.MediaType,
            Url = messageMedia.Url,
            DirectPath = messageMedia.DirectPath,
            MediaKey = messageMedia.MediaKey,
            MimeType = messageMedia.Mimetype,
            FileLength = messageMedia.FileLength,
            FileSHA256 = messageMedia.FileSHA256, 
            FileEncSHA256 = messageMedia.FileEncSHA256,
            FileName = messageMedia.Title
        };
    }
} 