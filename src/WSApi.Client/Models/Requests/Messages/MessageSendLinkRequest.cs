using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendLinkRequest : MessageRequestBase
{
    /// <summary>
    /// The message text to send. It should include the url of the link.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = null!;

    /// <summary>
    /// The URL to include in the message.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; init; } = null!;

    /// <summary>
    /// The title of the link.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// The description of the link.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// The JPEG thumbnail as a base64 string.
    /// </summary>
    [JsonPropertyName("jpegThumbnail")]
    public string? JpegThumbnail { get; init; }
}