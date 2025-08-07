using System.Net.Http;

namespace WSApi.Client.ApiClient;

public class WSApiClient(HttpClient httpClient) : IWSApiClient
{
    public IInstanceClient Instance { get; } = new InstanceClient(httpClient);
    public IAccountClient Account { get; } = new AccountClient(httpClient);
    public IChatsClient Chats { get; } = new ChatsClient(httpClient);
    public IContactsClient Contacts { get; } = new ContactsClient(httpClient);
    public IGroupsClient Groups { get; } = new GroupsClient(httpClient);
    public IMessagesClient Messages { get; } = new MessagesClient(httpClient);
    public IMediaClient Media { get; } = new MediaClient(httpClient);
    public IUsersClient Users { get; } = new UsersClient(httpClient);
    public ISessionClient Session { get; } = new SessionClient(httpClient);
    public ICallsClient Calls { get; } = new CallsClient(httpClient);
}

public interface IWSApiClient
{
    IInstanceClient Instance { get; }
    IAccountClient Account { get; }
    IChatsClient Chats { get; }
    IContactsClient Contacts { get; }
    IGroupsClient Groups { get; }
    IMediaClient Media { get; }
    IMessagesClient Messages { get; }
    IUsersClient Users { get; }
    ISessionClient Session { get; }
    ICallsClient Calls { get; }
}