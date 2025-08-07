using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendImageRequest : MessageRequestBase
{
    //add comments to each field to explain their purpose

    /// <summary>
    ///  Base64 encoded image data. 
    /// </summary>
    [JsonPropertyName("imageBase64")]
    public string ImageBase64 { get; init; } = null!;

    /// <summary>
    ///  URL of the image to be sent. (this is an alternative to ImageBase64)
    /// </summary>     
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; init; } = null!;

    /// <summary>
    ///  MIME type of the image, e.g., "image/jpeg", "image/png".
    ///  </summary>
    [JsonPropertyName("mimeType")]
    public string MimeType { get; init; } = null!;

    /// <summary>
    ///  Caption for the image, which can be displayed alongside the image.
    ///  </summary>
    [JsonPropertyName("caption")]
    public string Caption { get; init; } = null!;

    /// <summary>
    ///  Indicates whether the image should be sent as a view once message.
    ///  If true, the image can only be viewed once by the recipient only in the main device.
    ///  </summary>
    [JsonPropertyName("viewOnce")]
    public bool ViewOnce { get; init; }
}