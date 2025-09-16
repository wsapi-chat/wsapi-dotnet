using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Contacts;

public record ContactInfo
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("lid")] public string? Lid { get; init; } = null!;
    [JsonPropertyName("fullName")] public string FullName { get; init; } = null!;
    [JsonPropertyName("businessName")] public string BusinessName { get; init; } = null!;
    [JsonPropertyName("pushName")] public string PushName { get; init; } = null!;
    [JsonPropertyName("status")] public string Status { get; init; } = null!;
    [JsonPropertyName("pictureId")] public string PictureId { get; init; } = null!;
    [JsonPropertyName("inPhoneAddressBook")] public bool InPhoneAddressBook { get; init; }
    
}