using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Chats;

public class ChatRead
{
    [JsonPropertyName("isRead")] public bool IsRead { get; init; }
}