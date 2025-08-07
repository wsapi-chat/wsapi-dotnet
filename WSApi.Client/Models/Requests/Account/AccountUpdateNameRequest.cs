using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Account;

public record AccountUpdateNameRequest
{
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
}