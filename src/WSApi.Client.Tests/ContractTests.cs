using System.Net;
using System.Text;
using System.Text.Json;
using WSApi.Client.ApiClient;
using WSApi.Client.Models.Entities.Groups;
using WSApi.Client.Models.Events.Messages;
using WSApi.Client.Models.Requests.Communities;
using WSApi.Client.Models.Requests.Contacts;
using WSApi.Client.Models.Requests.Groups;
using WSApi.Client.Models.Requests.Messages;
using Xunit;

namespace WSApi.Client.Tests;

public class ContractTests
{
    [Fact]
    public void UnsupportedChatSynchronizationIsNotExposed()
    {
        Assert.Null(typeof(ChatsClient).GetMethod("SynchronizeAllAsync"));
        Assert.Null(typeof(ChatsClient).GetMethod("TrySynchronizeAllAsync"));
        Assert.Null(typeof(IChatsClient).GetMethod("SynchronizeAllAsync"));
        Assert.Null(typeof(IChatsClient).GetMethod("TrySynchronizeAllAsync"));
    }

    [Fact]
    public void OptionalMediaFieldsSerializeToTheCurrentContract()
    {
        var audio = JsonSerializer.SerializeToElement(new MessageSendAudioRequest { To = "u", Caption = "caption" });
        Assert.Equal("caption", audio.GetProperty("caption").GetString());
        var voice = JsonSerializer.SerializeToElement(new MessageSendVoiceRequest { To = "u", Caption = "caption", MimeType = "audio/ogg" });
        Assert.Equal("caption", voice.GetProperty("caption").GetString());
        Assert.Equal("audio/ogg", voice.GetProperty("mimeType").GetString());
        var document = JsonSerializer.SerializeToElement(new MessageSendDocumentRequest { To = "u", MimeType = "application/pdf", ViewOnce = false });
        Assert.Equal("application/pdf", document.GetProperty("mimeType").GetString());
        Assert.False(document.GetProperty("viewOnce").GetBoolean());
    }

    private sealed class CaptureHandler(Func<HttpResponseMessage> response) : HttpMessageHandler
    {
        public List<(HttpMethod Method, string Path, string? Body)> Calls { get; } = [];

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            Calls.Add((request.Method, request.RequestUri!.PathAndQuery,
                request.Content is null ? null : await request.Content.ReadAsStringAsync(token)));
            return response();
        }
    }

    private static HttpResponseMessage Json(string json) => new(HttpStatusCode.OK)
    {
        Content = new StringContent(json, Encoding.UTF8, "application/json")
    };

    private static HttpClient Client(CaptureHandler handler) => new(handler) { BaseAddress = new Uri("https://contract.invalid") };

    [Fact]
    public async Task QrImageReadsBinaryInBothVariants()
    {
        byte[] png = [137, 80, 78, 71, 13, 10, 26, 10, 255, 0];
        using var handler = new CaptureHandler(() => new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(png) });
        using var http = Client(handler);
        var session = new SessionClient(http);
        Assert.Equal(png, await session.GetLoginQRImageAsync());
        Assert.Equal(png, (await session.TryGetLoginQRImageAsync()).Result);
        Assert.All(handler.Calls, c => Assert.Equal("/session/qr", c.Path));
    }

    [Fact]
    public async Task QrTextUnwrapsCodeWithoutChangingPublicStringReturnType()
    {
        using var handler = new CaptureHandler(() => Json("{\"code\":\"qr-value\"}"));
        using var http = Client(handler);
        var session = new SessionClient(http);
        Assert.Equal("qr-value", await session.GetLoginQRCodeAsync());
        Assert.Equal("qr-value", (await session.TryGetLoginQRCodeAsync()).Result);
    }

    [Fact]
    public async Task TryQrPreservesApiErrors()
    {
        using var handler = new CaptureHandler(() => new HttpResponseMessage(HttpStatusCode.Unauthorized)
        { Content = new StringContent("{\"status\":401,\"detail\":\"unauthorized\"}", Encoding.UTF8, "application/json") });
        using var http = Client(handler);
        var result = await new SessionClient(http).TryGetLoginQRCodeAsync();
        Assert.False(result.IsSuccess);
        Assert.Equal(401, result.Error!.Status);
    }

    [Fact]
    public async Task GroupSettingsSendCanonicalKeysAndFalseValues()
    {
        using var handler = new CaptureHandler(() => new HttpResponseMessage(HttpStatusCode.NoContent));
        using var http = Client(handler);
        var groups = new GroupsClient(http);
        await groups.SetAnnounceAsync("g", new GroupAnnounceRequest { Announce = true });
        await groups.TrySetLockedAsync("g", new GroupLockedRequest { Locked = false });
        await groups.SetJoinApprovalAsync("g", new GroupJoinApprovalRequest { JoinApproval = true });
        await groups.SetMemberAddModeAsync("g", new GroupMemberAddModeRequest { OnlyAdmins = true });
        Assert.Equal("{\"enabled\":true}", handler.Calls[0].Body);
        Assert.Equal("{\"enabled\":false}", handler.Calls[1].Body);
        Assert.Equal("{\"enabled\":true}", handler.Calls[2].Body);
        Assert.Equal("{\"onlyAdminAdd\":true}", handler.Calls[3].Body);
        Assert.Equal("{\"enabled\":true}", JsonSerializer.Serialize(new CommunityUpdateLockedRequest { Locked = true }));
        Assert.Equal("{\"data\":\"base64\"}", JsonSerializer.Serialize(new GroupUpdatePictureRequest { PictureBase64 = "base64" }));
    }

    [Fact]
    public async Task GroupInvitesUseCurrentResponseShapes()
    {
        Assert.Equal("url", JsonSerializer.Deserialize<GroupInviteLink>("{\"link\":\"url\"}")!.InviteLink);
        Assert.Equal("g", JsonSerializer.Deserialize<GroupJoined>("{\"id\":\"g\"}")!.GroupId);
        using var handler = new CaptureHandler(() => new HttpResponseMessage(HttpStatusCode.NoContent));
        using var http = Client(handler);
        var groups = new GroupsClient(http);
        var request = new GroupJoinWithInviteRequest { GroupId = "g", InviterId = "u", Code = "code" };
        Assert.Equal("g", (await groups.JoinWithInviteAsync(request)).GroupId);
        var result = await groups.TryJoinWithInviteAsync(request);
        Assert.True(result.IsSuccess);
        Assert.Equal("g", result.Result!.GroupId);
    }

    [Fact]
    public async Task UpdateContactUsesUpsertAndIncludesId()
    {
        using var handler = new CaptureHandler(() => new HttpResponseMessage(HttpStatusCode.NoContent));
        using var http = Client(handler);
        var contacts = new ContactsClient(http);
        var request = new ContactUpdateRequest { FullName = "Alice", FirstName = "A" };
        await contacts.UpdateAsync("u", request);
        await contacts.TryUpdateAsync("u", request);
        Assert.All(handler.Calls, c =>
        {
            Assert.Equal(HttpMethod.Post, c.Method);
            Assert.Equal("/contacts", c.Path);
            Assert.Equal("u", JsonDocument.Parse(c.Body!).RootElement.GetProperty("id").GetString());
        });
    }

    [Fact]
    public async Task OptionalInstanceNameIsEncodedInBothVariants()
    {
        using var handler = new CaptureHandler(() => Json("\"instance\""));
        using var http = Client(handler);
        var account = new AccountClient(http);
        await account.CreateSubscriptionInstance("sub", name: "A + B& C");
        await account.TryCreateSubscriptionInstance("sub", name: "A + B& C");
        Assert.All(handler.Calls, c => Assert.EndsWith("?name=A%20%2B%20B%26%20C", c.Path));
    }

    [Theory]
    [InlineData("{\"ctwaClid\":\"click\",\"showAdAttribution\":false}")]
    [InlineData("null")]
    public void AttributionIsOptionalAndSurvivesHistory(string referral)
    {
        var message = "{\"id\":\"m\",\"adReferral\":" + referral + ",\"media\":{\"filename\":\"invoice.pdf\"}}";
        var parsed = JsonSerializer.Deserialize<MessageEvent>(message)!;
        Assert.Equal(referral == "null" ? null : "click", parsed.AdReferral?.CtwaClid);
        Assert.Equal("invoice.pdf", parsed.Media!.Filename);
        var history = JsonSerializer.Deserialize<MessageHistorySyncEvent>("{\"messages\":[" + message + "]}")!;
        Assert.Equal(parsed.AdReferral, history.Messages[0].AdReferral);
    }
}
