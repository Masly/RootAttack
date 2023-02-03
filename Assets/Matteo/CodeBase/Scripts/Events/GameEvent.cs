using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//loosely based on:  https://www.youtube.com/watch?v=raQ3iHhE_Kk&t=2720s
[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<UnityAction> listeners = new List<UnityAction>();
    [HideInInspector] public bool alreadyRaised = false;

    public void Raise()
    {
        Debug.LogWarning($"Raised event {this.name}");
        alreadyRaised = true;
        //It iterates backwards so if a listener gets removed during the loop it doesn't interfere with the loop cycle
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i]();
        }

    }

    public void RegisterListener(UnityAction listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(UnityAction listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }


}
