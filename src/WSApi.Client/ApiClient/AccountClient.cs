using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Accounts;
using WSApi.Client.Models.Requests.Account;

namespace WSApi.Client.ApiClient;

public class AccountClient(HttpClient httpClient) : IAccountClient
{

    // These methods return the actual data or throw an exception if the request fails.
    
    public async Task<AccountInfo> GetInfoAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/account/info", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<AccountInfo>();
    }

    public async Task UpdateNameAsync(AccountUpdateNameRequest updateNameRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/account/name", updateNameRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task UpdateStatusAsync(AccountUpdateStatusRequest updateStatusRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/account/status", updateStatusRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    
    public async Task<AccountUpdatePictureResponse> UpdatePictureAsync(AccountUpdatePictureRequest updatePictureRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/account/picture", updatePictureRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<AccountUpdatePictureResponse>();
    }
    
    public async Task UpdatePresenceAsync(AccountUpdatePresenceRequest updatePresenceRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/account/presence", updatePresenceRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }


    // Try methods for error handling. These methods return an ApiResponse object that contains the status and data.
    public async Task<ApiResponse<AccountInfo>> TryGetInfoAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/account/info", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<AccountInfo>();
    }
    
    public async Task<ApiResponse> TryUpdateNameAsync(AccountUpdateNameRequest updateNameRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/account/name", updateNameRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    
    public async Task<ApiResponse> TryUpdateStatusAsync(AccountUpdateStatusRequest updateStatusRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/account/status", updateStatusRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    
    public async Task<ApiResponse<AccountUpdatePictureResponse>> TryUpdatePictureAsync(AccountUpdatePictureRequest updatePictureRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/account/picture", updatePictureRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<AccountUpdatePictureResponse>();
    }
    
    public async Task<ApiResponse> TryUpdatePresenceAsync(AccountUpdatePresenceRequest updatePresenceRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/account/presence", updatePresenceRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
}

public interface IAccountClient
{
    Task<AccountInfo> GetInfoAsync(CancellationToken cancellationToken = default);
    Task UpdateNameAsync(AccountUpdateNameRequest updateNameRequest, CancellationToken cancellationToken = default);
    Task UpdateStatusAsync(AccountUpdateStatusRequest updateStatusRequest, CancellationToken cancellationToken = default);
    Task<AccountUpdatePictureResponse> UpdatePictureAsync(AccountUpdatePictureRequest updatePictureRequest, CancellationToken cancellationToken = default);
    Task UpdatePresenceAsync(AccountUpdatePresenceRequest updatePresenceRequest, CancellationToken cancellationToken = default);
    
    Task<ApiResponse<AccountInfo>> TryGetInfoAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateNameAsync(AccountUpdateNameRequest updateNameRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateStatusAsync(AccountUpdateStatusRequest updateStatusRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse<AccountUpdatePictureResponse>> TryUpdatePictureAsync(AccountUpdatePictureRequest updatePictureRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdatePresenceAsync(AccountUpdatePresenceRequest updatePresenceRequest, CancellationToken cancellationToken = default);
}
