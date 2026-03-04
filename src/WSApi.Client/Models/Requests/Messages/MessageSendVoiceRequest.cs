using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendVoiceRequest : MessageRequestBase
{
    /// <summary>
    ///  Base64 encoded voice data. It should be OGG format
    /// </summary>
    [JsonPropertyName("data")]
    public string Data { get; init; } = null!;

    /// <summary>
    ///  URL of the voice file to be sent. It should be OGG format (this is an alternative to Data)
    ///  </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    ///  Indicates whether the voice should be sent as a view once message.
    ///  If true, the voice can only be viewed once by the recipient only in the main device.
    ///  </summary>
    [JsonPropertyName("viewOnce")] public bool ViewOnce { get; init; }
}
