using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Contacts;

public record ContactUpdateDescriptionRequest
{
    [JsonPropertyName("description")] public string Description { get; init; } = null!;

}