using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Newsletters;

public record SetSubscriptionRequest
{
    [JsonPropertyName("subscribed")] public bool Subscribed { get; init; }
}
