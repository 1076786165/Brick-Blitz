using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

struct EventHandler
{
    public int Id;
    public Delegate Handler;
}

public class EventManager:MonoBehaviourSingleton<EventManager>
{
    private static Dictionary<string , List<EventHandler>> _event_dictionary = new Dictionary<string, List<EventHandler>>();
    private static int _event_id_maker = 0;

    public static int AddListener<T>(string eventName , Action<T> handler , List<int> evnetIds = null)
    {
        EventHandler event_handler;
        event_handler.Id = _event_id_maker++;
        event_handler.Handler = handler;

        if (!_event_dictionary.ContainsKey(eventName)){
            _event_dictionary[eventName] = new List<EventHandler>();
        }

        _event_dictionary[eventName].Add(event_handler);
        evnetIds?.Add(event_handler.Id);
        return event_handler.Id;
    }

    public static int AddListener(string eventName , Action handler , List<int> evnetIds = null)
    {
        EventHandler event_handler;
        event_handler.Id = _event_id_maker++;
        event_handler.Handler = handler;

        if (!_event_dictionary.ContainsKey(eventName)){
            _event_dictionary[eventName] = new List<EventHandler>();
        }

        _event_dictionary[eventName].Add(event_handler);
        evnetIds?.Add(event_handler.Id);
        return event_handler.Id;
    }

    public static void RemoveListener(int handlerId)
    {
        string event_name = "";
        foreach (var event_handlers in _event_dictionary){
            for (int i = 0; i < event_handlers.Value.Count; i++){
                if (event_handlers.Value[i].Id == handlerId){
                    event_name = event_handlers.Key;
                    event_handlers.Value.RemoveAt(i);
                    break;
                }
            }
        }

        if (event_name != "" && (_event_dictionary[event_name].Count == 0)){
            _event_dictionary.Remove(event_name);
        }
    }

    public static void RemoveListener(string eventName)
    {
        if (_event_dictionary.ContainsKey(eventName)){
            _event_dictionary.Remove(eventName);
        }
    }

    public static void RemoveListeners(List<int> eventNameIds)
    {
        foreach (int ids in eventNameIds){
            RemoveListener(ids);
        }
        eventNameIds.Clear();
    }

    public static void SendEvent<T>(string eventName, T eventData)
    {
        if (_event_dictionary.TryGetValue(eventName, out List<EventHandler> event_handlers))
        {
            for (int i = 0; i < event_handlers.Count; i++){
                if(event_handlers[i].Handler is Action<T>){
                    event_handlers[i].Handler.DynamicInvoke(eventData);
                }
            }
        }

    }

    public static void SendEvent(string eventName)
    {
        if (_event_dictionary.TryGetValue(eventName, out List<EventHandler> event_handlers))
        {
            for (int i = 0; i < event_handlers.Count; i++){
                if(event_handlers[i].Handler is Action){
                    event_handlers[i].Handler.DynamicInvoke();
                }
            }
        }

    }
}
