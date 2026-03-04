using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Account;

public record AccountInstanceDetail
{
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
    [JsonPropertyName("hasApiKey")] public bool HasApiKey { get; init; }
    [JsonPropertyName("settings")] public AccountInstanceSettings Settings { get; init; } = null!;
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
    [JsonPropertyName("deviceId")] public string? DeviceId { get; init; }
    [JsonPropertyName("created")] public DateTime Created { get; init; }
}
