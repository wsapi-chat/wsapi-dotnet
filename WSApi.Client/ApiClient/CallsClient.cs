using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Requests.Calls;

namespace WSApi.Client.ApiClient;

public class CallsClient(HttpClient httpClient) : ICallsClient
{
    // These methods return the actual data or throw an exception if the request fails.
    public async Task RejectCallAsync(string callId, RejectCallRequest rejectCallRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/calls/{callId}/reject", rejectCallRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    // Try methods for error handling. These methods return an ApiResponse object that contains the status and data.
    public async Task<ApiResponse> TryRejectCallAsync(string callId, RejectCallRequest rejectCallRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/calls/{callId}/reject", rejectCallRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
}

public interface ICallsClient
{
    Task RejectCallAsync(string callId, RejectCallRequest rejectCallRequest, CancellationToken cancellationToken = default);

    public Task<ApiResponse> TryRejectCallAsync(string callId, RejectCallRequest rejectCallRequest, CancellationToken cancellationToken = default);
}