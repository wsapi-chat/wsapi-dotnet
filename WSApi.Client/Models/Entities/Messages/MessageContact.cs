using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Messages;

public record MessageContact
{
    [JsonPropertyName("displayName")] 
    public string DisplayName { get; init; } = null!;

    [JsonPropertyName("vCard")] 
    public string VCard { get; init; } = null!;
}