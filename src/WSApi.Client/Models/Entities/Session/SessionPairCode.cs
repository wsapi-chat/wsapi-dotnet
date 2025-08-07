using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Session;

public class SessionPairCode
{
    [JsonPropertyName("code")] public string Code { get; init; } = null!;
}