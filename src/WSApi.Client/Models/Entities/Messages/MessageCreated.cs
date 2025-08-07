using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Messages;

public class MessageCreated
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
}