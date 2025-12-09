using System;
using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Groups;

public record GroupEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("sender")] public Sender? Sender { get; init; }
    [JsonPropertyName("description")] public GroupDescriptionChange? Description { get; init; }
    [JsonPropertyName("timestamp")] public DateTime? Timestamp { get; init; }
    [JsonPropertyName("join")] public string[]? Join { get; init; }
    [JsonPropertyName("leave")] public string[]? Leave { get; init; }

    public bool HasDescriptionChange => Description != null;
    public bool HasJoin => Join != null && Join.Length > 0;
    public bool HasLeave => Leave != null && Leave.Length > 0;
}

public record GroupDescriptionChange
{
    [JsonPropertyName("topic")] public string? Topic { get; init; }
}
