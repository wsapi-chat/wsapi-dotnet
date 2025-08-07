using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Session;

namespace WSApi.Client.ApiClient;

public class SessionClient(HttpClient httpClient) : ISessionClient
{
    public async Task<byte[]> GetLoginQRImageAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/login/qr/image", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<byte[]>();
    }

    public async Task<string> GetLoginQRCodeAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/login/qr/code", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<string>();
    }

    public async Task<SessionPairCode> GetLoginPairCodeAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/login/code/{phoneNumber}", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<SessionPairCode>();
    }

    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/session/logout", null, cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task<SessionStatus> GetSessionStatusAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/status", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<SessionStatus>();
    }

    public async Task<ApiResponse<byte[]>> TryGetLoginQRImageAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/login/qr/image", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<byte[]>();
    }

    public async Task<ApiResponse<string>> TryGetLoginQRCodeAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/login/qr/code", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<string>();
    }
    
    public async Task<ApiResponse<SessionPairCode>> TryGetLoginPairCodeAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/login/code/{phoneNumber}", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<SessionPairCode>();
    }

    public async Task<ApiResponse> TryLogoutAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/session/logout", null, cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<SessionStatus>> TryGetSessionStatusAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/status", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<SessionStatus>();
    }
}

public interface ISessionClient
{
    Task<byte[]> GetLoginQRImageAsync(CancellationToken cancellationToken = default);
    Task<string> GetLoginQRCodeAsync(CancellationToken cancellationToken = default);
    Task<SessionPairCode> GetLoginPairCodeAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task LogoutAsync(CancellationToken cancellationToken = default);
    Task<SessionStatus> GetSessionStatusAsync(CancellationToken cancellationToken = default);

    Task<ApiResponse<byte[]>> TryGetLoginQRImageAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<string>> TryGetLoginQRCodeAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<SessionPairCode>> TryGetLoginPairCodeAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryLogoutAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<SessionStatus>> TryGetSessionStatusAsync(CancellationToken cancellationToken = default);
}