using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendDocumentRequest : MessageRequestBase
{
    /// <summary>
    ///  The document to send, encoded in base64.
    /// </summary>
    [JsonPropertyName("data")]
    public string Data { get; init; } = null!;

    /// <summary>
    ///  The URL of the document to send. (this is an alternative to Data)
    ///  </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// The name of the file being sent.
    /// </summary>
    [JsonPropertyName("filename")]
    public string FileName { get; init; } = null!;

    /// <summary>
    ///  Caption for the document, which can be displayed alongside the document.
    /// </summary>
    [JsonPropertyName("caption")]
    public string Caption { get; init; } = null!;
}
