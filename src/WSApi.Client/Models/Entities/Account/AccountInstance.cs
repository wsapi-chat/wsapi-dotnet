using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Account;

public record AccountInstance
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("created")] public DateTime Created { get; init; }
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
    [JsonPropertyName("useCustomDefaults")] public bool UseCustomDefaults { get; init; }
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
    [JsonPropertyName("deviceId")] public string? DeviceId { get; init; }
    [JsonPropertyName("expiredAt")] public DateTime? ExpiredAt { get; init; }
    [JsonPropertyName("trialEndsAt")] public DateTime? TrialEndsAt { get; init; }
    [JsonPropertyName("isInTrial")] public bool IsInTrial { get; init; }
    [JsonPropertyName("hasApiKey")] public bool HasApiKey { get; init; }
}
