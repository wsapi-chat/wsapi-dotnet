using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Users;
using WSApi.Client.Models.Requests.Users;

namespace WSApi.Client.ApiClient;

public class UsersClient(HttpClient httpClient) : IUsersClient
{
    // ─── Own profile ────────────────────────────────────────────────

    public async Task<UserMeInfo> GetMyProfile(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/users/me/profile", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<UserMeInfo>();
    }

    public async Task UpdateMyProfile(UpdateProfileRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/users/me/profile", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task SetPresence(SetMyPresenceRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/users/me/presence", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    // ─── Privacy ────────────────────────────────────────────────────

    public async Task<PrivacySettings> GetPrivacySettingsAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/users/me/privacy", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<PrivacySettings>();
    }

    public async Task<PrivacySettings> SetPrivacySettingAsync(SetPrivacyRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/users/me/privacy", request, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<PrivacySettings>();
    }

    // ─── User lookup ────────────────────────────────────────────────

    public async Task<CheckUserResponse> CheckUser(string phone, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/users/{phone}/check", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<CheckUserResponse>();
    }

    public async Task<UserInfo> GetUserProfile(string phone, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/users/{phone}/profile", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<UserInfo>();
    }

    public async Task<BulkCheckResult[]> BulkCheckAsync(BulkCheckRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/users/check", request, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<BulkCheckResult[]>();
    }

    // ─── Try* variants ──────────────────────────────────────────────

    public async Task<ApiResponse<UserMeInfo>> TryGetMyProfile(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/users/me/profile", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<UserMeInfo>();
    }

    public async Task<ApiResponse> TryUpdateMyProfile(UpdateProfileRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/users/me/profile", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TrySetPresence(SetMyPresenceRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/users/me/presence", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<PrivacySettings>> TryGetPrivacySettingsAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/users/me/privacy", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<PrivacySettings>();
    }

    public async Task<ApiResponse<PrivacySettings>> TrySetPrivacySettingAsync(SetPrivacyRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("/users/me/privacy", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<PrivacySettings>();
    }

    public async Task<ApiResponse<CheckUserResponse>> TryCheckUser(string phone, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/users/{phone}/check", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<CheckUserResponse>();
    }

    public async Task<ApiResponse<UserInfo>> TryGetUserProfile(string phone, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/users/{phone}/profile", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<UserInfo>();
    }

    public async Task<ApiResponse<BulkCheckResult[]>> TryBulkCheckAsync(BulkCheckRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/users/check", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<BulkCheckResult[]>();
    }
}

public interface IUsersClient
{
    // Own profile
    Task<UserMeInfo> GetMyProfile(CancellationToken cancellationToken = default);
    Task UpdateMyProfile(UpdateProfileRequest request, CancellationToken cancellationToken = default);
    Task SetPresence(SetMyPresenceRequest request, CancellationToken cancellationToken = default);

    // Privacy
    Task<PrivacySettings> GetPrivacySettingsAsync(CancellationToken cancellationToken = default);
    Task<PrivacySettings> SetPrivacySettingAsync(SetPrivacyRequest request, CancellationToken cancellationToken = default);

    // User lookup
    Task<CheckUserResponse> CheckUser(string phone, CancellationToken cancellationToken = default);
    Task<UserInfo> GetUserProfile(string phone, CancellationToken cancellationToken = default);
    Task<BulkCheckResult[]> BulkCheckAsync(BulkCheckRequest request, CancellationToken cancellationToken = default);

    // Try* variants
    Task<ApiResponse<UserMeInfo>> TryGetMyProfile(CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateMyProfile(UpdateProfileRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> TrySetPresence(SetMyPresenceRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<PrivacySettings>> TryGetPrivacySettingsAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<PrivacySettings>> TrySetPrivacySettingAsync(SetPrivacyRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CheckUserResponse>> TryCheckUser(string phone, CancellationToken cancellationToken = default);
    Task<ApiResponse<UserInfo>> TryGetUserProfile(string phone, CancellationToken cancellationToken = default);
    Task<ApiResponse<BulkCheckResult[]>> TryBulkCheckAsync(BulkCheckRequest request, CancellationToken cancellationToken = default);
}
