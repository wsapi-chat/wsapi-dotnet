using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Instance;

namespace WSApi.Client.ApiClient;

public class InstanceClient(HttpClient httpClient) : IInstanceClient
{
    public async Task<InstanceSettings> GetSettings(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/instance/settings", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<InstanceSettings>();
    }

    public async Task UpdateSettings(InstanceSettings settings, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/instance/settings", settings, cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task Restart(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync("/instance/restart", null, cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task<string> UpdateApiKey(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync("/instance/apikey", null, cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<string>();
    }

    public async Task<ApiResponse<InstanceSettings>> TryGetSettings(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/instance/settings", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<InstanceSettings>();
    }

    public async Task<ApiResponse> TryUpdateSettings(InstanceSettings settings, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/instance/settings", settings, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TryRestart(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync("/instance/restart", null, cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<string>> TryUpdateApiKey(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync("/instance/apikey", null, cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<string>();
    }
}

public interface IInstanceClient
{
    Task<InstanceSettings> GetSettings(CancellationToken cancellationToken = default);
    Task UpdateSettings(InstanceSettings settings, CancellationToken cancellationToken = default);
    Task Restart(CancellationToken cancellationToken = default);
    Task<string> UpdateApiKey(CancellationToken cancellationToken = default);

    Task<ApiResponse<InstanceSettings>> TryGetSettings(CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateSettings(InstanceSettings settings, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryRestart(CancellationToken cancellationToken = default);
    Task<ApiResponse<string>> TryUpdateApiKey(CancellationToken cancellationToken = default);
}