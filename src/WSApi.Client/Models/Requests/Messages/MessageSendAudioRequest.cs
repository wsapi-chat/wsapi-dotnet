using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendAudioRequest : MessageRequestBase
{
    /// <summary>
    ///  Base64 encoded audio data.
    /// </summary>
    [JsonPropertyName("data")]
    public string Data { get; init; } = null!;

    /// <summary>
    ///  URL of the audio file to be sent. (this is an alternative to Data)
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    ///  MIME type of the audio file, e.g., "audio/mpeg", "audio/ogg".
    ///  </summary>
    [JsonPropertyName("mimeType")]
    public string MimeType { get; init; } = null!;

    /// <summary>
    ///  Indicates whether the audio should be sent as a view once message.
    ///  If true, the audio can only be viewed once by the recipient only in the main device.
    ///  </summary>
    [JsonPropertyName("viewOnce")]
    public bool ViewOnce { get; init; }
}
