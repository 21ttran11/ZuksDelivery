using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    private static Dictionary<string, List<Action<EventData>>> eventListeners = new Dictionary<string, List<Action<EventData>>>();

    public static void Subscribe(string eventName, Action<EventData> listener)
    {
        if (!eventListeners.ContainsKey(eventName))
        {
            eventListeners[eventName] = new List<Action<EventData>>();
        }
        eventListeners[eventName].Add(listener);
    }

    public static void Unsubscribe(string eventName, Action<EventData> listener)
    {
        if (eventListeners.ContainsKey(eventName))
        {
            eventListeners[eventName].Remove(listener);
            if (eventListeners[eventName].Count == 0)
            {
                eventListeners.Remove(eventName);
            }
        }
    }

    public static void Publish(EventData eventData)
    {
        if (eventListeners.ContainsKey(eventData.EventName))
        {
            foreach (var listener in eventListeners[eventData.EventName])
            {
                listener?.Invoke(eventData);
            }
        }
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

public class InteractionEventData : EventData
{
    public bool IsInteracting { get; private set; }
    public GameObject Source { get; private set; }

    public InteractionEventData(bool isInteracting, GameObject source)
        : base("InteractionEvent", source)
    {
        IsInteracting = isInteracting;
        Source = source;
    }
}
