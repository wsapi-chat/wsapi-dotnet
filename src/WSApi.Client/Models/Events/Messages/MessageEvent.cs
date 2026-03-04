using System;
using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Messages;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Messages;

public record MessageEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("chatId")] public string ChatId { get; init; } = null!;
    [JsonPropertyName("sender")] public Sender Sender { get; init; } = null!;
    [JsonPropertyName("time")] public DateTime Time { get; init; }
    [JsonPropertyName("isGroup")] public bool IsGroup { get; init; }
    [JsonPropertyName("isStatus")] public bool IsStatus { get; init; }
    [JsonPropertyName("mentions")] public string[]? Mentions { get; init; }
    [JsonPropertyName("ephemeralExpiration")] public string? EphemeralExpiration { get; init; } 
    
    [JsonPropertyName("type")] public string Type { get; init; } = null!;
    
    [JsonPropertyName("text")] public string? Text { get; init; }
    [JsonPropertyName("replyTo")] public MessageReplyTo? ReplyTo { get; init; }
    [JsonPropertyName("extendedText")] public MessageExtendedText? ExtendedText { get; init; }
    [JsonPropertyName("isEdit")] public bool IsEdit { get; init; }
    [JsonPropertyName("edit")] public MessageEdit? Edit { get; init; }
    [JsonPropertyName("media")] public MessageMedia? Media { get; init; }
    [JsonPropertyName("reaction")] public MessageReaction? Reaction { get; init; }
    [JsonPropertyName("contact")] public string? Contact { get; init; }
    [JsonPropertyName("contactArray")] public string[]? Contacts { get; init; }
    [JsonPropertyName("pin")] public MessagePin? Pin { get; init; }
    [JsonPropertyName("isForwarded")] public bool IsForwarded { get; init; }
    [JsonPropertyName("viewOnce")] public bool ViewOnce { get; init; }

    public bool HasMedia => Media != null;
    public bool HasReaction => Reaction != null;
    public bool HasContact => Contact != null;
    public bool HasContacts => Contacts != null && Contacts.Length > 0;
    public bool HasReplyTo => ReplyTo != null;
    public bool HasExtendedText => ExtendedText != null;
    public bool HasMentions => Mentions != null && Mentions.Length > 0;
    public bool HasPin => Pin != null;
    public bool HasText => !string.IsNullOrEmpty(Text);
    public bool HasEdit => Edit != null; 
}
