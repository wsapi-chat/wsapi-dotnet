using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Session;

public record SessionLoggedOutEvent : BaseEvent
{
    [JsonPropertyName("reason")] public string? Reason { get; init; }
}
