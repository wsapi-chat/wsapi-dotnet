using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Messages;
using WSApi.Client.Models.Entities.Newsletters;
using WSApi.Client.Models.Requests.Newsletters;

namespace WSApi.Client.ApiClient;

public class NewslettersClient(HttpClient httpClient) : INewslettersClient
{
    public async Task<NewsletterInfo[]> ListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/newsletters", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<NewsletterInfo[]>();
    }

    public async Task<NewsletterInfo> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/newsletters/{id}", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<NewsletterInfo>();
    }

    public async Task<NewsletterInfo> GetByInviteCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/newsletters/invite/{code}", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<NewsletterInfo>();
    }

    public async Task<MessageCreated> CreateAsync(CreateNewsletterRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/newsletters", request, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }

    public async Task SetSubscriptionAsync(string id, SetSubscriptionRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/newsletters/{id}/subscription", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task ToggleMuteAsync(string id, ToggleMuteNewsletterRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/newsletters/{id}/mute", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    // Try methods

    public async Task<ApiResponse<NewsletterInfo[]>> TryListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/newsletters", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<NewsletterInfo[]>();
    }

    public async Task<ApiResponse<NewsletterInfo>> TryGetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/newsletters/{id}", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<NewsletterInfo>();
    }

    public async Task<ApiResponse<NewsletterInfo>> TryGetByInviteCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/newsletters/invite/{code}", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<NewsletterInfo>();
    }

    public async Task<ApiResponse<MessageCreated>> TryCreateAsync(CreateNewsletterRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/newsletters", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }

    public async Task<ApiResponse> TrySetSubscriptionAsync(string id, SetSubscriptionRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/newsletters/{id}/subscription", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TryToggleMuteAsync(string id, ToggleMuteNewsletterRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/newsletters/{id}/mute", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
}

public interface INewslettersClient
{
    Task<NewsletterInfo[]> ListAsync(CancellationToken cancellationToken = default);
    Task<NewsletterInfo> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<NewsletterInfo> GetByInviteCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<MessageCreated> CreateAsync(CreateNewsletterRequest request, CancellationToken cancellationToken = default);
    Task SetSubscriptionAsync(string id, SetSubscriptionRequest request, CancellationToken cancellationToken = default);
    Task ToggleMuteAsync(string id, ToggleMuteNewsletterRequest request, CancellationToken cancellationToken = default);

    Task<ApiResponse<NewsletterInfo[]>> TryListAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<NewsletterInfo>> TryGetAsync(string id, CancellationToken cancellationToken = default);
    Task<ApiResponse<NewsletterInfo>> TryGetByInviteCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<ApiResponse<MessageCreated>> TryCreateAsync(CreateNewsletterRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> TrySetSubscriptionAsync(string id, SetSubscriptionRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryToggleMuteAsync(string id, ToggleMuteNewsletterRequest request, CancellationToken cancellationToken = default);
}
