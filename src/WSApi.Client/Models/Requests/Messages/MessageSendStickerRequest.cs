using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendStickerRequest : MessageRequestBase
{
    /// <summary>
    ///  The sticker to send, encoded in base64. The file should be in a WebP supported sticker format.
    /// </summary>
    [JsonPropertyName("stickerBase64")]
    public string StickerBase64 { get; init; } = null!;

    /// <summary>
    ///  The URL of the document to send. (this is an alternative to DocumentBase64). The file should be in a WebP supported sticker format.
    ///  </summary>
    [JsonPropertyName("stickerURL")]
    public string? StickerURL { get; init; }

    /// <summary>
    ///  MIME type of the sticker, e.g., "image/webp".
    ///  </summary>
    [JsonPropertyName("mimeType")]
    public string MimeType { get; init; } = null!;

    /// <summary>
    ///  Whether the sticker is animated or not.
    ///  </summary>
    [JsonPropertyName("isAnimated")]
    public bool IsAnimated { get; init; }
}