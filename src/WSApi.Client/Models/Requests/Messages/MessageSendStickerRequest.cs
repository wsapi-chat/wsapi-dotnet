using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendStickerRequest : MessageRequestBase
{
    /// <summary>
    ///  The sticker to send, encoded in base64. The file should be in a WebP supported sticker format.
    /// </summary>
    [JsonPropertyName("data")]
    public string Data { get; init; } = null!;

    /// <summary>
    ///  The URL of the sticker to send. (this is an alternative to Data). The file should be in a WebP supported sticker format.
    ///  </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    ///  Whether the sticker is animated or not.
    ///  </summary>
    [JsonPropertyName("isAnimated")]
    public bool IsAnimated { get; init; }
}
