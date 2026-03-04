using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Account;
using WSApi.Client.Models.Entities.Common;
using WSApi.Client.Models.Requests.Account;

namespace WSApi.Client.ApiClient;

public class AccountClient(HttpClient httpClient) : IAccountClient
{
    // ─── Instances ──────────────────────────────────────────────────

    public async Task<PagedResult<AccountInstance>> ListInstances(int? pageNumber = null, int? pageSize = null,
        DateTime? createdFrom = null, DateTime? createdTo = null, string? status = null,
        CancellationToken cancellationToken = default)
    {
        var url = BuildUrl("/account/instances", pageNumber, pageSize, createdFrom, createdTo, status);
        var response = await httpClient.GetAsync(url, cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<PagedResult<AccountInstance>>();
    }

    public async Task<AccountInstanceDetail> GetInstance(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/account/instances/{id}", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<AccountInstanceDetail>();
    }

    public async Task UpdateInstanceName(string id, UpdateInstanceNameRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/account/instances/{id}/name", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task UpdateInstanceSettings(string id, AccountInstanceSettings settings, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/account/instances/{id}/settings", settings, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task<string> RegenerateInstanceApiKey(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync($"/account/instances/{id}/apikey", null, cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<string>();
    }

    public async Task RestartInstance(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync($"/account/instances/{id}/restart", null, cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    // ─── Instance Defaults ──────────────────────────────────────────

    public async Task<AccountInstanceDefaults> GetInstanceDefaults(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/account/instances/defaults", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<AccountInstanceDefaults>();
    }

    public async Task UpdateInstanceDefaults(AccountInstanceDefaults defaults, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/account/instances/defaults", defaults, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    // ─── Subscriptions ──────────────────────────────────────────────

    public async Task<PagedResult<AccountSubscription>> ListSubscriptions(int? pageNumber = null, int? pageSize = null,
        CancellationToken cancellationToken = default)
    {
        var url = BuildUrl("/account/subscriptions", pageNumber, pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<PagedResult<AccountSubscription>>();
    }

    public async Task<PagedResult<AccountInstance>> ListSubscriptionInstances(string id, int? pageNumber = null, int? pageSize = null,
        DateTime? createdFrom = null, DateTime? createdTo = null, string? status = null,
        CancellationToken cancellationToken = default)
    {
        var url = BuildUrl($"/account/subscriptions/{id}/instances", pageNumber, pageSize, createdFrom, createdTo, status);
        var response = await httpClient.GetAsync(url, cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<PagedResult<AccountInstance>>();
    }

    public async Task<string> CreateSubscriptionInstance(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/account/subscriptions/{id}/instances", null, cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<string>();
    }

    public async Task DeleteSubscriptionInstance(string id, string instanceId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/account/subscriptions/{id}/instances/{instanceId}", cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task<PagedResult<AccountSubscriptionChange>> ListSubscriptionChanges(string id, int? pageNumber = null, int? pageSize = null,
        DateTime? timestampFrom = null, DateTime? timestampTo = null, string? changeType = null, string? instanceId = null,
        CancellationToken cancellationToken = default)
    {
        var url = BuildSubscriptionChangesUrl($"/account/subscriptions/{id}/changes", pageNumber, pageSize, timestampFrom, timestampTo, changeType, instanceId);
        var response = await httpClient.GetAsync(url, cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<PagedResult<AccountSubscriptionChange>>();
    }

    // ─── Try* variants ──────────────────────────────────────────────

    public async Task<ApiResponse<PagedResult<AccountInstance>>> TryListInstances(int? pageNumber = null, int? pageSize = null,
        DateTime? createdFrom = null, DateTime? createdTo = null, string? status = null,
        CancellationToken cancellationToken = default)
    {
        var url = BuildUrl("/account/instances", pageNumber, pageSize, createdFrom, createdTo, status);
        var response = await httpClient.GetAsync(url, cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<PagedResult<AccountInstance>>();
    }

    public async Task<ApiResponse<AccountInstanceDetail>> TryGetInstance(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/account/instances/{id}", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<AccountInstanceDetail>();
    }

    public async Task<ApiResponse> TryUpdateInstanceName(string id, UpdateInstanceNameRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/account/instances/{id}/name", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TryUpdateInstanceSettings(string id, AccountInstanceSettings settings, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/account/instances/{id}/settings", settings, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<string>> TryRegenerateInstanceApiKey(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync($"/account/instances/{id}/apikey", null, cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<string>();
    }

    public async Task<ApiResponse> TryRestartInstance(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync($"/account/instances/{id}/restart", null, cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<AccountInstanceDefaults>> TryGetInstanceDefaults(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/account/instances/defaults", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<AccountInstanceDefaults>();
    }

    public async Task<ApiResponse> TryUpdateInstanceDefaults(AccountInstanceDefaults defaults, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/account/instances/defaults", defaults, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<PagedResult<AccountSubscription>>> TryListSubscriptions(int? pageNumber = null, int? pageSize = null,
        CancellationToken cancellationToken = default)
    {
        var url = BuildUrl("/account/subscriptions", pageNumber, pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<PagedResult<AccountSubscription>>();
    }

    public async Task<ApiResponse<PagedResult<AccountInstance>>> TryListSubscriptionInstances(string id, int? pageNumber = null, int? pageSize = null,
        DateTime? createdFrom = null, DateTime? createdTo = null, string? status = null,
        CancellationToken cancellationToken = default)
    {
        var url = BuildUrl($"/account/subscriptions/{id}/instances", pageNumber, pageSize, createdFrom, createdTo, status);
        var response = await httpClient.GetAsync(url, cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<PagedResult<AccountInstance>>();
    }

    public async Task<ApiResponse<string>> TryCreateSubscriptionInstance(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/account/subscriptions/{id}/instances", null, cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<string>();
    }

    public async Task<ApiResponse> TryDeleteSubscriptionInstance(string id, string instanceId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/account/subscriptions/{id}/instances/{instanceId}", cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<PagedResult<AccountSubscriptionChange>>> TryListSubscriptionChanges(string id, int? pageNumber = null, int? pageSize = null,
        DateTime? timestampFrom = null, DateTime? timestampTo = null, string? changeType = null, string? instanceId = null,
        CancellationToken cancellationToken = default)
    {
        var url = BuildSubscriptionChangesUrl($"/account/subscriptions/{id}/changes", pageNumber, pageSize, timestampFrom, timestampTo, changeType, instanceId);
        var response = await httpClient.GetAsync(url, cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<PagedResult<AccountSubscriptionChange>>();
    }

    // ─── Helpers ────────────────────────────────────────────────────

    private static string BuildUrl(string basePath, int? pageNumber, int? pageSize,
        DateTime? createdFrom = null, DateTime? createdTo = null, string? status = null)
    {
        var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
        if (pageNumber.HasValue) query["pageNumber"] = pageNumber.Value.ToString();
        if (pageSize.HasValue) query["pageSize"] = pageSize.Value.ToString();
        if (createdFrom.HasValue) query["createdFrom"] = createdFrom.Value.ToString("o");
        if (createdTo.HasValue) query["createdTo"] = createdTo.Value.ToString("o");
        if (status != null) query["status"] = status;
        var qs = query.ToString();
        return string.IsNullOrEmpty(qs) ? basePath : $"{basePath}?{qs}";
    }

    private static string BuildSubscriptionChangesUrl(string basePath, int? pageNumber, int? pageSize,
        DateTime? timestampFrom, DateTime? timestampTo, string? changeType, string? instanceId)
    {
        var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
        if (pageNumber.HasValue) query["pageNumber"] = pageNumber.Value.ToString();
        if (pageSize.HasValue) query["pageSize"] = pageSize.Value.ToString();
        if (timestampFrom.HasValue) query["timestampFrom"] = timestampFrom.Value.ToString("o");
        if (timestampTo.HasValue) query["timestampTo"] = timestampTo.Value.ToString("o");
        if (changeType != null) query["changeType"] = changeType;
        if (instanceId != null) query["instanceId"] = instanceId;
        var qs = query.ToString();
        return string.IsNullOrEmpty(qs) ? basePath : $"{basePath}?{qs}";
    }
}

public interface IAccountClient
{
    // Instances
    Task<PagedResult<AccountInstance>> ListInstances(int? pageNumber = null, int? pageSize = null,
        DateTime? createdFrom = null, DateTime? createdTo = null, string? status = null,
        CancellationToken cancellationToken = default);
    Task<AccountInstanceDetail> GetInstance(string id, CancellationToken cancellationToken = default);
    Task UpdateInstanceName(string id, UpdateInstanceNameRequest request, CancellationToken cancellationToken = default);
    Task UpdateInstanceSettings(string id, AccountInstanceSettings settings, CancellationToken cancellationToken = default);
    Task<string> RegenerateInstanceApiKey(string id, CancellationToken cancellationToken = default);
    Task RestartInstance(string id, CancellationToken cancellationToken = default);

    // Instance Defaults
    Task<AccountInstanceDefaults> GetInstanceDefaults(CancellationToken cancellationToken = default);
    Task UpdateInstanceDefaults(AccountInstanceDefaults defaults, CancellationToken cancellationToken = default);

    // Subscriptions
    Task<PagedResult<AccountSubscription>> ListSubscriptions(int? pageNumber = null, int? pageSize = null,
        CancellationToken cancellationToken = default);
    Task<PagedResult<AccountInstance>> ListSubscriptionInstances(string id, int? pageNumber = null, int? pageSize = null,
        DateTime? createdFrom = null, DateTime? createdTo = null, string? status = null,
        CancellationToken cancellationToken = default);
    Task<string> CreateSubscriptionInstance(string id, CancellationToken cancellationToken = default);
    Task DeleteSubscriptionInstance(string id, string instanceId, CancellationToken cancellationToken = default);
    Task<PagedResult<AccountSubscriptionChange>> ListSubscriptionChanges(string id, int? pageNumber = null, int? pageSize = null,
        DateTime? timestampFrom = null, DateTime? timestampTo = null, string? changeType = null, string? instanceId = null,
        CancellationToken cancellationToken = default);

    // Try* variants
    Task<ApiResponse<PagedResult<AccountInstance>>> TryListInstances(int? pageNumber = null, int? pageSize = null,
        DateTime? createdFrom = null, DateTime? createdTo = null, string? status = null,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<AccountInstanceDetail>> TryGetInstance(string id, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateInstanceName(string id, UpdateInstanceNameRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateInstanceSettings(string id, AccountInstanceSettings settings, CancellationToken cancellationToken = default);
    Task<ApiResponse<string>> TryRegenerateInstanceApiKey(string id, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryRestartInstance(string id, CancellationToken cancellationToken = default);
    Task<ApiResponse<AccountInstanceDefaults>> TryGetInstanceDefaults(CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateInstanceDefaults(AccountInstanceDefaults defaults, CancellationToken cancellationToken = default);
    Task<ApiResponse<PagedResult<AccountSubscription>>> TryListSubscriptions(int? pageNumber = null, int? pageSize = null,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<PagedResult<AccountInstance>>> TryListSubscriptionInstances(string id, int? pageNumber = null, int? pageSize = null,
        DateTime? createdFrom = null, DateTime? createdTo = null, string? status = null,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<string>> TryCreateSubscriptionInstance(string id, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryDeleteSubscriptionInstance(string id, string instanceId, CancellationToken cancellationToken = default);
    Task<ApiResponse<PagedResult<AccountSubscriptionChange>>> TryListSubscriptionChanges(string id, int? pageNumber = null, int? pageSize = null,
        DateTime? timestampFrom = null, DateTime? timestampTo = null, string? changeType = null, string? instanceId = null,
        CancellationToken cancellationToken = default);
}
