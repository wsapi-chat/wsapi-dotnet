using System.Net;
using System.Text;
using System.Text.Json;
using WSApi.Client.ApiClient;
using WSApi.Client.Models.Constants;
using WSApi.Client.Models.Entities.Groups;
using WSApi.Client.Models.Entities.Session;
using WSApi.Client.Models.Entities.Users;
using WSApi.Client.Models.Events.Messages;
using WSApi.Client.Models.Requests.Communities;
using WSApi.Client.Models.Requests.Contacts;
using WSApi.Client.Models.Requests.Groups;
using WSApi.Client.Models.Requests.Messages;
using WSApi.Client.Models.Requests.Session;
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

    private const string PasskeyRequiredJson = """
        {"status":"required","source":"requested","origin":"https://web.whatsapp.com",
         "challenge":{"challenge":"Y2hhbA","timeout":600000,"rpId":"whatsapp.com",
           "allowCredentials":[{"id":"Y3JlZA","type":"public-key","transports":["internal","hybrid"]}],
           "userVerification":"required","extensions":{"appid":"x"}},
         "requestedAt":"2026-11-25T10:00:00Z","expiresAt":"2026-11-25T10:05:00Z"}
        """;

    [Fact]
    public async Task PasskeyEndpointsUseCurrentRoutesInBothVariants()
    {
        using var handler = new CaptureHandler(() => Json(PasskeyRequiredJson));
        using var http = Client(handler);
        var session = new SessionClient(http);
        var assertion = new PasskeyAssertion
        {
            Id = "Y3JlZA",
            RawId = "Y3JlZA",
            Type = "public-key",
            Response = new PasskeyAssertionResponse { ClientDataJson = "Y2Q", AuthenticatorData = "YWQ", Signature = "c2ln" }
        };

        var state = await session.GetPasskeyStateAsync();
        Assert.Equal(PasskeyStatuses.Required, state.Status);
        Assert.Equal(PasskeySources.Requested, state.Source);
        Assert.Equal("whatsapp.com", state.Challenge!.RpId);
        Assert.Equal(600000, state.Challenge.Timeout);
        Assert.Equal(["internal", "hybrid"], state.Challenge.AllowCredentials![0].Transports!);
        Assert.Equal("x", state.Challenge.Extensions!["appid"].GetString());
        Assert.Equal(state.RequestedAt!.Value.AddMinutes(5), state.ExpiresAt);
        Assert.Equal(PasskeyStatuses.Required, (await session.TryGetPasskeyStateAsync()).Result!.Status);
        await session.SubmitPasskeyAsync(assertion);
        Assert.True((await session.TrySubmitPasskeyAsync(assertion)).IsSuccess);
        await session.RequestPasskeyAsync();
        Assert.True((await session.TryRequestPasskeyAsync()).IsSuccess);

        Assert.Equal(
            [
                (HttpMethod.Get, "/session/passkey"), (HttpMethod.Get, "/session/passkey"),
                (HttpMethod.Post, "/session/passkey"), (HttpMethod.Post, "/session/passkey"),
                (HttpMethod.Post, "/session/passkey/request"), (HttpMethod.Post, "/session/passkey/request"),
            ],
            handler.Calls.Select(c => (c.Method, c.Path)));
        var body = JsonDocument.Parse(handler.Calls[2].Body!).RootElement;
        Assert.Equal("Y3JlZA", body.GetProperty("rawId").GetString());
        Assert.Equal("Y2Q", body.GetProperty("response").GetProperty("clientDataJSON").GetString());
        Assert.Equal("c2ln", body.GetProperty("response").GetProperty("signature").GetString());
        Assert.Equal(JsonValueKind.Null, body.GetProperty("response").GetProperty("userHandle").ValueKind);
        Assert.Null(handler.Calls[4].Body);
    }

    [Fact]
    public void PasskeyAssertionRoundTripsBrowserToJson()
    {
        const string browserJson = """
            {"id":"a","rawId":"a","type":"public-key","authenticatorAttachment":"platform",
             "response":{"clientDataJSON":"cd","authenticatorData":"ad","signature":"s","userHandle":"u"},
             "clientExtensionResults":{}}
            """;
        var assertion = JsonSerializer.Deserialize<PasskeyAssertion>(browserJson)!;
        Assert.Equal("u", assertion.Response.UserHandle);
        var minimal = JsonSerializer.SerializeToElement(new PasskeyAssertion
        {
            RawId = "a",
            Response = new PasskeyAssertionResponse { ClientDataJson = "cd", Signature = "s" }
        });
        Assert.False(minimal.TryGetProperty("id", out _));
        Assert.False(minimal.TryGetProperty("type", out _));
        Assert.False(minimal.GetProperty("response").TryGetProperty("authenticatorData", out _));
    }

    [Fact]
    public async Task ConfirmPasskeyAcceptsNoContent()
    {
        using var handler = new CaptureHandler(() => new HttpResponseMessage(HttpStatusCode.NoContent));
        using var http = Client(handler);
        var session = new SessionClient(http);
        await session.ConfirmPasskeyAsync();
        Assert.True((await session.TryConfirmPasskeyAsync()).IsSuccess);
        Assert.All(handler.Calls, c => Assert.Equal((HttpMethod.Post, "/session/passkey/confirm"), (c.Method, c.Path)));
    }

    [Fact]
    public async Task QrAndPairCodeSurfacePasskeyPendingConflict()
    {
        using var handler = new CaptureHandler(() => new HttpResponseMessage(HttpStatusCode.Conflict)
        {
            Content = new StringContent("{\"status\":409,\"detail\":\"passkey verification pending\"}", Encoding.UTF8, "application/json")
        });
        using var http = Client(handler);
        var session = new SessionClient(http);
        Assert.Equal(409, (await session.TryGetLoginQRImageAsync()).Error!.Status);
        Assert.Equal(409, (await session.TryGetLoginQRCodeAsync()).Error!.Status);
        Assert.Equal(409, (await session.TryGetLoginPairCodeAsync("123")).Error!.Status);
        var ex = await Assert.ThrowsAsync<ApiException>(() => session.GetLoginQRCodeAsync());
        Assert.Equal(409, ex.Problem.Status);
    }

    [Fact]
    public void SessionStatusExposesPasskeyStatus()
    {
        var status = JsonSerializer.Deserialize<SessionStatus>("{\"isConnected\":true,\"isLoggedIn\":false,\"passkeyStatus\":\"confirmation_required\"}")!;
        Assert.Equal(PasskeyStatuses.ConfirmationRequired, status.PasskeyStatus);
        Assert.True(PasskeyStatuses.IsPending(status.PasskeyStatus));
        Assert.False(PasskeyStatuses.IsPending(PasskeyStatuses.Error));
        Assert.Null(JsonSerializer.Deserialize<SessionStatus>("{\"isConnected\":true}")!.PasskeyStatus);
    }

    [Fact]
    public async Task CreateGroupWithoutParticipantsSendsOnlyName()
    {
        using var handler = new CaptureHandler(() => Json("{\"id\":\"g\"}"));
        using var http = Client(handler);
        await new GroupsClient(http).CreateAsync(new GroupCreateRequest { Name = "Solo" });
        var body = JsonDocument.Parse(handler.Calls[0].Body!).RootElement;
        Assert.Equal("Solo", body.GetProperty("name").GetString());
        Assert.Equal(0, body.GetProperty("participants").GetArrayLength());
    }

    [Fact]
    public void UserInfoExposesVerifiedName()
    {
        var user = JsonSerializer.Deserialize<UserInfo>("{\"id\":\"u\",\"status\":\"\",\"isVerified\":true,\"verifiedName\":\"Acme\"}")!;
        Assert.True(user.IsVerified);
        Assert.Equal("Acme", user.VerifiedName);
        Assert.Null(JsonSerializer.Deserialize<UserInfo>("{\"id\":\"u\"}")!.VerifiedName);
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
