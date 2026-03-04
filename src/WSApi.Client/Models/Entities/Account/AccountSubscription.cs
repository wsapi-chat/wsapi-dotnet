using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Account;

public record AccountSubscription
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("created")] public DateTime Created { get; init; }
    [JsonPropertyName("curPeriodStart")] public DateTime CurPeriodStart { get; init; }
    [JsonPropertyName("curPeriodEnd")] public DateTime CurPeriodEnd { get; init; }
    [JsonPropertyName("quantity")] public int Quantity { get; init; }
    [JsonPropertyName("plan")] public string Plan { get; init; } = null!;
    [JsonPropertyName("price")] public string Price { get; init; } = null!;
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
    [JsonPropertyName("cancelAtPeriodEnd")] public bool CancelAtPeriodEnd { get; init; }
    [JsonPropertyName("instanceIds")] public string[]? InstanceIds { get; init; }
}
