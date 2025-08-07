using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Entities.Chats;

public record ChatEphemeral
{
    [JsonPropertyName("expiration")] public string Expiration { get; init; } = null!;
    [JsonPropertyName("sender")] public Sender Sender { get; init; } = null!;
}