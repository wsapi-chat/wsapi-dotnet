using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Contacts;

public record ContactInfo
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("lid")] public string? Lid { get; init; } = null!;
    [JsonPropertyName("phone")] public string? Phone { get; init; }
    [JsonPropertyName("device")] public int? Device { get; init; }
    [JsonPropertyName("fullName")] public string FullName { get; init; } = null!;
    [JsonPropertyName("firstName")] public string? FirstName { get; init; }
    [JsonPropertyName("pushName")] public string? PushName { get; init; }
    [JsonPropertyName("businessName")] public string? BusinessName { get; init; }
    [JsonPropertyName("inPhoneAddressBook")] public bool InPhoneAddressBook { get; init; }

}
