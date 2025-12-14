using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendLocationRequest
{
    /// <summary>
    /// The recipient of the message. This could be a phone number, group ID, or broadcast list ID.
    /// Phone numbers should be in the 1234567890@s.whatsapp.net format.
    /// Group IDs should be in the format 12345678@g.us format
    /// </summary>
    [JsonPropertyName("to")]
    public string To { get; init; } = null!;

    /// <summary>
    /// Latitude of the location.
    /// </summary>
    [JsonPropertyName("latitude")]
    public double Latitude { get; init; }

    /// <summary>
    /// Longitude of the location.
    /// </summary>
    [JsonPropertyName("longitude")]
    public double Longitude { get; init; }

    /// <summary>
    /// Optional. The address of the location. This can be a full address or a description of the location.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>
    /// Optional. The name of the location. This can be a place name or a custom name for the location.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Optional. A URL to a map or a static image of the location. This can be used to provide additional context or visual representation of the location.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// Message ID of the message being replied to.
    /// </summary>
    [JsonPropertyName("replyTo")]
    public string? ReplyTo { get; init; }

    /// <summary>
    /// Sender ID of the message being replied to. Required when replying in group chats.
    /// </summary>
    [JsonPropertyName("replyToSenderId")]
    public string? ReplyToSenderId { get; init; }

    /// <summary>
    /// Indicates whether the message is a forwarded message.
    /// </summary>
    [JsonPropertyName("isForwarded")]
    public bool IsForwarded { get; init; }

    /// <summary>
    /// Ephemeral expiration time to override chat settings.
    /// </summary>
    [JsonPropertyName("ephemeralExpiration")]
    public string? EphemeralExpiration { get; init; }
}