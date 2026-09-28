using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupMemberAddModeRequest
{
    [JsonPropertyName("onlyAdminAdd")] public bool OnlyAdmins { get; init; }
}
