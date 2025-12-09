using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Entities.Messages;

public record MessageReplyTo
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("sender")] public Sender? Sender { get; init; }
    [JsonPropertyName("isForwarded")] public bool IsForwarded { get; init; }
    [JsonPropertyName("text")] public string? Text { get; init; }
}