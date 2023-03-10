using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;


public class GameEventListener : MonoBehaviour
{
    [System.Serializable]
    public class EventData
    {
        public EventData() { }
        public List<GameEvent> triggers = new List<GameEvent>();
        public UnityEvent responses = new UnityEvent();
        public bool triggersOnce = false;
        public float delay = 0;
        [HideInInspector] public bool alreadyTriggered = false;
        [HideInInspector] public UnityAction action;
    }

    //Many-to-Many correspondence without the need of weird inheritance stuff
    public List<EventData> events = new List<EventData>();

    public void RaiseEvent(GameEvent eventToInvoke, float delay)
    {
        StartCoroutine(WaitAndRaiseEvent(eventToInvoke, delay));
    }

    public bool IsMethodInAnyEvent(UnityAction method)
    {
        bool found = false;

        foreach (EventData eventData in events)
        {
            if (IsMethodInEventData(method, eventData)) found = true;
        }
        return found;
    }
    public bool IsMethodInEventData(UnityAction method, EventData eventData)
    {
        bool found = false;
        string methodName = method.Method.Name;
        for (int i = 0; i < eventData.responses.GetPersistentEventCount(); i++)
        {
            if (eventData.responses.GetPersistentMethodName(i) == methodName)
                found = true;
        }
        return found;
    }

    public void AddAction(GameEvent trigger, UnityAction action, bool triggersOnce = false, float delay = 0)
    {
        bool actionFound = false;
        //See if that trigger already calls the action

        foreach (EventData eventData in events)
        {
            //I split the research in two parts to avoid calling unnecessary code
            if (eventData.triggers.Contains(trigger)) continue;
            if (IsMethodInEventData(action, eventData)) continue;
            actionFound = true;
        }

        //if there are no callers, add a new event to the list
        if (!actionFound)
        {
            EventData newEventData = new EventData();
            newEventData.triggers.Add(trigger);
            newEventData.responses.AddListener(action);
            newEventData.action = () => Callback(newEventData);
            trigger.RegisterListener(newEventData.action);
        }

    }

    private void OnEnable()
    {
        foreach (EventData eventData in events)
        {
            foreach (GameEvent trigger in eventData.triggers)
            {
                if (trigger == null) continue;
                eventData.action = () => Callback(eventData);
                trigger.RegisterListener(eventData.action);
            }
        }
    }

    private void OnDisable()
    {
        foreach (EventData eventData in events)
        {
            foreach (GameEvent trigger in eventData.triggers)
            {
                if (trigger == null || eventData.action == null) continue;
                trigger.UnregisterListener(eventData.action);
            }
        }
    }

    private void Callback(EventData data)
    {
        if (!data.triggersOnce || !data.alreadyTriggered)
        {
            data.alreadyTriggered = true;
            StartCoroutine(WaitAndInvokeEvent(data.responses, data.delay));
        }
    }

    IEnumerator WaitAndInvokeEvent(UnityEvent eventToInvoke, float delay)
    {
        yield return new WaitForSeconds(delay);
        eventToInvoke.Invoke();
    }

    IEnumerator WaitAndRaiseEvent(GameEvent eventToRaise, float delay)
    {
        yield return new WaitForSeconds(delay);
        eventToRaise.Raise();
    }
}

