using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Groups;

public record GroupJoinRequest
{
    [JsonPropertyName("userId")] public string UserId { get; init; } = null!;
    [JsonPropertyName("requestedAt")] public DateTime RequestedAt { get; init; }
}
