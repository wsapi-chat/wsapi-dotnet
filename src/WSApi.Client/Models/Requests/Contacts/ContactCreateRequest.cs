using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Contacts;

public record ContactCreateRequest
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("fullName")] public string FullName { get; init; } = null!;
    [JsonPropertyName("firstName")] public string FirstName { get; init; } = null!;
}