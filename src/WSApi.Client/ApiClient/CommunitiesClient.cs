using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Communities;
using WSApi.Client.Models.Entities.Groups;
using WSApi.Client.Models.Requests.Communities;

namespace WSApi.Client.ApiClient;

public class CommunitiesClient(HttpClient httpClient) : ICommunitiesClient
{
    public async Task<CommunityInfo[]> ListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/communities", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<CommunityInfo[]>();
    }

    public async Task<CommunityCreated> CreateAsync(CommunityCreateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/communities", request, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<CommunityCreated>();
    }

    public async Task<CommunityInfo> GetAsync(string communityId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/communities/{communityId}", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<CommunityInfo>();
    }

    public async Task LeaveAsync(string communityId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/communities/{communityId}/leave", null, cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task UpdateNameAsync(string communityId, CommunityUpdateNameRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/communities/{communityId}/name", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task UpdateDescriptionAsync(string communityId, CommunityUpdateDescriptionRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/communities/{communityId}/description", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task<CommunityPictureUpdated> UpdatePictureAsync(string communityId, CommunityUpdatePictureRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"/communities/{communityId}/picture", request, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<CommunityPictureUpdated>();
    }

    public async Task SetLockedAsync(string communityId, CommunityUpdateLockedRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/communities/{communityId}/settings/locked", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task<GroupParticipantInfo[]> GetParticipantsAsync(string communityId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/communities/{communityId}/participants", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupParticipantInfo[]>();
    }

    public async Task UpdateParticipantsAsync(string communityId, CommunityUpdateParticipantsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/communities/{communityId}/participants", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task<CommunityInviteLink> GetInviteLinkAsync(string communityId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/communities/{communityId}/invite-link", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<CommunityInviteLink>();
    }

    public async Task<CommunityInviteLink> ResetInviteLinkAsync(string communityId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/communities/{communityId}/invite-link/reset", null, cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<CommunityInviteLink>();
    }

    public async Task<GroupInfo[]> GetSubGroupsAsync(string communityId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/communities/{communityId}/groups", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupInfo[]>();
    }

    public async Task<GroupCreated> CreateGroupAsync(string communityId, CommunityCreateGroupRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"/communities/{communityId}/groups", request, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupCreated>();
    }

    public async Task LinkGroupAsync(string communityId, CommunityLinkGroupRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"/communities/{communityId}/groups/link", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task UnlinkGroupAsync(string communityId, string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/communities/{communityId}/groups/{groupId}", cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    // Try methods

    public async Task<ApiResponse<CommunityInfo[]>> TryListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/communities", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<CommunityInfo[]>();
    }

    public async Task<ApiResponse<CommunityCreated>> TryCreateAsync(CommunityCreateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/communities", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<CommunityCreated>();
    }

    public async Task<ApiResponse<CommunityInfo>> TryGetAsync(string communityId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/communities/{communityId}", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<CommunityInfo>();
    }

    public async Task<ApiResponse> TryLeaveAsync(string communityId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/communities/{communityId}/leave", null, cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TryUpdateNameAsync(string communityId, CommunityUpdateNameRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/communities/{communityId}/name", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TryUpdateDescriptionAsync(string communityId, CommunityUpdateDescriptionRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/communities/{communityId}/description", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<CommunityPictureUpdated>> TryUpdatePictureAsync(string communityId, CommunityUpdatePictureRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"/communities/{communityId}/picture", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<CommunityPictureUpdated>();
    }

    public async Task<ApiResponse> TrySetLockedAsync(string communityId, CommunityUpdateLockedRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/communities/{communityId}/settings/locked", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<GroupParticipantInfo[]>> TryGetParticipantsAsync(string communityId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/communities/{communityId}/participants", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupParticipantInfo[]>();
    }

    public async Task<ApiResponse> TryUpdateParticipantsAsync(string communityId, CommunityUpdateParticipantsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/communities/{communityId}/participants", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<CommunityInviteLink>> TryGetInviteLinkAsync(string communityId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/communities/{communityId}/invite-link", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<CommunityInviteLink>();
    }

    public async Task<ApiResponse<CommunityInviteLink>> TryResetInviteLinkAsync(string communityId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/communities/{communityId}/invite-link/reset", null, cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<CommunityInviteLink>();
    }

    public async Task<ApiResponse<GroupInfo[]>> TryGetSubGroupsAsync(string communityId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/communities/{communityId}/groups", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupInfo[]>();
    }

    public async Task<ApiResponse<GroupCreated>> TryCreateGroupAsync(string communityId, CommunityCreateGroupRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"/communities/{communityId}/groups", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupCreated>();
    }

    public async Task<ApiResponse> TryLinkGroupAsync(string communityId, CommunityLinkGroupRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"/communities/{communityId}/groups/link", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TryUnlinkGroupAsync(string communityId, string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/communities/{communityId}/groups/{groupId}", cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
}

public interface ICommunitiesClient
{
    Task<CommunityInfo[]> ListAsync(CancellationToken cancellationToken = default);
    Task<CommunityCreated> CreateAsync(CommunityCreateRequest request, CancellationToken cancellationToken = default);
    Task<CommunityInfo> GetAsync(string communityId, CancellationToken cancellationToken = default);
    Task LeaveAsync(string communityId, CancellationToken cancellationToken = default);
    Task UpdateNameAsync(string communityId, CommunityUpdateNameRequest request, CancellationToken cancellationToken = default);
    Task UpdateDescriptionAsync(string communityId, CommunityUpdateDescriptionRequest request, CancellationToken cancellationToken = default);
    Task<CommunityPictureUpdated> UpdatePictureAsync(string communityId, CommunityUpdatePictureRequest request, CancellationToken cancellationToken = default);
    Task SetLockedAsync(string communityId, CommunityUpdateLockedRequest request, CancellationToken cancellationToken = default);
    Task<GroupParticipantInfo[]> GetParticipantsAsync(string communityId, CancellationToken cancellationToken = default);
    Task UpdateParticipantsAsync(string communityId, CommunityUpdateParticipantsRequest request, CancellationToken cancellationToken = default);
    Task<CommunityInviteLink> GetInviteLinkAsync(string communityId, CancellationToken cancellationToken = default);
    Task<CommunityInviteLink> ResetInviteLinkAsync(string communityId, CancellationToken cancellationToken = default);
    Task<GroupInfo[]> GetSubGroupsAsync(string communityId, CancellationToken cancellationToken = default);
    Task<GroupCreated> CreateGroupAsync(string communityId, CommunityCreateGroupRequest request, CancellationToken cancellationToken = default);
    Task LinkGroupAsync(string communityId, CommunityLinkGroupRequest request, CancellationToken cancellationToken = default);
    Task UnlinkGroupAsync(string communityId, string groupId, CancellationToken cancellationToken = default);

    Task<ApiResponse<CommunityInfo[]>> TryListAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<CommunityCreated>> TryCreateAsync(CommunityCreateRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CommunityInfo>> TryGetAsync(string communityId, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryLeaveAsync(string communityId, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateNameAsync(string communityId, CommunityUpdateNameRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateDescriptionAsync(string communityId, CommunityUpdateDescriptionRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CommunityPictureUpdated>> TryUpdatePictureAsync(string communityId, CommunityUpdatePictureRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> TrySetLockedAsync(string communityId, CommunityUpdateLockedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupParticipantInfo[]>> TryGetParticipantsAsync(string communityId, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateParticipantsAsync(string communityId, CommunityUpdateParticipantsRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CommunityInviteLink>> TryGetInviteLinkAsync(string communityId, CancellationToken cancellationToken = default);
    Task<ApiResponse<CommunityInviteLink>> TryResetInviteLinkAsync(string communityId, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupInfo[]>> TryGetSubGroupsAsync(string communityId, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupCreated>> TryCreateGroupAsync(string communityId, CommunityCreateGroupRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryLinkGroupAsync(string communityId, CommunityLinkGroupRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUnlinkGroupAsync(string communityId, string groupId, CancellationToken cancellationToken = default);
}
