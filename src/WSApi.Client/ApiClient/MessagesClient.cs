using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Messages;
using WSApi.Client.Models.Requests.Messages;

namespace WSApi.Client.ApiClient;

public class MessagesClient(HttpClient httpClient) : IMessagesClient
{
    public async Task<MessageCreated> SendTextAsync(MessageSendTextRequest messageSendTextRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/text", messageSendTextRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }
    public async Task<MessageCreated> SendImageAsync(MessageSendImageRequest messageSendImageRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/image", messageSendImageRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }
    public async Task<MessageCreated> SendVideoAsync(MessageSendVideoRequest messageSendVideoRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/video", messageSendVideoRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }
    public async Task<MessageCreated> SendAudioAsync(MessageSendAudioRequest messageSendAudioRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/audio", messageSendAudioRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }
    public async Task<MessageCreated> SendVoiceAsync(MessageSendVoiceRequest messageSendVoiceRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/voice", messageSendVoiceRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }
    public async Task<MessageCreated> SendStickerAsync(MessageSendStickerRequest messageSendStickerRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/sticker", messageSendStickerRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }
    public async Task<MessageCreated> SendDocumentAsync(MessageSendDocumentRequest messageSendDocumentRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/document", messageSendDocumentRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }
    public async Task<MessageCreated> SendContactAsync(MessageSendContactRequest messageSendContactRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/contact", messageSendContactRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }
    public async Task<MessageCreated> SendLocationAsync(MessageSendLocationRequest messageSendLocationRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/location", messageSendLocationRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }
    public async Task<MessageCreated> SendLinkAsync(MessageSendLinkRequest messageSendLinkRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/link", messageSendLinkRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }
    public async Task<MessageCreated> SendReactionAsync(string MessageId, MessageSendReactionRequest messageSendReactionRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{MessageId}/reaction", messageSendReactionRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }
    public async Task<MessageCreated> SendEditTextAsync(string MessageId, MessageSendTextRequest messageSendTextRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{MessageId}/edit", messageSendTextRequest, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<MessageCreated>();
    }

    public async Task MarkAsReadAsync(string MessageId, MessageMarkAsReadRequest messageMarkAsReadRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{MessageId}/read", messageMarkAsReadRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    public async Task StarAsync(string MessageId, MessageStarRequest messageStarRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{MessageId}/star", messageStarRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    public async Task DeleteAsync(string MessageId, MessageDeleteRequest messageDeleteRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{MessageId}/delete", messageDeleteRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task DeleteForMeAsync(string MessageId, MessageDeleteForMeRequest messageDeleteForMeRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{MessageId}/delete-for-me", messageDeleteForMeRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task PinAsync(string MessageId, MessagePinRequest messagePinRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{MessageId}/pin", messagePinRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task<ApiResponse<MessageCreated>> TrySendTextAsync(MessageSendTextRequest messageSendTextRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/text", messageSendTextRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }
    public async Task<ApiResponse<MessageCreated>> TrySendImageAsync(MessageSendImageRequest messageSendImageRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/image", messageSendImageRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }
    public async Task<ApiResponse<MessageCreated>> TrySendVideoAsync(MessageSendVideoRequest messageSendVideoRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/video", messageSendVideoRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }
    public async Task<ApiResponse<MessageCreated>> TrySendAudioAsync(MessageSendAudioRequest messageSendAudioRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/audio", messageSendAudioRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }
    public async Task<ApiResponse<MessageCreated>> TrySendVoiceAsync(MessageSendVoiceRequest messageSendVoiceRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/voice", messageSendVoiceRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }
    public async Task<ApiResponse<MessageCreated>> TrySendStickerAsync(MessageSendStickerRequest messageSendStickerRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/sticker", messageSendStickerRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }
    public async Task<ApiResponse<MessageCreated>> TrySendDocumentAsync(MessageSendDocumentRequest messageSendDocumentRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/document", messageSendDocumentRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }
    public async Task<ApiResponse<MessageCreated>> TrySendContactAsync(MessageSendContactRequest messageSendContactRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/contact", messageSendContactRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }
    public async Task<ApiResponse<MessageCreated>> TrySendLocationAsync(MessageSendLocationRequest messageSendLocationRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/location", messageSendLocationRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }
    public async Task<ApiResponse<MessageCreated>> TrySendLinkAsync(MessageSendLinkRequest messageSendLinkRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("messages/link", messageSendLinkRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }
    public async Task<ApiResponse<MessageCreated>> TrySendReactionAsync(string MessageId, MessageSendReactionRequest messageSendReactionRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{MessageId}/reaction", messageSendReactionRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }
    public async Task<ApiResponse<MessageCreated>> TrySendEditTextAsync(string messageId, MessageSendTextRequest messageSendTextRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{messageId}/edit", messageSendTextRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<MessageCreated>();
    }
    public async Task<ApiResponse> TryMarkAsReadAsync(string MessageId, MessageMarkAsReadRequest messageMarkAsReadRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{MessageId}/read", messageMarkAsReadRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    public async Task<ApiResponse> TryStarAsync(string MessageId, MessageStarRequest messageStarRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{MessageId}/star", messageStarRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    public async Task<ApiResponse> TryDeleteAsync(string MessageId, MessageDeleteRequest messageDeleteRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{MessageId}/delete", messageDeleteRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    public async Task<ApiResponse> TryDeleteForMeAsync(string MessageId, MessageDeleteForMeRequest messageDeleteForMeRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{MessageId}/delete-for-me", messageDeleteForMeRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    public async Task<ApiResponse> TryPinAsync(string MessageId, MessagePinRequest messagePinRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"messages/{MessageId}/pin", messagePinRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
}

public interface IMessagesClient
{
    public Task<MessageCreated> SendTextAsync(MessageSendTextRequest messageSendTextRequest, CancellationToken cancellationToken = default);
    public Task<MessageCreated> SendLinkAsync(MessageSendLinkRequest messageSendLinkRequest, CancellationToken cancellationToken = default);
    public Task<MessageCreated> SendImageAsync(MessageSendImageRequest messageSendImageRequest, CancellationToken cancellationToken = default);
    public Task<MessageCreated> SendVideoAsync(MessageSendVideoRequest messageSendVideoRequest, CancellationToken cancellationToken = default);
    public Task<MessageCreated> SendAudioAsync(MessageSendAudioRequest messageSendAudioRequest, CancellationToken cancellationToken = default);
    public Task<MessageCreated> SendVoiceAsync(MessageSendVoiceRequest messageSendVoiceRequest, CancellationToken cancellationToken = default);
    public Task<MessageCreated> SendStickerAsync(MessageSendStickerRequest messageSendStickerRequest, CancellationToken cancellationToken = default);
    public Task<MessageCreated> SendDocumentAsync(MessageSendDocumentRequest messageSendDocumentRequest, CancellationToken cancellationToken = default);
    public Task<MessageCreated> SendContactAsync(MessageSendContactRequest messageSendContactRequest, CancellationToken cancellationToken = default);
    public Task<MessageCreated> SendLocationAsync(MessageSendLocationRequest messageSendLocationRequest, CancellationToken cancellationToken = default);
    public Task<MessageCreated> SendReactionAsync(string MessageId, MessageSendReactionRequest messageSendReactionRequest, CancellationToken cancellationToken = default);
    public Task<MessageCreated> SendEditTextAsync(string messageId, MessageSendTextRequest messageSendTextRequest, CancellationToken cancellationToken = default);
    public Task MarkAsReadAsync(string MessageId, MessageMarkAsReadRequest messageMarkAsReadRequest, CancellationToken cancellationToken = default);
    public Task StarAsync(string MessageId, MessageStarRequest messageStarRequest, CancellationToken cancellationToken = default);
    public Task DeleteAsync(string MessageId, MessageDeleteRequest messageDeleteRequest, CancellationToken cancellationToken = default);
    public Task DeleteForMeAsync(string MessageId, MessageDeleteForMeRequest messageDeleteForMeRequest, CancellationToken cancellationToken = default);
    public Task PinAsync(string MessageId, MessagePinRequest messagePinRequest, CancellationToken cancellationToken = default);

    public Task<ApiResponse<MessageCreated>> TrySendTextAsync(MessageSendTextRequest messageSendTextRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse<MessageCreated>> TrySendLinkAsync(MessageSendLinkRequest messageSendLinkRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse<MessageCreated>> TrySendImageAsync(MessageSendImageRequest messageSendImageRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse<MessageCreated>> TrySendVideoAsync(MessageSendVideoRequest messageSendVideoRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse<MessageCreated>> TrySendAudioAsync(MessageSendAudioRequest messageSendAudioRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse<MessageCreated>> TrySendVoiceAsync(MessageSendVoiceRequest messageSendVoiceRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse<MessageCreated>> TrySendStickerAsync(MessageSendStickerRequest messageSendStickerRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse<MessageCreated>> TrySendDocumentAsync(MessageSendDocumentRequest messageSendDocumentRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse<MessageCreated>> TrySendContactAsync(MessageSendContactRequest messageSendContactRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse<MessageCreated>> TrySendLocationAsync(MessageSendLocationRequest messageSendLocationRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse<MessageCreated>> TrySendReactionAsync(string MessageId, MessageSendReactionRequest messageSendReactionRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse<MessageCreated>> TrySendEditTextAsync(string messageId, MessageSendTextRequest messageSendTextRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse> TryMarkAsReadAsync(string MessageId, MessageMarkAsReadRequest messageMarkAsReadRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse> TryStarAsync(string MessageId, MessageStarRequest messageStarRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse> TryDeleteAsync(string MessageId, MessageDeleteRequest messageDeleteRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse> TryDeleteForMeAsync(string MessageId, MessageDeleteForMeRequest messageDeleteForMeRequest, CancellationToken cancellationToken = default);
    public Task<ApiResponse> TryPinAsync(string MessageId, MessagePinRequest messagePinRequest, CancellationToken cancellationToken = default);
}
