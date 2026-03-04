using System;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Newsletters;

public record NewsletterInfo
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("subscriberCount")] public int SubscriberCount { get; init; }
    [JsonPropertyName("verificationState")] public string? VerificationState { get; init; }
    [JsonPropertyName("pictureUrl")] public string? PictureUrl { get; init; }
    [JsonPropertyName("inviteCode")] public string? InviteCode { get; init; }
    [JsonPropertyName("role")] public string? Role { get; init; }
    [JsonPropertyName("mute")] public string? Mute { get; init; }
    [JsonPropertyName("state")] public string? State { get; init; }
    [JsonPropertyName("createdAt")] public DateTime? CreatedAt { get; init; }
}
