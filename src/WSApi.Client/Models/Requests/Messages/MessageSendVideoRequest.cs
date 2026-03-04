using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendVideoRequest : MessageRequestBase
{
    /// <summary>
    ///  Base64 encoded video data. It should be in a supported video format.
    /// </summary>
    [JsonPropertyName("data")]
    public string? Data { get; init; }

    /// <summary>
    ///  URL of the video file to be sent. (this is an alternative to Data)
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    ///  MIME type of the video file, e.g., "video/mp4", "video/avi".
    /// </summary>
    [JsonPropertyName("mimeType")]
    public string MimeType { get; init; } = null!;

    /// <summary>
    ///  Caption for the video, which can be displayed alongside the video.
    /// </summary>
    [JsonPropertyName("caption")]
    public string? Caption { get; init; }

    /// <summary>
    ///  Indicates whether the video should be sent as a view once message.
    ///  If true, the video can only be viewed once by the recipient only in the main device.
    /// </summary>
    [JsonPropertyName("viewOnce")]
    public bool ViewOnce { get; init; }
}
