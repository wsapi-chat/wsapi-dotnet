using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendReactionRequest
{
    /// <summary>
    ///  The recipient of the reaction. This could be a phone number, group ID, or broadcast list ID.
    /// </summary>
    [JsonPropertyName("to")]
    public string To { get; init; } = null!;

    /// <summary>
    ///  If the recepient is a group, this is the ID of the sender of the message to which the reaction is being sent. If the recipient is a user, this could be null
    /// </summary>
    [JsonPropertyName("senderId")]
    public string SenderId { get; init; } = null!;

    /// <summary>
    ///  The emoji reaction to be sent. This should be a valid emoji string.
    /// </summary>
    [JsonPropertyName("reaction")]
    public string Reaction { get; init; } = null!;
}