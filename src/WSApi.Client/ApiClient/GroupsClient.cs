using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Groups;
using WSApi.Client.Models.Requests.Groups;

namespace WSApi.Client.ApiClient;

public class GroupsClient(HttpClient httpClient) : IGroupsClient
{
 
    public async Task<GroupInfo[]> ListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/groups", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupInfo[]>();
    }
    public async Task<GroupInfo> GetAsync(string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/groups/{groupId}", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupInfo>();
    }
    public async Task<GroupCreated> CreateAsync(GroupCreateRequest groupCreateRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/groups", groupCreateRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupCreated>();
    }
    public async Task UpdateDescriptionAsync(string groupId, GroupUpdateDescriptionRequest groupUpdateDescriptionRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/description", groupUpdateDescriptionRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    public async Task UpdateNameAsync(string groupId, GroupUpdateNameRequest groupUpdateNameRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/name", groupUpdateNameRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    public async Task<GroupPictureUpdated> UpdatePictureAsync(string groupId, GroupUpdatePictureRequest groupUpdatePictureRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"/groups/{groupId}/picture", groupUpdatePictureRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupPictureUpdated>();
    }
    public async Task LeaveGroupAsync(string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync($"/groups/{groupId}", null, cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    
    
    
    public async Task<ApiResponse<GroupInfo[]>> TryListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/groups", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupInfo[]>();
    }
    public async Task<ApiResponse<GroupInfo>> TryGetAsync(string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/groups/{groupId}", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupInfo>();
    }

    public async Task<ApiResponse<GroupCreated>> TryCreateAsync(GroupCreateRequest groupCreateRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/groups", groupCreateRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupCreated>();
    }
    public async Task<ApiResponse> TryUpdateDescriptionAsync(string groupId, GroupUpdateDescriptionRequest groupUpdateDescriptionRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/description", groupUpdateDescriptionRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    public async Task<ApiResponse> TryUpdateNameAsync(string groupId, GroupUpdateNameRequest groupUpdateNameRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/name", groupUpdateNameRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    public async Task<ApiResponse<GroupPictureUpdated>> TryUpdatePictureAsync(string groupId, GroupUpdatePictureRequest groupUpdatePictureRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"/groups/{groupId}/picture", groupUpdatePictureRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupPictureUpdated>();
    }
    public async Task<ApiResponse> TryLeaveGroupAsync(string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync($"/groups/{groupId}/leave", null, cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
}

public interface IGroupsClient
{
    Task<GroupInfo[]> ListAsync(CancellationToken cancellationToken = default);
    Task<GroupInfo> GetAsync(string groupId, CancellationToken cancellationToken = default);

    Task<GroupCreated> CreateAsync(GroupCreateRequest groupCreateRequest, CancellationToken cancellationToken = default);
    
    Task UpdateDescriptionAsync(string groupId, GroupUpdateDescriptionRequest groupUpdateDescriptionRequest, CancellationToken cancellationToken = default);
    Task UpdateNameAsync(string groupId, GroupUpdateNameRequest groupUpdateNameRequest, CancellationToken cancellationToken = default);
    Task<GroupPictureUpdated> UpdatePictureAsync(string groupId, GroupUpdatePictureRequest groupUpdatePictureRequest, CancellationToken cancellationToken = default);
    Task LeaveGroupAsync(string groupId, CancellationToken cancellationToken = default);
    
    
    Task<ApiResponse<GroupInfo[]>> TryListAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupInfo>> TryGetAsync(string groupId, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupCreated>> TryCreateAsync(GroupCreateRequest groupCreateRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateDescriptionAsync(string groupId, GroupUpdateDescriptionRequest groupUpdateDescriptionRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateNameAsync(string groupId, GroupUpdateNameRequest groupUpdateNameRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupPictureUpdated>> TryUpdatePictureAsync(string groupId, GroupUpdatePictureRequest groupUpdatePictureRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryLeaveGroupAsync(string groupId, CancellationToken cancellationToken = default);
}