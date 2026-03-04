using System;
using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Common;

namespace WSApi.Client.Models.Entities.Groups;

public record GroupJoinRequest
{
    [JsonPropertyName("user")] public Identity User { get; init; } = null!;
    [JsonPropertyName("requestedAt")] public DateTime RequestedAt { get; init; }
}
