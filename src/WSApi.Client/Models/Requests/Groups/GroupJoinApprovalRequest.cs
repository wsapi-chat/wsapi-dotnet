using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupJoinApprovalRequest
{
    [JsonPropertyName("enabled")] public bool JoinApproval { get; init; }
}
