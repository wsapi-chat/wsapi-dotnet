using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupParticipantRequest
{
    [JsonPropertyName("userId")] public string UserId { get; init; } = null!;
    [JsonPropertyName("requestedAt")] public DateTime RequestedAt { get; init; }
    
}