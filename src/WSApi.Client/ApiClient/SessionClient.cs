using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Session;
using WSApi.Client.Models.Requests.Session;

namespace WSApi.Client.ApiClient;

public class SessionClient(HttpClient httpClient) : ISessionClient
{
    public async Task<byte[]> GetLoginQRImageAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/qr", cancellationToken);
        return await response.EnsureSuccessOrThrowByteArrayAsync();
    }

    public async Task<string> GetLoginQRCodeAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/qr/text", cancellationToken);
        return (await response.EnsureSuccessOrThrowJsonAsync<SessionQrCode>()).Code;
    }

    public async Task<SessionPairCode> GetLoginPairCodeAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/pair-code/{phoneNumber}", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<SessionPairCode>();
    }

    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/session/logout", null, cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task<SessionStatus> GetSessionStatusAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/status", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<SessionStatus>();
    }

    public async Task<FlushHistoryResponse> FlushHistoryAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/session/flush-history", null, cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<FlushHistoryResponse>();
    }

    public async Task<PasskeyState> GetPasskeyStateAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/passkey", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<PasskeyState>();
    }

    public async Task<PasskeyState> SubmitPasskeyAsync(PasskeyAssertion assertion, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/session/passkey", assertion, cancellationToken: cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<PasskeyState>();
    }

    public async Task<PasskeyState> RequestPasskeyAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/session/passkey/request", null, cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<PasskeyState>();
    }

    public async Task ConfirmPasskeyAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/session/passkey/confirm", null, cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task<ApiResponse<byte[]>> TryGetLoginQRImageAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/qr", cancellationToken);
        return await response.ReadAsApiResponseByteArrayAsync();
    }

    public async Task<ApiResponse<string>> TryGetLoginQRCodeAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/qr/text", cancellationToken);
        var result = await response.ReadAsApiResponseJsonAsync<SessionQrCode>();
        return new ApiResponse<string> { Result = result.Result?.Code, Error = result.Error };
    }

    public async Task<ApiResponse<SessionPairCode>> TryGetLoginPairCodeAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/pair-code/{phoneNumber}", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<SessionPairCode>();
    }

    public async Task<ApiResponse> TryLogoutAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/session/logout", null, cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse<SessionStatus>> TryGetSessionStatusAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/status", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<SessionStatus>();
    }

    public async Task<ApiResponse<FlushHistoryResponse>> TryFlushHistoryAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/session/flush-history", null, cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<FlushHistoryResponse>();
    }

    public async Task<ApiResponse<PasskeyState>> TryGetPasskeyStateAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/session/passkey", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<PasskeyState>();
    }

    public async Task<ApiResponse<PasskeyState>> TrySubmitPasskeyAsync(PasskeyAssertion assertion, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/session/passkey", assertion, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<PasskeyState>();
    }

    public async Task<ApiResponse<PasskeyState>> TryRequestPasskeyAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/session/passkey/request", null, cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<PasskeyState>();
    }

    public async Task<ApiResponse> TryConfirmPasskeyAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/session/passkey/confirm", null, cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
}

public interface ISessionClient
{
    /// <remarks>Returns HTTP 409 (<see cref="ApiException"/> / <see cref="ApiResponse{T}.Error"/> with status 409) while a passkey step is pending; stop polling until it completes, fails or expires.</remarks>
    Task<byte[]> GetLoginQRImageAsync(CancellationToken cancellationToken = default);
    /// <remarks>Returns HTTP 409 (<see cref="ApiException"/> / <see cref="ApiResponse{T}.Error"/> with status 409) while a passkey step is pending; stop polling until it completes, fails or expires.</remarks>
    Task<string> GetLoginQRCodeAsync(CancellationToken cancellationToken = default);
    /// <remarks>Returns HTTP 409 (<see cref="ApiException"/> / <see cref="ApiResponse{T}.Error"/> with status 409) while a passkey step is pending; stop polling until it completes, fails or expires.</remarks>
    Task<SessionPairCode> GetLoginPairCodeAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task LogoutAsync(CancellationToken cancellationToken = default);
    Task<SessionStatus> GetSessionStatusAsync(CancellationToken cancellationToken = default);
    Task<FlushHistoryResponse> FlushHistoryAsync(CancellationToken cancellationToken = default);

    /// <summary>Gets the passkey (WebAuthn) verification state (<c>GET /session/passkey</c>).</summary>
    Task<PasskeyState> GetPasskeyStateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Submits the WebAuthn assertion (<c>PublicKeyCredential.toJSON()</c>) signed on <c>https://web.whatsapp.com</c>
    /// (<c>POST /session/passkey</c>). Errors: 400 bad/mismatched assertion, 409 no step pending, 413 body too large, 502 rejected by WhatsApp.
    /// </summary>
    Task<PasskeyState> SubmitPasskeyAsync(PasskeyAssertion assertion, CancellationToken cancellationToken = default);

    /// <summary>
    /// Requests a passkey challenge for an unpaired device (<c>POST /session/passkey/request</c>). Idempotent while a challenge is pending.
    /// Errors: 400 already paired, 409 could not connect, 502 no challenge returned.
    /// </summary>
    Task<PasskeyState> RequestPasskeyAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Optional, compatibility only (<c>POST /session/passkey/confirm</c>): the server now sends the confirmation automatically
    /// when the status becomes <c>confirmation_required</c>, so clients do not need to call this. No-op if already sent.
    /// </summary>
    Task ConfirmPasskeyAsync(CancellationToken cancellationToken = default);

    /// <remarks>Returns HTTP 409 (<see cref="ApiException"/> / <see cref="ApiResponse{T}.Error"/> with status 409) while a passkey step is pending; stop polling until it completes, fails or expires.</remarks>
    Task<ApiResponse<byte[]>> TryGetLoginQRImageAsync(CancellationToken cancellationToken = default);
    /// <remarks>Returns HTTP 409 (<see cref="ApiException"/> / <see cref="ApiResponse{T}.Error"/> with status 409) while a passkey step is pending; stop polling until it completes, fails or expires.</remarks>
    Task<ApiResponse<string>> TryGetLoginQRCodeAsync(CancellationToken cancellationToken = default);
    /// <remarks>Returns HTTP 409 (<see cref="ApiException"/> / <see cref="ApiResponse{T}.Error"/> with status 409) while a passkey step is pending; stop polling until it completes, fails or expires.</remarks>
    Task<ApiResponse<SessionPairCode>> TryGetLoginPairCodeAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryLogoutAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<SessionStatus>> TryGetSessionStatusAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<FlushHistoryResponse>> TryFlushHistoryAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetPasskeyStateAsync"/>
    Task<ApiResponse<PasskeyState>> TryGetPasskeyStateAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc cref="SubmitPasskeyAsync"/>
    Task<ApiResponse<PasskeyState>> TrySubmitPasskeyAsync(PasskeyAssertion assertion, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="RequestPasskeyAsync"/>
    Task<ApiResponse<PasskeyState>> TryRequestPasskeyAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc cref="ConfirmPasskeyAsync"/>
    Task<ApiResponse> TryConfirmPasskeyAsync(CancellationToken cancellationToken = default);
}
