using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Account;

public record AccountUpdatePresenceRequest
{
    [JsonPropertyName("status")] public string Status { get; set; } = null!;
} 