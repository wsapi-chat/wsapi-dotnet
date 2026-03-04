using System;

namespace WSApi.Client.Models.Events;

public abstract record BaseEvent
{
    public string EventId { get; set; } = null!;
    public string InstanceId { get; set; } = default!;
    public DateTime ReceivedAt { get; set; }
    public string EventType { get; set; } = null!;
}

