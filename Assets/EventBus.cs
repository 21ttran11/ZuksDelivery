using System;
using UnityEngine;

public static class EventBus 
{
    public static Action<EventData> OnEventTriggered;

    public static void Publish(EventData eventData)
    {
        OnEventTriggered?.Invoke(eventData);
    }
}

public class EventData
{
    public String EventName;
    public object Data;
    public EventData(String eventName, object data)
    {
        EventName = eventName;
        Data = data;
    }
}