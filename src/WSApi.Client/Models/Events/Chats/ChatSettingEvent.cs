using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Chats;

namespace WSApi.Client.Models.Events.Chats;

public record ChatSettingEvent: BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("settingType")] public string SetttingType { get; init; } = null!;
    [JsonPropertyName("archive")] public ChatArchive? Archive { get; init; }
    [JsonPropertyName("pin")] public ChatPin? Pin { get; init; }
    [JsonPropertyName("read")] public ChatRead? Read { get; init; }
    [JsonPropertyName("mute")] public ChatMute? Mute { get; init; }
    [JsonPropertyName("ephemeral")] public ChatEphemeral? Ephemeral { get; init; }
}
