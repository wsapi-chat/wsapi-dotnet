using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Groups;

public record GroupJoinApprovalRequest
{
    [JsonPropertyName("joinApproval")] public bool JoinApproval { get; init; }
}
