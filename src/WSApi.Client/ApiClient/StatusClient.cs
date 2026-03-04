using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Messages;
using WSApi.Client.Models.Entities.Status;
using WSApi.Client.Models.Requests.Status;

namespace WSApi.Client.ApiClient;

public class StatusClient(HttpClient httpClient) : IStatusClient
{
    public async Task<StatusPrivacy[]> GetPrivacyAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/status/privacy", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<StatusPrivacy[]>();
    }

    public async Task<MessageCreated> PostTextAsync(PostTextStatusRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/status/text", request, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }

    public async Task<MessageCreated> PostImageAsync(PostMediaStatusRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/status/image", request, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }

    public async Task<MessageCreated> PostVideoAsync(PostMediaStatusRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/status/video", request, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }

    public async Task DeleteAsync(string messageId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/status/{messageId}/delete", null, cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    // Try methods

    public async Task<ApiResponse<StatusPrivacy[]>> TryGetPrivacyAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/status/privacy", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<StatusPrivacy[]>();
    }

    public async Task<ApiResponse<MessageCreated>> TryPostTextAsync(PostTextStatusRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/status/text", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }

    public async Task<ApiResponse<MessageCreated>> TryPostImageAsync(PostMediaStatusRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/status/image", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }

    public async Task<ApiResponse<MessageCreated>> TryPostVideoAsync(PostMediaStatusRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/status/video", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }

    public async Task<ApiResponse> TryDeleteAsync(string messageId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/status/{messageId}/delete", null, cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
}

public interface IStatusClient
{
    Task<StatusPrivacy[]> GetPrivacyAsync(CancellationToken cancellationToken = default);
    Task<MessageCreated> PostTextAsync(PostTextStatusRequest request, CancellationToken cancellationToken = default);
    Task<MessageCreated> PostImageAsync(PostMediaStatusRequest request, CancellationToken cancellationToken = default);
    Task<MessageCreated> PostVideoAsync(PostMediaStatusRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string messageId, CancellationToken cancellationToken = default);

    Task<ApiResponse<StatusPrivacy[]>> TryGetPrivacyAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<MessageCreated>> TryPostTextAsync(PostTextStatusRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<MessageCreated>> TryPostImageAsync(PostMediaStatusRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<MessageCreated>> TryPostVideoAsync(PostMediaStatusRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryDeleteAsync(string messageId, CancellationToken cancellationToken = default);
}
