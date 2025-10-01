namespace WSApi.Client.Models.Constants;

public class EventTypes
{
    public const string LoggedIn = "logged_in";
    public const string LoggedError = "logged_error";
    public const string LoggedOut = "logged_out";
    
    public const string InitialSyncFinished ="initial_sync_finished";

    public const string ChatPresence = "chat_presence";
    public const string ChatSetting = "chat_setting";
    
    public const string Message = "message";
    public const string MessageDelete = "message_delete";
    public const string MessageHistorySync = "message_history_sync";
    public const string MessageRead = "message_read";
    public const string MessageStar = "message_star";
    
    public const string Contact = "contact";
    
    public const string ChatPushName = "user_push_name";
    public const string ChatPicture = "user_picture";
    public const string UserPresence = "user_presence";
    public const string ChatStatus = "user_status";
    
    public const string CallOffer = "call_offer";
    public const string CallAccept = "call_accept";
    public const string CallTerminate = "call_terminate";
}