using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Groups;

public record GroupInviteLink
{
    [JsonPropertyName("link")] public string InviteLink { get; init; } = null!;
}
