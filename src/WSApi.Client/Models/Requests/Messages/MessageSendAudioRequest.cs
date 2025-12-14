using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendAudioRequest : MessageRequestBase
{
    /// <summary>
    ///  Base64 encoded audio data.
    ///  /// </summary>
    [JsonPropertyName("audioBase64")]
    public string AudioBase64 { get; init; } = null!;

    /// <summary>
    ///  URL of the audio file to be sent. (this is an alternative to AudioBase64)
    /// </summary>
    [JsonPropertyName("audioURL")]
    public string? AudioURL { get; init; }

    /// <summary>
    ///  MIME type of the audio file, e.g., "audio/mpeg", "audio/ogg".
    ///  </summary>
    [JsonPropertyName("mimeType")]
    public string MimeType { get; init; } = null!;

    /// <summary>
    ///  Indicates whether the image should be sent as a view once message.
    ///  If true, the image can only be viewed once by the recipient only in the main device.
    ///  </summary>
    [JsonPropertyName("viewOnce")]
    public bool ViewOnce { get; init; }
}