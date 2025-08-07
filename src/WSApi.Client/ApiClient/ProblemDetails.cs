using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WSApi.Client.ApiClient;

public sealed class ProblemDetails
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int? Status { get; set; }
    public string? Detail { get; set; }
    public string? Instance { get; set; }

    // Captures any extra fields (e.g., validation errors) without coupling to ASP.NET types
    [JsonExtensionData] 
    public Dictionary<string, JsonElement>? Extensions { get; set; }
}