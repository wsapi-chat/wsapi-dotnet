using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Chats;
using WSApi.Client.Models.Entities.Messages;
using WSApi.Client.Models.Requests.Chats;

namespace WSApi.Client.ApiClient;

public class ChatsClient(HttpClient httpClient) : IChatsClient
{
    // These methods return the actual data or throw an exception if the request fails.

    public async Task<ChatInfo[]> ListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/chats", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<ChatInfo[]>();
    }
    
    public async Task<ChatInfo> GetAsync(string chatId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/chats/{chatId}", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<ChatInfo>();
    }
    
    public async Task<ChatPicture> GetPictureAsync(string chatId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/chats/{chatId}/picture", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<ChatPicture>();
    }
    
    public async Task<ChatBusinessProfile> GetBusinessProfileAsync(string chatId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/chats/{chatId}/business", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<ChatBusinessProfile>();
    }



    public async Task UpdatePresenceAsync(string chatId, ChatUpdatePresenceRequest chatUpdatePresenceRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/presence", chatUpdatePresenceRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    
    public async Task SuscribePresenceAsync(string chatId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/presence/subscribe", new { }, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    
    public async Task UpdateEphemeralAsync(string chatId, ChatUpdateEphemeralExpirationRequest chatUpdateEphemeralExpirationRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/ephemeral", chatUpdateEphemeralExpirationRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    
    public async Task UpdateMuteAsync(string chatId, ChatUpdateMuteRequest chatUpdateMuteRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/mute", chatUpdateMuteRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    
    public async Task UpdatePinAsync(string chatId, ChatUpdatePinRequest chatUpdatePinRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/pin", chatUpdatePinRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    
    public async Task UpdateArchiveAsync(string chatId, ChatUpdateArchiveRequest chatUpdateArchiveRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/archive", chatUpdateArchiveRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    
    public async Task UpdateReadAsync(string chatId, ChatUpdateReadRequest chatUpdateReadRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/read", chatUpdateReadRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    
    public async Task DeleteChatAsync(string chatId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/chats/{chatId}", cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task ClearChatAsync(string chatId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/chats/{chatId}/clear", null, cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task SynchronizeAllAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync($"/chats/sync", null, cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task RequestMessagesAsync(string chatId, RequestMessagesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"/chats/{chatId}/messages", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    
    // Try methods for error handling
    
    public async Task<ApiResponse<ChatInfo[]>> TryListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/chats", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<ChatInfo[]>();
    }
    
    public async Task<ApiResponse<ChatInfo>> TryGetAsync(string chatId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/chats/{chatId}", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<ChatInfo>();
    }
    
    public async Task<ApiResponse<ChatPicture>> TryGetPictureAsync(string chatId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/chats/{chatId}/picture", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<ChatPicture>();
    }
    
    public async Task<ApiResponse<ChatBusinessProfile>> TryGetBusinessProfileAsync(string chatId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/chats/{chatId}/business", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<ChatBusinessProfile>();
    }



    public async Task<ApiResponse> TryUpdatePresenceAsync(string chatId, ChatUpdatePresenceRequest chatUpdatePresenceRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/presence", chatUpdatePresenceRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    
    public async Task<ApiResponse> TrySuscribePresenceAsync(string chatId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/presence/subscribe", new { }, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    
    public async Task<ApiResponse> TryUpdateEphemeralAsync(string chatId, ChatUpdateEphemeralExpirationRequest chatUpdateEphemeralExpirationRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/ephemeral", chatUpdateEphemeralExpirationRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    
    public async Task<ApiResponse> TryUpdateMuteAsync(string chatId, ChatUpdateMuteRequest chatUpdateMuteRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/mute", chatUpdateMuteRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    
    public async Task<ApiResponse> TryUpdatePinAsync(string chatId, ChatUpdatePinRequest chatUpdatePinRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/pin", chatUpdatePinRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    
    public async Task<ApiResponse> TryUpdateArchiveAsync(string chatId, ChatUpdateArchiveRequest chatUpdateArchiveRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/archive", chatUpdateArchiveRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    
    public async Task<ApiResponse> TryUpdateReadAsync(string chatId, ChatUpdateReadRequest chatUpdateReadRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/chats/{chatId}/read", chatUpdateReadRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    
    public async Task<ApiResponse> TryDeleteChatAsync(string chatId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/chats/{chatId}", cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TryClearChatAsync(string chatId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/chats/{chatId}/clear", null, cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TrySynchronizeAllAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync($"/chats/sync", null, cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TryRequestMessagesAsync(string chatId, RequestMessagesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"/chats/{chatId}/messages", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
}

public interface IChatsClient
{
    Task<ChatInfo[]> ListAsync(CancellationToken cancellationToken = default);
    Task<ChatInfo> GetAsync(string chatId, CancellationToken cancellationToken = default);
    Task<ChatPicture> GetPictureAsync(string chatId, CancellationToken cancellationToken = default);
    Task<ChatBusinessProfile> GetBusinessProfileAsync(string chatId, CancellationToken cancellationToken = default);
    Task UpdatePresenceAsync(string chatId, ChatUpdatePresenceRequest chatUpdatePresenceRequest, CancellationToken cancellationToken = default);
    Task SuscribePresenceAsync(string chatId, CancellationToken cancellationToken = default);
    Task UpdateEphemeralAsync(string chatId, ChatUpdateEphemeralExpirationRequest chatUpdateEphemeralRequest, CancellationToken cancellationToken = default);
    Task UpdateMuteAsync(string chatId, ChatUpdateMuteRequest chatUpdateMuteRequest, CancellationToken cancellationToken = default);
    Task UpdatePinAsync(string chatId, ChatUpdatePinRequest chatUpdatePinRequest, CancellationToken cancellationToken = default);
    Task UpdateArchiveAsync(string chatId, ChatUpdateArchiveRequest chatUpdateArchiveRequest, CancellationToken cancellationToken = default);
    Task UpdateReadAsync(string chatId, ChatUpdateReadRequest chatUpdateReadRequest, CancellationToken cancellationToken = default);
    Task DeleteChatAsync(string chatId, CancellationToken cancellationToken = default);
    Task ClearChatAsync(string chatId, CancellationToken cancellationToken = default);
    Task SynchronizeAllAsync(CancellationToken cancellationToken = default);
    Task RequestMessagesAsync(string chatId, RequestMessagesRequest request, CancellationToken cancellationToken = default);

    Task<ApiResponse<ChatInfo[]>> TryListAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<ChatInfo>> TryGetAsync(string chatId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ChatPicture>> TryGetPictureAsync(string chatId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ChatBusinessProfile>> TryGetBusinessProfileAsync(string chatId, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdatePresenceAsync(string chatId, ChatUpdatePresenceRequest chatUpdatePresenceRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TrySuscribePresenceAsync(string chatId, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateEphemeralAsync(string chatId, ChatUpdateEphemeralExpirationRequest chatUpdateEphemeralRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateMuteAsync(string chatId, ChatUpdateMuteRequest chatUpdateMuteRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdatePinAsync(string chatId, ChatUpdatePinRequest chatUpdatePinRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateArchiveAsync(string chatId, ChatUpdateArchiveRequest chatUpdateArchiveRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateReadAsync(string chatId, ChatUpdateReadRequest chatUpdateReadRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryDeleteChatAsync(string chatId, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryClearChatAsync(string chatId, CancellationToken cancellationToken = default);
    Task<ApiResponse> TrySynchronizeAllAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse> TryRequestMessagesAsync(string chatId, RequestMessagesRequest request, CancellationToken cancellationToken = default);
}