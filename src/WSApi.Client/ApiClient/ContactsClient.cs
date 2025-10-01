using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WSApi.Client.Models.Entities.Contacts;
using WSApi.Client.Models.Requests.Contacts;

namespace WSApi.Client.ApiClient;

public class ContactsClient(HttpClient httpClient) : IContactsClient
{
    // These methods return the actual data or throw an exception if the request fails.

    public async Task<ContactInfo[]> ListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/contacts", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<ContactInfo[]>();
    }

    public async Task<ContactInfo> GetAsync(string contactId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/contacts/{contactId}", cancellationToken);
        return await response.EnsureSuccessOrThrowJsonAsync<ContactInfo>();
    }
    public async Task CreateAsync(ContactCreateRequest contactCreateRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"/contacts", contactCreateRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }

    public async Task UpdateAsync(string contactId, ContactUpdateRequest updateRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/contacts/{contactId}", updateRequest, cancellationToken: cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }
    
    public async Task SynchronizeAllAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync($"/contacts/sync", null, cancellationToken);
        await response.EnsureSuccessOrThrowAsync();
    }



    // Try methods for error handling. These methods return an ApiResponse object that contains the status and data.
    public async Task<ApiResponse<ContactInfo[]>> TryListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/contacts", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<ContactInfo[]>();
    }

    public async Task<ApiResponse<ContactInfo>> TryGetAsync(string contactId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/contacts/{contactId}", cancellationToken);
        return await response.ReadAsApiResponseJsonAsync<ContactInfo>();
    }

    public async Task<ApiResponse> TryCreateAsync(ContactCreateRequest contactCreateRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"/contacts", contactCreateRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

    public async Task<ApiResponse> TryUpdateAsync(string contactId, ContactUpdateRequest updateRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/contacts/{contactId}", updateRequest, cancellationToken: cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }
    
    public async Task<ApiResponse> TrySynchronizeAllAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsync($"/contacts/sync", null, cancellationToken);
        return await response.ReadAsApiResponseAsync();
    }

}

public interface IContactsClient
{
    Task<ContactInfo[]> ListAsync(CancellationToken cancellationToken = default);
    Task<ContactInfo> GetAsync(string contactId, CancellationToken cancellationToken = default);
    Task CreateAsync(ContactCreateRequest contactCreateRequest, CancellationToken cancellationToken = default);
    Task UpdateAsync(string contactId, ContactUpdateRequest updateRequest, CancellationToken cancellationToken = default);
    Task SynchronizeAllAsync(CancellationToken cancellationToken = default);

    Task<ApiResponse<ContactInfo[]>> TryListAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<ContactInfo>> TryGetAsync(string contactId, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryCreateAsync(ContactCreateRequest contactCreateRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TryUpdateAsync(string contactId, ContactUpdateRequest updateRequest, CancellationToken cancellationToken = default);
    Task<ApiResponse> TrySynchronizeAllAsync(CancellationToken cancellationToken = default);
}