using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Users;

public record UserPictureEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("sender")] public Sender Sender { get; init; } = null!;
    [JsonPropertyName("pictureId")] public string PictureId { get; init; } = null!;
}
