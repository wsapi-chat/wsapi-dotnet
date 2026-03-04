using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendContactRequest : MessageRequestBase
{
    [JsonPropertyName("vcard")]
    public string VCard { get; init; } = null!;

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; init; }
}
