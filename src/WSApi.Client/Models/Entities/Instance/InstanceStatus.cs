using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Instance;

public record InstanceStatus
{
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
}   