using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Events.Contacts;

public record ContactEvent : BaseEvent
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("fullName")] public string FullName { get; init; } = null!;
    [JsonPropertyName("inPhoneAddressBook")] public bool InPhoneAddressBook { get; init; }
}
