using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
namespace CodeBase.Events
{

    public class SquareGameEventListener : MonoBehaviour
    {
        [System.Serializable]
        public class SquareEventData
        {
            public SquareEventData() { }
            public List<SquareEvent> triggers = new List<SquareEvent>();
            public UnityEvent responses = new UnityEvent();
            public bool triggersOnce = false;
            public float delay = 0;
            [HideInInspector] public bool alreadyTriggered = false;
            [HideInInspector] public UnityAction<Vector2Int> action;
        }

        //Many-to-Many correspondence without the need of weird inheritance stuff
        public List<SquareEventData> events = new List<SquareEventData>();

        public void RaiseEvent(SquareEvent eventToInvoke, float delay)
        {
            StartCoroutine(WaitAndRaiseEvent(eventToInvoke, delay));
        }

        public bool IsMethodInAnyEvent(UnityAction method)
        {
            bool found = false;

            foreach (SquareEventData eventData in events)
            {
                if (IsMethodInEventData(method, eventData)) found = true;
            }
            return found;
        }
        public bool IsMethodInEventData(UnityAction method, SquareEventData eventData)
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

        public void AddAction(SquareEvent trigger, UnityAction action, bool triggersOnce = false, float delay = 0)
        {
            bool actionFound = false;
            //See if that trigger already calls the action

            foreach (SquareEventData eventData in events)
            {
                //I split the research in two parts to avoid calling unnecessary code
                if (eventData.triggers.Contains(trigger)) continue;
                if (IsMethodInEventData(action, eventData)) continue;
                actionFound = true;
            }

            //if there are no callers, add a new event to the list
            if (!actionFound)
            {
                SquareEventData newEventData = new SquareEventData();
                newEventData.triggers.Add(trigger);
                newEventData.responses.AddListener(action);
                newEventData.action = Vector2Int => Callback(newEventData);
                trigger.RegisterListener(newEventData.action);
            }

        }

        private void OnEnable()
        {
            foreach (SquareEventData eventData in events)
            {
                foreach (SquareEvent trigger in eventData.triggers)
                {
                    if (trigger == null) continue;
                    eventData.action = Vector2Int => Callback(eventData);
                    trigger.RegisterListener(eventData.action);
                }
            }
        }

        private void OnDisable()
        {
            foreach (SquareEventData eventData in events)
            {
                foreach (SquareEvent trigger in eventData.triggers)
                {
                    if (trigger == null || eventData.action == null) continue;
                    trigger.UnregisterListener(eventData.action);
                }
            }
        }

        private void Callback(SquareEventData data)
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

        IEnumerator WaitAndRaiseEvent(SquareEvent eventToRaise, float delay)
        {
            yield return new WaitForSeconds(delay);
            Debug.LogError("Check SquareEventListener WaitAndRaiseEvent");
            //eventToRaise.Raise(Vector2Int);
        }
    }
}
