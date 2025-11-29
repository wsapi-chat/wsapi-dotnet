using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public abstract record MessageRequestBase
{
    /// <summary>
    ///  The recipient of the message. This could be a phone number, group ID, or broadcast list ID.
    ///  Phone numbers should be in the 1234567890@s.whatsapp.net format.
    ///  Group IDs should be in the format 12345678@g.us format
    /// </summary>
    [JsonPropertyName("to")]
    public string To { get; init; } = null!;

    /// <summary>
    /// An array of phone numbers that are mentioned in the message.
    /// The message mentions should be in the following format: Hello @1234567890
    /// The mentions should be in the whatsapp format: 1234567890@s.whatsapp.net
    /// </summary>
    [JsonPropertyName("mentions")]
    public string[] Mentions { get; init; } = null!;

    /// <summary>
    /// Message ID of the message being replied to.
    /// </summary>
    [JsonPropertyName("replyTo")]
    public string ReplyTo { get; init; } = null!;

    /// <summary>
    /// Sender ID of the message being replied to. Required when replying in group chats.
    /// </summary>
    [JsonPropertyName("replyToSenderId")]
    public string? ReplyToSenderId { get; init; }

    /// <summary>
    /// Ephemeral expiration time to override chat settings.
    /// </summary>
    [JsonPropertyName("ephemeralExpiration")]
    public string? EphemeralExpiration { get; init; }

    /// <summary>
    /// Indicates whether the message is a forwarded message.
    /// </summary>
    [JsonPropertyName("isForwarded")]
    public bool IsForwarded { get; init; }
}