using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendContactRequest : MessageRequestBase
{
    [JsonPropertyName("vCard")]
    public string VCard { get; init; } = null!;

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; init; }
}
