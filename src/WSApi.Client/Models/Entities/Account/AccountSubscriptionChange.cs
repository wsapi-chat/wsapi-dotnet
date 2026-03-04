using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Account;

public record AccountSubscriptionChange
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("timestamp")] public DateTime Timestamp { get; init; }
    [JsonPropertyName("subscriptionId")] public string SubscriptionId { get; init; } = null!;
    [JsonPropertyName("changeType")] public string ChangeType { get; init; } = null!;
    [JsonPropertyName("previousQuantity")] public int? PreviousQuantity { get; init; }
    [JsonPropertyName("newQuantity")] public int? NewQuantity { get; init; }
    [JsonPropertyName("instanceId")] public string? InstanceId { get; init; }
    [JsonPropertyName("proratedAmount")] public decimal? ProratedAmount { get; init; }
    [JsonPropertyName("notes")] public string? Notes { get; init; }
}
