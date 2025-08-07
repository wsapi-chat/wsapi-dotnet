using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Account;

public record AccountUpdateStatusRequest
{
    [JsonPropertyName("status")] public string Status { get; set; } = null!;
} 