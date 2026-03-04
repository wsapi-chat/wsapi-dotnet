using System;
using System.Collections.Generic;
using System.Text.Json;
using WSApi.Client.Models.Constants;
using WSApi.Client.Models.Events;
using WSApi.Client.Models.Events.Calls;
using WSApi.Client.Models.Events.Chats;
using WSApi.Client.Models.Events.Contacts;
using WSApi.Client.Models.Events.Groups;
using WSApi.Client.Models.Events.Messages;
using WSApi.Client.Models.Events.Newsletters;
using WSApi.Client.Models.Events.Session;
using WSApi.Client.Models.Events.Users;

namespace WSApi.Client
{
    public static class EventFactory
    {
        private static readonly Dictionary<string, Type> _eventTypes = new()
        {
            [EventTypes.LoggedIn] = typeof(SessionLoggedInEvent),
            [EventTypes.LoggedOut] = typeof(SessionLoggedOutEvent),
            [EventTypes.LoginError] = typeof(SessionLoginErrorEvent),
            [EventTypes.InitialSyncFinished] = typeof(SessionInitialSyncFinished),
            [EventTypes.ChatPresence] = typeof(ChatPresenceEvent),
            [EventTypes.ChatSetting] = typeof(ChatSettingEvent),
            [EventTypes.ChatPushName] = typeof(ChatPushNameEvent),
            [EventTypes.ChatPicture] = typeof(ChatPictureEvent),
            [EventTypes.ChatStatus] = typeof(ChatStatusEvent),
            [EventTypes.Message] = typeof(MessageEvent),
            [EventTypes.MessageDelete] = typeof(MessageDeleteEvent),
            [EventTypes.MessageHistorySync] = typeof(MessageHistorySyncEvent),
            [EventTypes.MessageRead] = typeof(MessageReadEvent),
            [EventTypes.MessageStar] = typeof(MessageStarEvent),
            [EventTypes.Contact] = typeof(ContactEvent),
            [EventTypes.Group] = typeof(GroupEvent),
            [EventTypes.UserPresence] = typeof(UserPresenceEvent),
            [EventTypes.CallOffer] = typeof(CallOfferEvent),
            [EventTypes.CallAccept] = typeof(CallAcceptEvent),
            [EventTypes.CallTerminate] = typeof(CallTerminateEvent),
            [EventTypes.Newsletter] = typeof(NewsletterEvent),
        };

        public static BaseEvent ParseEvent(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentException("JSON cannot be null or empty", nameof(json));

            try
            {
                using var document = JsonDocument.Parse(json);
                var rootElement = document.RootElement;

                if (!rootElement.TryGetProperty("eventId", out var jsonEventId))
                    throw new ArgumentException("Missing required property: eventId");

                if (!rootElement.TryGetProperty("receivedAt", out var jsonReceivedAtProp))
                    throw new ArgumentException("Missing required property: receivedAt");
        
                if (!rootElement.TryGetProperty("instanceId", out var jsonInstanceId))
                    throw new ArgumentException("Missing required property: instanceId");
        
                if (!rootElement.TryGetProperty("eventType", out var jsonEventType))
                    throw new ArgumentException("Missing required property: eventType");
        
                if (!rootElement.TryGetProperty("eventData", out var jsonEventData))
                    throw new ArgumentException("Missing required property: eventData");

                var eventId = jsonEventId.GetString() ?? throw new ArgumentException("EventId property is null or empty");
                var receivedAt = jsonReceivedAtProp.GetDateTime();
                var instanceId = jsonInstanceId.GetString() ?? throw new ArgumentException("InstanceId property is null or empty");
                var eventType = jsonEventType.GetString() ?? throw new ArgumentException("EventType property is null or empty");

                if (!_eventTypes.TryGetValue(eventType, out var targetType))
                    throw new ArgumentException($"Unknown event type: {eventType}");

                var evt = jsonEventData.Deserialize(targetType) ?? throw new JsonException($"Failed to deserialize {eventType}");
                var baseEvent = (BaseEvent) evt;
                baseEvent.EventId = eventId;
                baseEvent.InstanceId = instanceId!;
                baseEvent.ReceivedAt = receivedAt;
                baseEvent.EventType = eventType;

                return baseEvent;
            }
            catch (JsonException ex)
            {
                throw new ArgumentException("Invalid JSON format", nameof(json), ex);
            }
        }
    }
}