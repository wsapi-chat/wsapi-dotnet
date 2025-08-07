using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendTextRequest : MessageRequestBase
{
    /// <summary>
    /// The message text to send.
    ///  </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = null!;
}