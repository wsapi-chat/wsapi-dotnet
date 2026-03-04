using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Users;

public record CheckUserResponse
{
    [JsonPropertyName("isInWhatsApp")] public bool IsInWhatsApp { get; init; }
}
