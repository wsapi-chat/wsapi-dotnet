using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Requests.Messages;

public record MessageSendLocationRequest : MessageRequestBase
{
    /// <summary>
    ///  Latitude of the location. 
    /// </summary>
    [JsonPropertyName("latitude")]
    public double Latitude { get; init; }

    /// <summary>
    ///  Longitude of the location.
    /// </summary>
    [JsonPropertyName("longitude")]
    public double Longitude { get; init; }

    /// <summary>
    ///  Optional. The address of the location. This can be a full address or a description of the location.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>
    /// Optional. The name of the location. This can be a place name or a custom name for the location.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    ///  Optional. A URL to a map or a static image of the location. This can be used to provide additional context or visual representation of the location.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}