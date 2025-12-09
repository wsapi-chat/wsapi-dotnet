using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupMemberAddModeRequest
{
    [JsonPropertyName("onlyAdmins")] public bool OnlyAdmins { get; init; }
}
