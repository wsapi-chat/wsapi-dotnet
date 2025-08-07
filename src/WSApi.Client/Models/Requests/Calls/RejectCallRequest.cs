using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Calls;

public class RejectCallRequest
{
    [JsonPropertyName("caller")] public string Caller { get; set; } = null!;
}