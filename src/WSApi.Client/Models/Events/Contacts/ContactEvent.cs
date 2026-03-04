using System.Text.Json.Serialization;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.Models.Events.Contacts;

public record ContactEvent : BaseEvent
{
    [JsonPropertyName("contact")] public Identity Contact { get; init; } = null!;
    [JsonPropertyName("fullName")] public string FullName { get; init; } = null!;
    [JsonPropertyName("inPhoneAddressBook")] public bool InPhoneAddressBook { get; init; }
}
