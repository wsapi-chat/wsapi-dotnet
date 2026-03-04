using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Calls;

public class RejectCallRequest
{
    [JsonPropertyName("callerId")] public string CallerId { get; set; } = null!;
}