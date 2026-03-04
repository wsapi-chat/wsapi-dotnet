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
        var response = await httpClient.PostAsync($"/groups/{groupId}/leave", null, cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task<GroupInviteLink> GetInviteLinkAsync(string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/groups/{groupId}/invite-link", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupInviteLink>();
    }

    public async Task<GroupInviteLink> ResetInviteLinkAsync(string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/groups/{groupId}/invite-link/reset", null, cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupInviteLink>();
    }

    public async Task<GroupParticipantInfo[]> GetParticipantsAsync(string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/groups/{groupId}/participants", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupParticipantInfo[]>();
    }

    public async Task SetAnnounceAsync(string groupId, GroupAnnounceRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/settings/announce", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task SetLockedAsync(string groupId, GroupLockedRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/settings/locked", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task SetJoinApprovalAsync(string groupId, GroupJoinApprovalRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/settings/join-approval", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task SetMemberAddModeAsync(string groupId, GroupMemberAddModeRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/settings/member-add-mode", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task<GroupJoined> JoinWithLinkAsync(GroupJoinWithLinkRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/groups/join/link", request, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupJoined>();
    }

    public async Task<GroupJoined> JoinWithInviteAsync(GroupJoinWithInviteRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/groups/join/invite", request, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupJoined>();
    }

    public async Task<GroupJoinRequest[]> GetJoinRequestsAsync(string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/groups/{groupId}/requests", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupJoinRequest[]>();
    }

    public async Task UpdateJoinRequestsAsync(string groupId, GroupUpdateRequestParticipantsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/requests", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task UpdateParticipantsAsync(string groupId, GroupUpdateRequestParticipantsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/participants", request, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task<GroupInfo> GetInfoFromInviteAsync(string inviteCode, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/groups/invite/{inviteCode}", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<GroupInfo>();
    }

    // Try methods

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
        var response = await httpClient.PostAsync($"/groups/{groupId}/leave", null, cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<GroupInviteLink>> TryGetInviteLinkAsync(string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/groups/{groupId}/invite-link", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupInviteLink>();
    }

    public async Task<ApiResponse<GroupInviteLink>> TryResetInviteLinkAsync(string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/groups/{groupId}/invite-link/reset", null, cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupInviteLink>();
    }

    public async Task<ApiResponse<GroupParticipantInfo[]>> TryGetParticipantsAsync(string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/groups/{groupId}/participants", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupParticipantInfo[]>();
    }

    public async Task<ApiResponse> TrySetAnnounceAsync(string groupId, GroupAnnounceRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/settings/announce", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TrySetLockedAsync(string groupId, GroupLockedRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/settings/locked", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TrySetJoinApprovalAsync(string groupId, GroupJoinApprovalRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/settings/join-approval", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TrySetMemberAddModeAsync(string groupId, GroupMemberAddModeRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/settings/member-add-mode", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<GroupJoined>> TryJoinWithLinkAsync(GroupJoinWithLinkRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/groups/join/link", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupJoined>();
    }

    public async Task<ApiResponse<GroupJoined>> TryJoinWithInviteAsync(GroupJoinWithInviteRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/groups/join/invite", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupJoined>();
    }

    public async Task<ApiResponse<GroupJoinRequest[]>> TryGetJoinRequestsAsync(string groupId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/groups/{groupId}/requests", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupJoinRequest[]>();
    }

    public async Task<ApiResponse> TryUpdateJoinRequestsAsync(string groupId, GroupUpdateRequestParticipantsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/requests", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TryUpdateParticipantsAsync(string groupId, GroupUpdateRequestParticipantsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/groups/{groupId}/participants", request, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<GroupInfo>> TryGetInfoFromInviteAsync(string inviteCode, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/groups/invite/{inviteCode}", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<GroupInfo>();
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
    Task<GroupInviteLink> GetInviteLinkAsync(string groupId, CancellationToken cancellationToken = default);
    Task<GroupInviteLink> ResetInviteLinkAsync(string groupId, CancellationToken cancellationToken = default);
    Task<GroupParticipantInfo[]> GetParticipantsAsync(string groupId, CancellationToken cancellationToken = default);
    Task SetAnnounceAsync(string groupId, GroupAnnounceRequest request, CancellationToken cancellationToken = default);
    Task SetLockedAsync(string groupId, GroupLockedRequest request, CancellationToken cancellationToken = default);
    Task SetJoinApprovalAsync(string groupId, GroupJoinApprovalRequest request, CancellationToken cancellationToken = default);
    Task SetMemberAddModeAsync(string groupId, GroupMemberAddModeRequest request, CancellationToken cancellationToken = default);
    Task<GroupJoined> JoinWithLinkAsync(GroupJoinWithLinkRequest request, CancellationToken cancellationToken = default);
    Task<GroupJoined> JoinWithInviteAsync(GroupJoinWithInviteRequest request, CancellationToken cancellationToken = default);
    Task<GroupJoinRequest[]> GetJoinRequestsAsync(string groupId, CancellationToken cancellationToken = default);
    Task UpdateJoinRequestsAsync(string groupId, GroupUpdateRequestParticipantsRequest request, CancellationToken cancellationToken = default);
    Task UpdateParticipantsAsync(string groupId, GroupUpdateRequestParticipantsRequest request, CancellationToken cancellationToken = default);
    Task<GroupInfo> GetInfoFromInviteAsync(string inviteCode, CancellationToken cancellationToken = default);

    Task<ApiResponse<GroupInfo[]>> TryListAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupInfo>> TryGetAsync(string groupId, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupCreated>> TryCreateAsync(GroupCreateRequest groupCreateRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateDescriptionAsync(string groupId, GroupUpdateDescriptionRequest groupUpdateDescriptionRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateNameAsync(string groupId, GroupUpdateNameRequest groupUpdateNameRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupPictureUpdated>> TryUpdatePictureAsync(string groupId, GroupUpdatePictureRequest groupUpdatePictureRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryLeaveGroupAsync(string groupId, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupInviteLink>> TryGetInviteLinkAsync(string groupId, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupInviteLink>> TryResetInviteLinkAsync(string groupId, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupParticipantInfo[]>> TryGetParticipantsAsync(string groupId, CancellationToken cancellationToken = default);
    Task<ApiResponse> TrySetAnnounceAsync(string groupId, GroupAnnounceRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> TrySetLockedAsync(string groupId, GroupLockedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> TrySetJoinApprovalAsync(string groupId, GroupJoinApprovalRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> TrySetMemberAddModeAsync(string groupId, GroupMemberAddModeRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupJoined>> TryJoinWithLinkAsync(GroupJoinWithLinkRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupJoined>> TryJoinWithInviteAsync(GroupJoinWithInviteRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupJoinRequest[]>> TryGetJoinRequestsAsync(string groupId, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateJoinRequestsAsync(string groupId, GroupUpdateRequestParticipantsRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateParticipantsAsync(string groupId, GroupUpdateRequestParticipantsRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<GroupInfo>> TryGetInfoFromInviteAsync(string inviteCode, CancellationToken cancellationToken = default);
}
