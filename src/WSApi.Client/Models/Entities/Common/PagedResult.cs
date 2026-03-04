using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WSApi.Client.Models.Entities.Common;

public record PagedResult<T>
{
    [JsonPropertyName("items")] public List<T> Items { get; init; } = new();
    [JsonPropertyName("totalCount")] public int TotalCount { get; init; }
    [JsonPropertyName("pageNumber")] public int PageNumber { get; init; }
    [JsonPropertyName("pageSize")] public int PageSize { get; init; }
}
