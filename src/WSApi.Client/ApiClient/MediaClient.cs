using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WSApi.Client.ApiClient;

public class MediaClient(HttpClient httpClient) : IMediaClient
{
    public async Task<byte[]> DownloadAsync(string mediaId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/media/download?id={mediaId}", cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowByteArrayAsync();
    }

    public async Task<ApiResponse<byte[]>> TryDownloadAync(string mediaId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/media/download?id={mediaId}", cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseByteArrayAsync();
    }
}

public interface IMediaClient
{
    Task<byte[]> DownloadAsync(string mediaId, CancellationToken cancellationToken = default);

    Task<ApiResponse<byte[]>> TryDownloadAync(string mediaId, CancellationToken cancellationToken = default);
}