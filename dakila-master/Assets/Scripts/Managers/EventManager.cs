using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;

public class EventManager : Singleton<EventManager>
{
    protected EventManager() { }

    private Dictionary<string, UnityEvent> _eventDictionary;

    void Awake()
    {
        _eventDictionary = new Dictionary<string, UnityEvent>();
    }

    public void StartListening(string eventName, UnityAction listener)
    {
        if (Instance._eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            thisEvent.AddListener(listener);
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance._eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StopListening(string eventName, UnityAction listener)
    {
        if (Instance == null)
            return;

        if (Instance._eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            thisEvent.RemoveListener(listener);
    }

    public void TriggerEvent(string eventName)
    {
        if (Instance._eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            thisEvent.Invoke();
    }

    #region Game State

    public delegate void UpdateState(GameStates state);
    public static event UpdateState OnUpdateState;

    public void TriggerOnUpdateState(GameStates state)
    {
        OnUpdateState?.Invoke(state);
    }

    public delegate void UpdatePresiousScreenHidden(bool hidden);
    public static event UpdatePresiousScreenHidden OnUpdatePreviousScreenHidden;

    public void TriggerOnUpdatePreviousScreenHidden(bool hidden)
    {
        OnUpdatePreviousScreenHidden?.Invoke(hidden);
    }

    #endregion
}
