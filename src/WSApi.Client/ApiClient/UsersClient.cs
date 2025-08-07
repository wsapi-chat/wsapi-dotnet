using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Users;

namespace WSApi.Client.ApiClient;

public class UsersClient(HttpClient httpClient) : IUsersClient
{
    public async Task<UserInfo> GetAsync(string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/users/{userId}", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<UserInfo>();
    }

    public async Task<ApiResponse<UserInfo>> TryGetAsync(string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/users/{userId}", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<UserInfo>();
    }
}

public interface IUsersClient
{
    Task<UserInfo> GetAsync(string phoneNumber, CancellationToken cancellationToken = default);

    Task<ApiResponse<UserInfo>> TryGetAsync(string phoneNumber, CancellationToken cancellationToken = default);
}