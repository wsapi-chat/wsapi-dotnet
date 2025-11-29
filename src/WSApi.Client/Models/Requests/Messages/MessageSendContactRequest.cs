using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendContactRequest
{
    [JsonPropertyName("to")] public string To { get; init; } = null!;
    [JsonPropertyName("vCard")] public string VCard { get; init; } = null!;
    [JsonPropertyName("displayName")] public string DisplayName { get; init; } = null!;

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
    /// Ephemeral expiration time to override chat settings.
    /// </summary>
    [JsonPropertyName("ephemeralExpiration")]
    public string? EphemeralExpiration { get; init; }
}
