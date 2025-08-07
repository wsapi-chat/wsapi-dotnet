using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Chats;

public record ChatArchive
{
    [JsonPropertyName("isArchived")] public bool IsArchived { get; init; }
}