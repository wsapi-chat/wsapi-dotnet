using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendVoiceRequest : MessageRequestBase
{
    //add comments to each field to explain their purpose
    /// <summary>
    ///  Base64 encoded voice data. It should be OGG format
    /// </summary>
    [JsonPropertyName("voiceBase64")]
    public string VoiceBase64 { get; init; } = null!;

    /// <summary>
    ///  URL of the voice file to be sent. It should be OGG format (this is an alternative to AudioBase64)
    ///  </summary>
    [JsonPropertyName("voiceURL")]
    public string? VoiceURL { get; init; }

    /// <summary>
    ///  Indicates whether the image should be sent as a view once message.
    ///  If true, the image can only be viewed once by the recipient only in the main device.
    ///  </summary>
    [JsonPropertyName("viewOnce")] public bool ViewOnce { get; init; }
}