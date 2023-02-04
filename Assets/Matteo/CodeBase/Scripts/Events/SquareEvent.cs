using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//loosely based on:  https://www.youtube.com/watch?v=raQ3iHhE_Kk&t=2720s
[CreateAssetMenu(menuName = "Square Event")]
public class SquareEvent : ScriptableObject
{
    private List<UnityAction<Vector2Int>> listeners = new List<UnityAction<Vector2Int>>();
    [HideInInspector] public bool alreadyRaised = false;

    public void Raise(Vector2Int coord)
    {
        Debug.LogWarning($"Raised event {this.name}");
        alreadyRaised = true;
        //It iterates backwards so if a listener gets removed during the loop it doesn't interfere with the loop cycle
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            //listeners[i]();
            listeners[i].Invoke(coord);
        }

    }

    public void RegisterListener(UnityAction<Vector2Int> listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(UnityAction<Vector2Int> listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }


}

