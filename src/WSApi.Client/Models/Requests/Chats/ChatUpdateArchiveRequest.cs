using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Chats;

public class ChatUpdateArchiveRequest
{
    [JsonPropertyName("archived")] public bool Archived { get; set; }
} 