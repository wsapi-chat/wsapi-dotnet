using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Newsletters;

public record ToggleMuteNewsletterRequest
{
    [JsonPropertyName("mute")] public bool Mute { get; init; }
}
