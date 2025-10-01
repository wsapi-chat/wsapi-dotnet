using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Chats;

public record ChatBusinessProfile
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("address")] public string Address { get; init; } = null!;
    [JsonPropertyName("description")] public string Description { get; init; } = null!;
    [JsonPropertyName("email")] public string Email { get; init; } = null!;
    [JsonPropertyName("website")] public string Website { get; init; } = null!;
    [JsonPropertyName("latitude")] public double Latitude { get; init; }
    [JsonPropertyName("longitude")] public double Longitude { get; init; }
    [JsonPropertyName("memberSince")] public string MemberSince { get; init; } = null!;
    [JsonPropertyName("categories")] public ChatBusinessCategory[] BusinessCategories { get; init; } = null!;
    [JsonPropertyName("businessHoursTimeZone")] public string BusinessHoursTimeZone { get; init; } = null!;
    [JsonPropertyName("businessHours")] public BusinessHours[] BusinessHours { get; init; } = null!;
    [JsonPropertyName("profileOptions")] public Dictionary<string,string> ProfileOptions { get; init; } = null!;
}

public record ChatBusinessCategory
{
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
}

public record BusinessHours
{
    [JsonPropertyName("dayOfWeek")] public string DayOfWeek { get; init; } = null!;
    [JsonPropertyName("mode")] public string Mode { get; init; } = null!;
    [JsonPropertyName("openTime")] public string OpenTime { get; init; } = null!;
    [JsonPropertyName("closeTime")] public string CloseTime { get; init; } = null!;
}