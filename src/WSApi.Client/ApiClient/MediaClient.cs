using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Requests.Media;

namespace WSApi.Client.ApiClient;

public class MediaClient(HttpClient httpClient) : IMediaClient
{
    public async Task<byte[]> DownloadAsync(MediaDownloadRequest mediaDownloadRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/media/download", mediaDownloadRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowByteArrayAsync();
    }

    public async Task<ApiResponse<byte[]>> TryDownloadAync(MediaDownloadRequest mediaDownloadRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/media/download", mediaDownloadRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseByteArrayAsync();
    }
}

public interface IMediaClient
{
    Task<byte[]> DownloadAsync(MediaDownloadRequest mediaDownloadRequest, CancellationToken cancellationToken = default);
    
    Task<ApiResponse<byte[]>> TryDownloadAync(MediaDownloadRequest mediaDownloadRequest, CancellationToken cancellationToken = default);
}