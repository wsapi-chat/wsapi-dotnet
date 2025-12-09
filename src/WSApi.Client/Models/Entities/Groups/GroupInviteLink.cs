using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Groups;

public record GroupInviteLink
{
    [JsonPropertyName("inviteLink")] public string InviteLink { get; init; } = null!;
}
